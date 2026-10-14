using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NInscripcion_Taller
    {
        DInscripcion_Taller dInscripcion = new DInscripcion_Taller();

        public string Registrar(Inscripcion_Taller inscripcion)
        {
            return dInscripcion.Registrar(inscripcion);
        }

        public List<Inscripcion_Taller> ListarTodo()
        {
            return dInscripcion.ListarTodo();
        }

        public List<Inscripcion_Taller> ListarPorTaller(string idTaller)
        {
            return dInscripcion.ListarPorTaller(idTaller);
        }

        public string MarcarAsistencia(string idTaller, string idEstudiante)
        {
            return dInscripcion.MarcarAsistencia(idTaller, idEstudiante);
        }

        public string Eliminar(string idTaller, string idEstudiante)
        {
            return dInscripcion.Eliminar(idTaller, idEstudiante);
        }
    }
}
