namespace ToDoAppBL.Models
{
    public class Taak
    {
        public Taak()
        {
        }

        public Taak(string titel, string beschrijving, bool isAfgewerkt, Persoon persoon)
        {
            Titel = titel;
            Beschrijving = beschrijving;
            IsAfgewerkt = isAfgewerkt;
            Persoon = persoon;
        }

        public Taak(string titel, string beschrijving, bool isAfgewerkt, Persoon persoon, DateTime datumTaakAanmaak, DateTime datumTaakWijziging)
        {
            Titel = titel;
            Beschrijving = beschrijving;
            IsAfgewerkt = isAfgewerkt;
            Persoon = persoon;
            DatumTaakAanmaak = datumTaakAanmaak;
            DatumTaakWijziging = datumTaakWijziging;
        }

        public Taak(int id, string titel, string beschrijving, bool isAfgewerkt, Persoon persoon, DateTime datumTaakAanmaak)
        {
            Id = id;
            Titel = titel;
            Beschrijving = beschrijving;
            IsAfgewerkt = isAfgewerkt;
            Persoon = persoon;
            DatumTaakAanmaak = datumTaakAanmaak;
        }

        public Taak(int id, string titel, string beschrijving, bool isAfgewerkt, Persoon persoon, DateTime datumTaakAanmaak, DateTime datumTaakWijziging)
        {
            Id = id;
            Titel = titel;
            Beschrijving = beschrijving;
            IsAfgewerkt = isAfgewerkt;
            Persoon = persoon;
            DatumTaakAanmaak = datumTaakAanmaak;
            DatumTaakWijziging = datumTaakWijziging;
        }

        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public bool IsAfgewerkt { get; set; }
        public Persoon Persoon { get; set; }
        public DateTime DatumTaakAanmaak { get; set; }
        public DateTime DatumTaakWijziging { get; set; }

    }
}
