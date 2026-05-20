using System;
using System.Collections.Generic;
using System.IO;

namespace MyLibary
{
    public class Person
    {
        public string Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Geburtsdatum { get; set; }
        public string Adresse { get; set; }
        public string Postleitzahl { get; set; }
        public string Telefonnummer { get; set; }

        public Person()
        {
            Id = String.Empty;
            Vorname = String.Empty;
            Nachname = String.Empty;
            Geburtsdatum = String.Empty;
            Adresse = String.Empty;
            Postleitzahl = String.Empty;
            Telefonnummer = String.Empty;
        }
    }
}
