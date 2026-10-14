using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NEstudiante
    {

        DEstudiante dEstudiante = new DEstudiante();

        public string Registrar(Estudiante estudiante)
        {
            return dEstudiante.Registrar(estudiante);
        }

        public List<Estudiante> ListarTodo()
        {
            return dEstudiante.ListarTodo();
        }
        public string Modificar(Estudiante estudiante)
        {
            return dEstudiante.Modificar(estudiante);
        }

        public string Eliminar(string id)
        {
            return dEstudiante.Eliminar(id);
        }

        public Estudiante BuscarPorId(string id)
        {

                return dEstudiante.BuscarPorId(id);
            

        }
    }
}
