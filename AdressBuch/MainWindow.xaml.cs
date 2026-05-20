using MyLibary;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AdressBuch
{
    public partial class MainWindow : Window
    {
        private Personen personen;
        private string ChoosenField;
        private int ChoosenFieldId;
        private string User;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ProgrammStart(string UserName)
        {
            User = UserName;

            int PersonenCount;

            string FileInput;
            string FilePath = "Kontakte.txt";
            string[] FileContent;

            FileInput = File.ReadAllText(FilePath);
            FileInput = Personen.Base64Decode(FileInput);
            FileContent = FileInput.Split('\n');

            personen = new Personen();
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
        private void Reload()
        {
            string FileInput;
            string FilePath = "Kontakte.txt";
            string[] FileContent;

            FileInput = File.ReadAllText(FilePath);
            FileInput = Personen.Base64Decode(FileInput);
            FileContent = FileInput.Split('\n');

            personen.SaveChanges("Kontakte.txt");
            foreach (string Content in FileContent)
            {
                string[] KontakteInhalt = Content.Split(';');
                ContactListField.Items.Remove($"ID: {KontakteInhalt[0]}, Name: {KontakteInhalt[1]} {KontakteInhalt[2]}, Geburtsdatum: {KontakteInhalt[3]}, Adresse: {KontakteInhalt[4]}, Postleizahl: {KontakteInhalt[5]}, Telefonnummer: {KontakteInhalt[6]}");
            }

            ProgrammStart(User);
        }

        private void FindChoosenFieldId()
        {
            string[] Field = ChoosenField.Split(':', ',');
            ChoosenFieldId = Convert.ToInt16(Field[1]);
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
            AddPerson add = new AddPerson();

            bool Succes = false;
            Succes = personen.AddPerson();
            if (Succes)
            {
                Error error = new Error();
                error.Show();
            }
        }
        private void RemovePersonClick(object sender, RoutedEventArgs e)
        {
            bool Succes = false;
            Succes = personen.RemovePerson(ChoosenFieldId);

            if (Succes)
            {
                Error error = new Error();
                error.Show();
            }
        }
    }
}