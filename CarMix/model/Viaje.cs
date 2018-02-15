using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarMix.model
{
    public class Viaje
    {
        public long Id { set; get; }
        public string Origen { set; get; }
        public string Destino { set; get; }
        public int Plazas { set; get; }
        public decimal Precio { set; get; }
        public string Descripcion { set; get; }
        public List<User> Invitados { set; get; }
        public User Creador { get; set; }
    }
}