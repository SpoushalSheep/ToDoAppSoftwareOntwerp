using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppBL.Models;

namespace ToDoAppBL.Messages
{
    public class TaakAddMessage
    {
        public Taak TaakUpdate { get; set; }
        public TaakAddMessage(Taak taak)
        {

            TaakUpdate = taak;
        }
    }
}
