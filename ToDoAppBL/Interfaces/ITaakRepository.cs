using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppBL.Models;

namespace ToDoAppBL.Interfaces
{
    public interface ITaakRepository
    {
        void BewaarTaak(Taak t);
        List<Taak> GeefAlleTaken();
        Taak GeefTaakMetId(int id);
    }
}
