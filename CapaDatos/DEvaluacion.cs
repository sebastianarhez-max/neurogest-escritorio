using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DEvaluacion
    {
        public string Registrar(Evaluacion evaluacion)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    Cita cita = context.Cita.Find(evaluacion.id_Cita);

                    if (cita == null)
                    {
                        return "No se encontró la cita";
                    }

                    if (cita.estado != "Realizada")
                    {
                        return "Solo se puede registrar evaluación a una cita realizada";
                    }

                    List<Evaluacion> evaluaciones = context.Evaluacion.Where(e => e.id_Cita == evaluacion.id_Cita).ToList();

                    if (evaluaciones.Count != 0)
                    {
                        return "La cita ya tiene una evaluación registrada";
                    }

                    context.Evaluacion.Add(evaluacion);
                    context.SaveChanges();

                    return "Registro correcto";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Evaluacion> ListarTodo()
        {
            using (var context = new NeuroGGestEntities())
            {
                return context.Evaluacion
                              .Include("Cita")
                              .Include("Cita.Estudiante")
                              .ToList();
            }
        }

        public List<Cita> ListarCitasRealizadas()
        {
            using (var context = new NeuroGGestEntities())
            {
                return context.Cita.Where(c => c.estado == "Realizada").ToList();
            }
        }

        public string Modificar(Evaluacion evaluacion)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    Evaluacion modificado = context.Evaluacion.Find(evaluacion.id);

                    if (modificado == null)
                    {
                        return "No se encontró la evaluación";
                    }

                    modificado.observaciones = evaluacion.observaciones;
                    modificado.resultados = evaluacion.resultados;
                    modificado.recomendacion = evaluacion.recomendacion;

                    context.SaveChanges();

                    return "Se modificó correctamente";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Eliminar(int id)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    Evaluacion eliminado = context.Evaluacion.Find(id);

                    if (eliminado == null)
                    {
                        return "No se encontró la evaluación";
                    }

                    context.Evaluacion.Remove(eliminado);
                    context.SaveChanges();

                    return "Se eliminó correctamente";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
