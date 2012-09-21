/*
 * Created by SharpDevelop.
 * User: fengli
 * Date: 23/06/2012
 * Time: 12:41 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SharpTouch
{
    public sealed class SharpTouchApp
    {
        private NotifyIcon m_notifyIcon;
        private ContextMenu m_notificationMenu;
        private Touchpad m_touchpad;
        
        #region Initialize icon and menu
        public SharpTouchApp()
        {
            m_notifyIcon = new NotifyIcon();
            m_notificationMenu = new ContextMenu(InitializeMenu());
            
            m_notifyIcon.DoubleClick += IconDoubleClick;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SharpTouchApp));
            m_notifyIcon.Icon = (Icon)resources.GetObject("$this.Icon");
            m_notifyIcon.ContextMenu = m_notificationMenu;
            
            m_touchpad = new Touchpad();
        }
        
        private MenuItem[] InitializeMenu()
        {
            MenuItem[] menu = new MenuItem[] {
                new MenuItem("Settings", menuSettingsClick),
                new MenuItem("Exit", menuExitClick)
            };
            return menu;
        }
        #endregion
        
        #region Main - Program entry point
        /// <summary>Program entry point.</summary>
        /// <param name="args">Command Line Arguments</param>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            bool isFirstInstance;
            // Please use a unique name for the mutex to prevent conflicts with other programs
            using (Mutex mtx = new Mutex(true, "SharpTouch", out isFirstInstance)) {
                if (isFirstInstance) {
                    SharpTouchApp app = new SharpTouchApp();
                    app.m_notifyIcon.Visible = true;
                    Application.Run();
                    app.m_notifyIcon.Dispose();
                } else {
                    // The application is already running
                    // TODO: Display message box or change focus to existing application instance
                }
            } // releases the Mutex
        }
        #endregion
        
        #region Event Handlers
        private void menuSettingsClick(object sender, EventArgs e)
        {
            m_touchpad.ShowControlPanel();
        }
        
        private void menuExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void IconDoubleClick(object sender, EventArgs e)
        {
            m_touchpad.ShowControlPanel();
        }
        #endregion
    }
}
