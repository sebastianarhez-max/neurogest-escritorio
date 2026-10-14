using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NEvaluacion
    {
        DEvaluacion dEvaluacion = new DEvaluacion();

        public string Registrar(Evaluacion evaluacion)
        {
            return dEvaluacion.Registrar(evaluacion);
        }

        public List<Evaluacion> ListarTodo()
        {
            return dEvaluacion.ListarTodo();
        }

        public List<Cita> ListarCitasRealizadas()
        {
            return dEvaluacion.ListarCitasRealizadas();
        }

        public string Modificar(Evaluacion evaluacion)
        {
            return dEvaluacion.Modificar(evaluacion);
        }

        public string Eliminar(int id)
        {
            return dEvaluacion.Eliminar(id);
        }
    }
}
