using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppBL.Models;

namespace ToDoAppBL.Messages
{
    public class PersoonVerwijderMessage
    {
        public Persoon PersoonVerwijder { get; set; }

        public PersoonVerwijderMessage(Persoon persoon)
        {

            PersoonVerwijder = persoon;
        }
    }
}
