using CarMix.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMix.persistence
{
    interface IUserViajePersistence
    {
        List<Viaje> ViajesCreados(long idUser);
        List<Viaje> ViajesSuscrito(long idUser);
        User CreadorViaje(long idViaje);
        List<User> InvitadosViaje(long idViaje);
        bool DeleteUserViaje(int idViaje);
    }
}
