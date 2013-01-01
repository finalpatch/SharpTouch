/*
 * Created by SharpDevelop.
 * User: fengli
 * Date: 19/09/2012
 * Time: 9:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Linq;
using System.Collections.Generic;

namespace SharpTouch
{
    public class Touchpad
    {
        const int motionThreshhold = 500;
        const int pushPullThreshold = 200;
        const int swipeThreshold = 200;
        int xScrollScale = 1000;
        int yScrollScale = 1000;

        readonly SYNCOMLib.SynAPI m_api = new SYNCOMLib.SynAPIClass();
        readonly SYNCOMLib.SynDevice m_dev = new SYNCOMLib.SynDeviceClass();
        readonly SYNCOMLib.SynPacket packet = new SYNCOMLib.SynPacket();
        readonly AutoResetEvent m_notifyEvent = new AutoResetEvent(false);
        bool m_actionStarted = false;
        bool m_gestureInProgress = false;
        bool m_scrolling = false;
        readonly int m_xMin;
        readonly int m_xMax;
        readonly int m_yMin;
        readonly int m_yMax;
        System.Drawing.Point m_startPosition = new System.Drawing.Point(0, 0);
        ControlPanel m_cpl;

        public Touchpad()
        {
            m_api.Initialize();
            
            int hdev = -1;
            m_api.FindDevice((int)SYNCTRLLib.SynConnectionType.SE_ConnectionAny, (int)SYNCTRLLib.SynDeviceType.SE_DeviceTouchPad, ref hdev);
            if (hdev < 0)
            {
                MessageBox.Show("No Synaptics TouchPad device found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }            
            m_dev.Select(hdev);
            m_dev.GetProperty((int)SYNCTRLLib.SynDeviceProperty.SP_XLoBorder, ref m_xMin);
            m_dev.GetProperty((int)SYNCTRLLib.SynDeviceProperty.SP_XHiBorder, ref m_xMax);
            m_dev.GetProperty((int)SYNCTRLLib.SynDeviceProperty.SP_YLoBorder, ref m_yMin);
            m_dev.GetProperty((int)SYNCTRLLib.SynDeviceProperty.SP_YHiBorder, ref m_yMax);

            // must create form (to get the sync context) before start processing events
            m_cpl = new ControlPanel(m_api, m_dev);
            m_cpl.SettingsChanged += m_cpl_SettingsChanged;

            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

            m_dev.SetEventNotification(m_notifyEvent.SafeWaitHandle.DangerousGetHandle());
            ProcessTouchEvents();

#if DEBUG
            ShowControlPanel();
#endif
        }

        void m_cpl_SettingsChanged()
        {
            xScrollScale = m_cpl.ScrollSpeedX;
            yScrollScale = m_cpl.ScrollSpeedY;
        }

        async void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Resume)
            {
                for (int i = 0; i < 20; ++i)
                {
                    await Task.Delay(500);
                    m_dev.SetEventNotification(m_notifyEvent.SafeWaitHandle.DangerousGetHandle());
                }
            }
        }

        public void ShowControlPanel()
        {
            m_cpl.Show();
        }

        async void ProcessTouchEvents()
        {
            await Task.Run(() => { m_notifyEvent.WaitOne(); } );
            while (true)
            {
                try
                {
                    m_dev.LoadPacket(packet);
                    ProcessPacket(packet);
                }
                catch (COMException)
                {
                    break;
                }
            }
            ProcessTouchEvents();
        }

        void ProcessPacket(SYNCOMLib.SynPacket packet)
        {
            int extraState = 0;
            int fingerState = 0;
            int xDelta = 0;
            int yDelta = 0;

            packet.GetProperty((int)SYNCTRLLib.SynPacketProperty.SP_ExtraFingerState, ref extraState);
            packet.GetProperty((int)SYNCTRLLib.SynPacketProperty.SP_FingerState, ref fingerState);
            // only read horizontal offset when shift key is down or caps lock is on
            if (Control.ModifierKeys.HasFlag(Keys.Shift) || Control.IsKeyLocked(Keys.CapsLock))
            {
                packet.GetProperty((int)SYNCTRLLib.SynPacketProperty.SP_XDelta, ref xDelta);
            }
            packet.GetProperty((int)SYNCTRLLib.SynPacketProperty.SP_YDelta, ref yDelta);

            int numOfFingers = extraState & 3;

            // no finger
            if ((fingerState & (int)SYNCTRLLib.SynFingerFlags.SF_FingerPresent) == 0)
            {
                m_actionStarted = false;
                m_gestureInProgress = false;
                m_scrolling = false;
                return;
            }

            if ((fingerState & (int)SYNCTRLLib.SynFingerFlags.SF_FingerMotion) == 0)
                return;

            if (Math.Abs(xDelta) > motionThreshhold || Math.Abs(yDelta) > motionThreshhold)
                return;

            // 2 finger scroll
            if (numOfFingers == 2)
            {
                int x = 0;
                int y = 0;
                packet.GetProperty((int)SYNCTRLLib.SynPacketProperty.SP_X, ref x);
                packet.GetProperty((int)SYNCTRLLib.SynPacketProperty.SP_Y, ref y);
                // not at edges
                if (!(x <= m_xMin || x >= m_xMax || y <= m_yMin || y >= m_yMax))
                {
                    DoScroll(xDelta, yDelta);
                }       
                m_scrolling = true;
            }
            else if (numOfFingers == 1 && m_scrolling)
            {
                DoScroll(xDelta, yDelta);
            }
            else if (numOfFingers == 3 && !m_actionStarted)
            {
                Handle3FingerGestures(packet);
            }
        }

        private void Handle3FingerGestures(SYNCOMLib.SynPacket packet)
        {
            int x = 0;
            int y = 0;
            packet.GetProperty((int)SYNCTRLLib.SynPacketProperty.SP_X, ref x);
            packet.GetProperty((int)SYNCTRLLib.SynPacketProperty.SP_Y, ref y);

            if (!m_gestureInProgress)
            {
                m_gestureInProgress = true;
                m_startPosition = new System.Drawing.Point(x, y);
            }
            else
            {
                int xSwipe = x - m_startPosition.X;
                int ySwipe = y - m_startPosition.Y;

                if (Math.Abs(ySwipe) > Math.Abs(xSwipe))
                {
                    if (ySwipe > pushPullThreshold)
                    {
                        // push
                        m_actionStarted = true;
                        DoKeySeq(m_cpl.UpAction.KeySeq);
                    }
                    else if (ySwipe < (-pushPullThreshold))
                    {
                        // pull
                        m_actionStarted = true;
                        DoKeySeq(m_cpl.DownAction.KeySeq);
                    }
                }
                else
                {
                    if (xSwipe < -swipeThreshold)
                    {
                        // left
                        m_actionStarted = true;
                        DoKeySeq(m_cpl.LeftAction.KeySeq);
                    }
                    else if (xSwipe > swipeThreshold)
                    {
                        // right
                        m_actionStarted = true;
                        DoKeySeq(m_cpl.RightAction.KeySeq);
                    }
                }
            }
        }

        void DoScroll(int dx, int dy)
        {
            dx = dx * xScrollScale / 1000;
            dy = dy * yScrollScale / 1000;
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, dy, 0);
            mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, dx, 0);
        }

        void DoKeySeq(Keys[] keys)
        {
            foreach (var key in keys)
                keybd_event((byte)key, 0, 0, UIntPtr.Zero);
            foreach (var key in keys.Reverse())
                keybd_event((byte)key, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }

        #region P/Invoke
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_WHEEL = 0x0800;
        const uint MOUSEEVENTF_HWHEEL = 0x1000;
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        const uint KEYEVENTF_EXTENDEDKEY = 0x1;
        const uint KEYEVENTF_KEYUP = 0x2;
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        #endregion
    }
}
