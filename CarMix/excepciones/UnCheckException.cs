using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarMix.excepciones
{
    public class UnCheckException : Exception
    {
        public UnCheckException():base(message: "Se necesitan permisos de administrador para realizar esta operación")
        {
        }

        public UnCheckException(string message)
            : base(message)
        {
        }

        public UnCheckException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}