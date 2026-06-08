using ToDoAppBL.Models;

namespace ToDoAppBL.Messages
{
    public class PersoonUpdateMessage
    {
        public Persoon PersoonUpdate { get; set; }
       
        public PersoonUpdateMessage(int id, Persoon persoon)
        {
           
            PersoonUpdate = persoon;
        }
    }
}
