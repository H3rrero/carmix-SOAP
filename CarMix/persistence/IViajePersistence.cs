using CarMix.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMix.persistence
{
    interface IViajePersistence
    {
        List<Viaje> Viajes();

        Viaje FindViaje(int id);

        string AddViaje(Viaje viaje);

        string DeleteViaje(int id);

        string EditViaje(int id, string origen, string destino, int plazas, decimal precio, string descripcion);

    }
}
