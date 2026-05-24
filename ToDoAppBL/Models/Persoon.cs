namespace ToDoAppBL.Models
{
    public class Persoon
    {
        public Persoon(string voornaam, string achternaam, string? url, DateOnly geboorteDatum) // creërt object zonder ID
        {
            Voornaam = voornaam;
            Achternaam = achternaam;
            Url = url;
            GeboorteDatum = geboorteDatum;
        }

        public Persoon(string voornaam, string achternaam, string? url, DateOnly geboorteDatum, DateTime datumProfielAanmaak, DateTime datumProfielWijziging)
        {
            Voornaam = voornaam;
            Achternaam = achternaam;
            Url = url;
            GeboorteDatum = geboorteDatum;
            DatumProfielAanmaak = datumProfielAanmaak;
            DatumProfielWijziging = datumProfielWijziging;
        }

        public Persoon(int id, string voornaam, string achternaam, string? url, DateOnly geboorteDatum, DateTime datumProfielAanmaak, DateTime datumProfielWijziging)
        {
            Id = id;
            Voornaam = voornaam;
            Achternaam = achternaam;
            Url = url;
            GeboorteDatum = geboorteDatum;
            DatumProfielAanmaak = datumProfielAanmaak;
            DatumProfielWijziging = datumProfielWijziging;
        }

        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string? Url { get; set; }
        public DateOnly GeboorteDatum { get; set; }
        public DateTime DatumProfielAanmaak { get; set; }
        public DateTime DatumProfielWijziging { get; set; }


        public int BerekenLeeftijd() // berekend leeftijd
        {
            int leeftijd = DateTime.Now.Year - GeboorteDatum.Year;
            DateOnly vandaag = DateOnly.FromDateTime(DateTime.Now); // checkt of persoon al verjaard is 

            if (vandaag < GeboorteDatum.AddYears(leeftijd))
            {
                leeftijd--; // als de dag nog niet gepaseerd is trekt hij er een jaartje af
            }

            return leeftijd;

        }

        public override string ToString()
        {
            return $"{Voornaam} {Achternaam} ({BerekenLeeftijd()})";
        }
        public bool Equals(Persoon other)
        {
            if (other == null)
                return false;
            return this.Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Persoon);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
