using ToDoAppBL.Models;

namespace ToDoAppBL.Messages
{
    public class TaakUpdateMessage
    {
        public Taak TaakUpdate { get; set; }
        public TaakUpdateMessage(Taak taak)
        {

            TaakUpdate = taak;
        }
    }
}
