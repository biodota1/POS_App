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

            Application.Run(new MemberForm());

            /*   if (authForm.ShowDialog() == DialogResult.OK)
               {
                   if (authForm.UserRole == "Admin")
                   {
                       Application.Run(new AdminForm());
                   }
                   else
                   {
                       Application.Run(new MemberForm());
                   }

               }*/

        }
    }
}
