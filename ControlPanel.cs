﻿using System;
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
    public partial class ControlPanel : Form
    {
        readonly Assembly asm = Assembly.GetExecutingAssembly();
        const string cvrun = @"Software\Microsoft\Windows\CurrentVersion\Run";

        public ControlPanel(SYNCOMLib.SynAPI api, SYNCOMLib.SynDevice device)
        {
            InitializeComponent();

            // auto start checkbox
            using (RegistryKey run = Registry.CurrentUser.OpenSubKey(cvrun))
            {
                m_autoStart.Checked = (run.GetValue(asm.GetName().Name) != null);
            }

            // api version and device version
            m_apiVer.Text = GetSynapticAPIStringProperty(api, SYNCTRLLib.SynAPIStringProperty.SP_VersionString, 100);
            m_devName.Text = GetSynapticDeviceStringProperty(device, SYNCTRLLib.SynDeviceStringProperty.SP_ModelString, 100);
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
    }
}
