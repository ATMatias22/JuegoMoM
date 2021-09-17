using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM.Excepciones
{
    public class EstaArmaNoPuedeColocarMunicionesException : SystemException
    {
        public EstaArmaNoPuedeColocarMunicionesException(String message) : base(message)
        {
        }
    }
}
