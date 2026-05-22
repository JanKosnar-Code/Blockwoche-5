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
        private readonly MainWindow? mainWindow;
        public AddPerson(MainWindow? mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            Input = $"{Id.Text}, {Name.Text}, {Nachname.Text}, {Geburtsdatum.Text}, {Adresse.Text}, {Postleizahl.Text}, {Telefonnummer.Text}";

            mainWindow.NewPerson(Input);
            Close();
        }
    }
}
