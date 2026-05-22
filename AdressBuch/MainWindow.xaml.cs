using MyLibary;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AdressBuch
{
    public partial class MainWindow : Window
    {
        private Personen personen;
        private string ChoosenField;
        private int ChoosenFieldId;
        private string User;
        private int PersonenCount;
        // private bool Color = false;

        public MainWindow()
        {
            InitializeComponent();
            personen = new Personen();
        }

        public void ProgrammStart(string UserName)
        {
            User = UserName;

            string FileInput;
            string FilePath = "Kontakte.txt";
            string[] FileContent;

            FileInput = File.ReadAllText(FilePath);
            FileInput = Personen.Base64Decode(FileInput);
            FileContent = FileInput.Split('\n');

            personen.GetPersonen(FileContent);

            PersonenCount = personen.GetPersonenCount();

            foreach (string Content in FileContent) 
            {
                string[] KontakteInhalt = Content.Split(';');
                ContactListField.Items.Add($"ID: {KontakteInhalt[0]}, Name: {KontakteInhalt[1]} {KontakteInhalt[2]}, Geburtsdatum: {KontakteInhalt[3]}, Adresse: {KontakteInhalt[4]}, Postleizahl: {KontakteInhalt[5]}, Telefonnummer: {KontakteInhalt[6]}");
            }

            LoginButton.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Visible;
            RemovePersonButton.Visibility = Visibility.Visible;
            AddPersonButton.Visibility = Visibility.Visible;

            UserNameText.Visibility = Visibility.Visible;
            PersonenCountBlock.Visibility = Visibility.Visible;

            UserNameText.Text = UserName;
            PersonenCountBlock.Text = $"Du hast {PersonenCount} Kontakte.";
        }
        private void FindChoosenFieldId()
        {
            string[] Field = ChoosenField.Split(':', ',');
            ChoosenFieldId = Convert.ToInt16(Field[1]);
        }
        public void NewPerson(string Input)
        {
            string Succes;

            Succes = personen.AddPerson(Input);
            if (Succes == "")
            {
                Error error = new Error();
                error.Owner = this;
                error.ShowDialog(); 
                error.Show();
            }
            else
            {
                ContactListField.Items.Add(Succes);
                PersonenCount = personen.GetPersonenCount();
                PersonenCountBlock.Text = $"Du hast {PersonenCount} Kontakte.";
            }
        }
        private void ContactListField_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContactListField.SelectedItem != null)
            {
                ChoosenField = ContactListField.SelectedItem.ToString();
                FindChoosenFieldId();
            }
        }
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();

            loginWindow.Show();
            this.Visibility = Visibility.Hidden;
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            personen.SaveChanges("Kontakte.txt");
        }
        private void AddPersonClick(object sender, RoutedEventArgs e)
        {
            AddPerson addPersonWindow = new AddPerson(this);
            addPersonWindow.Owner = this;
            addPersonWindow.ShowDialog();
        }
        private void RemovePersonClick(object sender, RoutedEventArgs e)
        {
            string Succes;
            Succes = personen.RemovePerson(ChoosenFieldId);

            if (Succes == "")
            {
                Error error = new Error();
                error.Show();
            }
            else
            {
                ContactListField.Items.Remove(Succes);
                PersonenCount = personen.GetPersonenCount();
                PersonenCountBlock.Text = $"Du hast {PersonenCount} Kontakte.";
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Style style = new Style
            {
                TargetType = typeof(Window)
            };

            style.Setters.Add(new Setter(Window.BackgroundProperty, (bool)((CheckBox)sender).IsChecked! ? Brushes.Black : Brushes.White));

            Application.Current.Resources["Colour"] = style;
        }
    }
}