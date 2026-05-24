using ToDoAppBL.Models;

namespace ToDoAppBL.Messages
{
    public class PersoonUpdateMessage
    {
        public Persoon? PersoonUpdate { get; set; }
        public int PersoonId { get; set; }
        public PersoonUpdateMessage(int id, Persoon persoon)
        {
            PersoonId = id;
            PersoonUpdate = persoon;
        }
    }
}
