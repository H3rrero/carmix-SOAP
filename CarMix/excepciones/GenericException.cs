using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarMix.excepciones
{
    public class GenericException : Exception
    {
        public GenericException() : base(message: "Se produjo un error al procesar su petición")
        {
        }

        public GenericException(string message)
            : base(message)
        {
        }

        public GenericException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}