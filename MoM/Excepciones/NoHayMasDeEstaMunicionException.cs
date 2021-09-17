using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM.Excepciones
{
    public class NoHayMasDeEstaMunicionException : SystemException
    {

        public NoHayMasDeEstaMunicionException(String message) : base(message)
        {
            
        }

    }
}
