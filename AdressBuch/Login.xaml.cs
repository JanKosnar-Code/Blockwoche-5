using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace AdressBuch
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            string UserListTxtPath = "UserList.txt";
            string UserList;
            string Password = PWD.Text;
            string UserData = User.Text;

            bool IdentityIsCorrect = false;

            string[] UserPwdList;

            if (Password == null || UserData == null) 
                return;
            else
            {
                UserList = File.ReadAllText(UserListTxtPath);
                UserPwdList = UserList.Split("\r\n");

                foreach (string UserPwd in UserPwdList) 
                {
                    if (UserPwd == UserData + " " + Password)
                        IdentityIsCorrect = true;
                }

                if (IdentityIsCorrect) 
                {
                    MainWindow main = new MainWindow();
                    main.Show();

                    main.ProgrammStart(UserData);

                    this.Close();
                    return;
                }
            }
        }
    }
}
