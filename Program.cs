using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2_POS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AuthForm authForm = new AuthForm();
            AdminForm adminForm = new AdminForm();
            MemberForm memberForm = new MemberForm();
            Application.Run(new AuthForm());
 /*           Application.Run(new AdminForm());*/


        }
    }
}
