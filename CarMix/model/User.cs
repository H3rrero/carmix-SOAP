using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarMix.model
{
    public class User
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Password { set; get; }
        public string GeneroMusical { set; get; }
        public List<Viaje> ViajesCreados { set; get; }
        public List<Viaje> ViajesSuscrito { set; get; }
    }
}