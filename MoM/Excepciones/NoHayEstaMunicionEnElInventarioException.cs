using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM.Excepciones
{ 
    public class NoHayEstaMunicionEnElInventarioException : SystemException
    {
        public NoHayEstaMunicionEnElInventarioException(String message) : base(message)
        {
        }
    }
}
