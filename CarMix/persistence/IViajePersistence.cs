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

        Viaje FindViaje(long id);

        string AddViaje(Viaje viaje);

        string DeleteViaje(long id);

        string EditViaje(long id, string origen, string destino, int plazas, decimal precio, string descripcion);

        List<string> OrigenesPopulares();

        List<string> DestinosPopulares();

    }
}
