﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WorkLibrary;
using NewProject;

namespace AutoUpForum
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (Common.CheckRegister() == false)
            {
                MessageBox.Show("Phần mềm chưa được đăng ký sử dụng.Vui lòng đăng ký sử dụng phần mềm", "Thông báo");
                frmRegister frm = new frmRegister();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new frmAutoUpForum());
                }
            }
            else
            {
                Application.Run(new frmAutoUpForum());
            }
        }
    }
}
