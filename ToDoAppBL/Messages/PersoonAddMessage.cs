using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppBL.Models;

namespace ToDoAppBL.Messages
{
    public class PersoonAddMessage
    {
        public Persoon PersoonAdd { get; set; }
        
        public PersoonAddMessage( Persoon persoon)
        {
           
            PersoonAdd = persoon;
        }
    }
}
