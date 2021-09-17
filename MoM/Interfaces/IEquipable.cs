using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM
{
    public interface IEquipable
    {
        public bool sePudoAutoEquipar(Personaje p);
        public void desequiparse(Personaje p);
    }
}
