using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM.Excepciones
{
    public class YaTieneMunicionEquipadaException : SystemException
    {

        public YaTieneMunicionEquipadaException(String message) : base(message)
        {

        }

    }
}
