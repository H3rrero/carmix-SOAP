using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarMix.excepciones
{
    public class FindException: Exception
    {
        public FindException() : base(message: "No se ha encontrado la entidad solicitada")
        {
        }

        public FindException(string message)
            : base(message)
        {
        }

        public FindException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}