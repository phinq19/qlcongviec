﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WorkLibrary;

namespace CreateWebStep
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
                
            }
            else
            {
                Application.Run(new frmWebPageList());
            }
            
        }
    }
}
