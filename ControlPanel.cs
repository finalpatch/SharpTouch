using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;

namespace SharpTouch
{
    public enum GestureAction
    {
        AeroPeek,
        Flip2D,
        Flip3D,
        MinimizeOthers,

        DockToLeft,
        DockToRight,
        Maximize,
        Minimize,
    };

    public partial class ControlPanel : Form
    {
        readonly Assembly asm = Assembly.GetExecutingAssembly();
        const string cvrun = @"Software\Microsoft\Windows\CurrentVersion\Run";
        const string settingsKeyName = @"Software\SharpTouch";

        public delegate void SettingsChangedEventHandler();
        public event SettingsChangedEventHandler SettingsChanged;

        public int ScrollSpeedX
        {
            get { return m_scrollSpeed.Value; }
        }
        public int ScrollSpeedY
        {
            get { return m_scrollSpeed.Value; }
        }

        public GestureAction UpAction
        {
            get { return (GestureAction)m_cbUpAction.SelectedIndex; }
        }
        public GestureAction DownAction
        {
            get { return (GestureAction)m_cbDownAction.SelectedIndex; }
        }
        public GestureAction LeftAction
        {
            get { return (GestureAction)m_cbLeftAction.SelectedIndex; }
        }
        public GestureAction RightAction
        {
            get { return (GestureAction)m_cbRightAction.SelectedIndex; }
        }

        public ControlPanel(SYNCOMLib.SynAPI api, SYNCOMLib.SynDevice device)
        {
            InitializeComponent();

            // auto start checkbox
            using (RegistryKey run = Registry.CurrentUser.OpenSubKey(cvrun))
            {
                m_autoStart.Checked = (run.GetValue(asm.GetName().Name) != null);
            }

            // speed
            using (RegistryKey mySettings = Registry.CurrentUser.CreateSubKey(settingsKeyName))
            {
                m_scrollSpeed.Value = (int)mySettings.GetValue("ScrollSpeed", 1000);
                m_speedLabel.Text = string.Format("{0}%", m_scrollSpeed.Value / 10);
            }

            FillComboBox(m_cbUpAction);
            FillComboBox(m_cbDownAction);
            FillComboBox(m_cbLeftAction);
            FillComboBox(m_cbRightAction);

            // gestures
            using (RegistryKey mySettings = Registry.CurrentUser.CreateSubKey(settingsKeyName))
            {
                m_cbUpAction.SelectedIndex = (int)mySettings.GetValue("UpAction", (int)GestureAction.Flip3D);
                m_cbDownAction.SelectedIndex = (int)mySettings.GetValue("DownAction", (int)GestureAction.MinimizeOthers);
                m_cbLeftAction.SelectedIndex = (int)mySettings.GetValue("LeftAction", (int)GestureAction.DockToLeft);
                m_cbRightAction.SelectedIndex = (int)mySettings.GetValue("RightAction", (int)GestureAction.DockToRight);
            }

            m_cbUpAction.SelectedIndexChanged += gestureActionChanged;
            m_cbDownAction.SelectedIndexChanged += gestureActionChanged;
            m_cbLeftAction.SelectedIndexChanged += gestureActionChanged;
            m_cbRightAction.SelectedIndexChanged += gestureActionChanged;

            // api version and device version
            m_apiVer.Text = GetSynapticAPIStringProperty(api, SYNCTRLLib.SynAPIStringProperty.SP_VersionString, 100);
            m_devName.Text = GetSynapticDeviceStringProperty(device, SYNCTRLLib.SynDeviceStringProperty.SP_ModelString, 100);
        }

        private void gestureActionChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            using (RegistryKey mySettings = Registry.CurrentUser.CreateSubKey(settingsKeyName))
            {
                mySettings.SetValue((string)cb.Tag, cb.SelectedIndex);
                if (SettingsChanged != null)
                    SettingsChanged();
            }
        }

        void FillComboBox(ComboBox cb)
        {
            string[] items =
            {
                "Aero Peek",
                "Flip2D",
                "Flip3D",
                "Minimize Other Windows",
                "Dock Window to Left",
                "Dock Window to Right",
                "Maximize Window",
                "Minimize Window"
            };
            cb.Items.AddRange(items);
        }

        private void ControlPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void m_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void m_autoStart_CheckedChanged(object sender, EventArgs e)
        {
            using (RegistryKey run = Registry.CurrentUser.OpenSubKey(cvrun, true))
            {
                if (m_autoStart.Checked)
                    run.SetValue(asm.GetName().Name, "\"" + asm.Location + "\"");
                else
                    run.DeleteValue(asm.GetName().Name);
            }
        }

        string GetSynapticAPIStringProperty(SYNCOMLib.SynAPI api, SYNCTRLLib.SynAPIStringProperty prop, int bufSize)
        {
            byte[] buf = new byte[bufSize];
            api.GetStringProperty((int)prop, ref buf[0], ref bufSize);
            return System.Text.ASCIIEncoding.ASCII.GetString(buf, 0, bufSize);
        }
        string GetSynapticDeviceStringProperty(SYNCOMLib.SynDevice dev, SYNCTRLLib.SynDeviceStringProperty prop, int bufSize)
        {
            byte[] buf = new byte[bufSize];
            dev.GetStringProperty((int)prop, ref buf[0], ref bufSize);
            return System.Text.ASCIIEncoding.ASCII.GetString(buf, 0, bufSize);
        }

        private void m_scrollSpeed_Scroll(object sender, EventArgs e)
        {
            m_speedLabel.Text = string.Format("{0}%", m_scrollSpeed.Value / 10);
            using (RegistryKey mySettings = Registry.CurrentUser.CreateSubKey(settingsKeyName))
            {
                mySettings.SetValue("ScrollSpeed", m_scrollSpeed.Value);
                if (SettingsChanged != null)
                    SettingsChanged();
            }
        }
    }
}
