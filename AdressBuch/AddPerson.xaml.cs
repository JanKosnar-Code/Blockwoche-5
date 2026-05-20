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

namespace AdressBuch
{
    public partial class AddPerson : Window
    {
        public string Input;
        public AddPerson()
        {
            InitializeComponent();
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();

            Input = $"{Id.Text}, {Name.Text}, {Nachname.Text}, {Geburtsdatum.Text}, {Adresse.Text}, {Postleizahl.Text}, {Telefonnummer.Text}";

            main.Visibility = Visibility.Visible;
            main.NewPerson(Input);
            this.Close();
        }
    }
}
