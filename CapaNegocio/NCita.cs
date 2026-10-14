using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NCita
    {
        DCita dCita = new DCita();

        public string Registrar(Cita cita)
        {
            if (dCita.HorarioOcupado(cita))
            {
                return "El especialista no está disponible en ese horario. Porfavor intentar de nuevo con otro horario";
            }
            return dCita.Registrar(cita);
        }


        public List<Cita> ListarTodo()
        {
            return dCita.ListarTodo();
        }

        public string Modificar(Cita cita)
        {
            return dCita.Modificar(cita);
        }

        public string MarcarProgramada(string id)
        {
            return dCita.MarcarProgramada(id);
        }

        public string Eliminar(string id)
        {
            return dCita.Eliminar(id);
        }

        public string MarcarRealizada(string id)
        {
            return dCita.MarcarRealizada(id);
        }
    }
}
