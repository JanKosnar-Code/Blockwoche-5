using System;

namespace MyLibary
{
    public class Personen
    {
        private List<Person> personen;
        private string WrongInput = "Falsche Eingabe!";
        public Personen()
        {
            personen = new List<Person>();
        }

        public static string Base64Decode(string Text) // Decoded einen Text in Base64.
        {
            var OutputText = System.Convert.FromBase64String(Text);
            return System.Text.Encoding.UTF8.GetString(OutputText);
        }
        public static string Base64Encode(string Text) // Encoded einen Text in Base64.
        {
            var OutputText = System.Text.Encoding.UTF8.GetBytes(Text);
            return System.Convert.ToBase64String(OutputText);
        }
        public int GetPersonenCount() // Gibt die Anzahl der Personen im System zurück.
        {
            return personen.Count;
        }
        public List<Person> GetPersonen(string[] D) // Liest die Personen aus einer .txt ein.
        {
            string[] Result = new string[7];
            foreach (string item in D)
            {
                Result = item.Split(';');

                Person p = new Person();
                p.Id = Result[0];
                p.Vorname = Result[1];
                p.Nachname = Result[2];
                p.Geburtsdatum = Result[3];
                p.Adresse = Result[4];
                p.Postleitzahl = Result[5];
                p.Telefonnummer = Result[6];

                personen.Add(p);
            }
            return personen;
        }
        public bool Pruefen(string Input) // Prüft ob ein Text Valid ist zum adden
        {
            string[] Text;
            bool CheckNumber;

            DateTime ValidDate;

            Text = Input.Split(", ");

            CheckNumber = double.TryParse(Text[1], out double Temp1);
            if (CheckNumber == false) CheckNumber = double.TryParse(Text[2], out double Temp2);

            if (Text.Length == 7 && CheckNumber == false && DateTime.TryParse(Text[3], out ValidDate)) return true;
            else return false;
        }
        public bool SaveChanges(string Path) // Speichert alle Informationen und gibt einen Bool zurück (true wenn Erfolgreich, false wenn Fehler).
        {
            string BackToTxt = string.Empty;

            int count = 1;

            foreach (Person p in personen)
            {
                if (count == 1) BackToTxt = p.Id + ";";
                else BackToTxt = BackToTxt + p.Id + ";";

                BackToTxt = BackToTxt + p.Vorname + ";";
                BackToTxt = BackToTxt + p.Nachname + ";";
                BackToTxt = BackToTxt + p.Geburtsdatum + ";";
                BackToTxt = BackToTxt + p.Adresse + ";";
                BackToTxt = BackToTxt + p.Postleitzahl + ";";

                if (count == personen.Count) BackToTxt = BackToTxt + p.Telefonnummer;
                else BackToTxt = BackToTxt + p.Telefonnummer + "\n";

                count++;
            }
            if (Path == null)
                return false;
            else
            {
                BackToTxt = Base64Encode(BackToTxt);
                File.WriteAllText(Path, BackToTxt);
                return true;
            }
        }
        public bool RemovePerson(int DelInput) // Löscht einen Datensatz anhand der Datensatz Nummer und gibt einen Bool zurück (Wenn True Erfolgreich, Wenn False Fehlgeschlagen).
        {
            string StringDelInput = DelInput.ToString();
            try
            {
                foreach (Person p in personen)
                {
                    if (p.Id == StringDelInput)
                    {
                        personen.Remove(p);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<string> AllPersons() // Gibt dir Alle Personen zurück
        {
            List<string> list = new List<string>();
            foreach (Person p in personen)
            {
                list.Add($"{p.Id} {p.Vorname} {p.Nachname} {p.Geburtsdatum} {p.Adresse} {p.Postleitzahl} {p.Telefonnummer}");
            }
            return list;
        }
        public bool AddPerson(string Input) // Gibt dir anhand des Inputs einen bool zurück (Wenn True Erfolgreich, Wenn False Fehlgeschlagen).
        {
            bool ValidAnswer = true;

            string[] Text;

            Person p = new Person();

            if (Input == null) return false;

            ValidAnswer = Pruefen(Input);

            if (ValidAnswer == true)
            {
                Text = Input.Split(", ");

                p.Id = Text[0];
                p.Vorname = Text[1];
                p.Nachname = Text[2];
                p.Geburtsdatum = Text[3];
                p.Adresse = Text[4];
                p.Postleitzahl = Text[5];
                p.Telefonnummer = Text[6];

                personen.Add(p);

                return true;
            }
            else return false;
        }
        public List<string> PersonSearch(string Input) // Anhand eines Begriffes in einem Kontakt (Telefonnummer, Adresse, Name etc) sucht es nach Kontakten.
        {
            int Count = 1;

            List<string> list = new List<string>();

            if (Input == null) { list.Add(WrongInput); return list; }

            if (list[0] != WrongInput)
            {
                foreach (Person p in personen)
                {
                    bool Übereinstimung = false;

                    if (p.Id == Input) Übereinstimung = true;
                    else if (p.Vorname == Input) Übereinstimung = true;
                    else if (p.Nachname == Input) Übereinstimung = true;
                    else if (p.Geburtsdatum == Input) Übereinstimung = true;
                    else if (p.Adresse == Input) Übereinstimung = true;
                    else if (p.Postleitzahl == Input) Übereinstimung = true;
                    else if (p.Telefonnummer == Input) Übereinstimung = true;

                    if (Übereinstimung)
                        list.Add($"Person {p.Id}: {p.Vorname} {p.Nachname} {p.Geburtsdatum} {p.Adresse} {p.Postleitzahl} {p.Telefonnummer}");

                    Count++;
                }
            }
            return list;
        }
    }
}
