using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DCita
    {
        public string Registrar(Cita cita)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    cita.id = GenerarId();
                    cita.estado = "Solicitada";
                    context.Cita.Add(cita);
                    context.SaveChanges();
                    return "Registro correcto";
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string errores = "";

                foreach (var entityErrors in ex.EntityValidationErrors)
                {
                    errores += "Entidad: " + entityErrors.Entry.Entity.GetType().Name + "\n";

                    foreach (var error in entityErrors.ValidationErrors)
                    {
                        errores += error.PropertyName + " -> " + error.ErrorMessage + "\n";
                    }
                }

                return errores;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string GenerarId()
        {
            using (var context = new NeuroGGestEntities())
            {
                var ultimaCita = context.Cita
                                        .OrderByDescending(c => c.id)
                                        .FirstOrDefault();

                if (ultimaCita == null)
                    return "CIT001";

                int numero = int.Parse(ultimaCita.id.Substring(3)) + 1;

                return "CIT" + numero.ToString("000");
            }
        }
        public List<Cita> ListarTodo()
        {
            using (var context = new NeuroGGestEntities())
            {
                return context.Cita.ToList();
            }
        }

        public string Modificar(Cita cita)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    Cita modificado = context.Cita.Find(cita.id);
                    if (modificado == null)
                    {
                        return "No se encontró la cita";
                    }
                    modificado.id_Estudiante = cita.id_Estudiante;
                    modificado.id_Especialista = cita.id_Especialista;
                    modificado.fecha = cita.fecha;
                    modificado.hora = cita.hora;
                    context.SaveChanges();
                    return "Se modificó correctamente";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string MarcarProgramada(string id)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    Cita cita = context.Cita.Find(id);
                    if (cita == null)
                    {
                        return "No se encontró la cita";
                    }
                    cita.estado = "Programada";
                    context.SaveChanges();

                    return "La cita fue marcada como Programada";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string MarcarRealizada(string id)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    Cita cita = context.Cita.Find(id);
                    if (cita == null)
                    {
                        return "No se encontró la cita";
                    }
                    cita.estado = "Realizada";
                    context.SaveChanges();
                    Evaluacion evaluacion = new Evaluacion();

                    evaluacion.id = GenerarIdEvaluacion();
                    evaluacion.id_Cita = cita.id;
                    evaluacion.observaciones = "";
                    evaluacion.resultados = "";
                    evaluacion.recomendacion = "";

                    context.Evaluacion.Add(evaluacion);

                    context.SaveChanges();

                    return "La cita fue marcada como realizada";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Eliminar(string id)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    Cita eliminado = context.Cita.Find(id);
                    if (eliminado == null)
                    {
                        return "No se encontró la cita";
                    }
                    context.Cita.Remove(eliminado);
                    context.SaveChanges();

                    return "Se eliminó correctamente";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public bool HorarioOcupado(Cita cita)
        {
            using (var context = new NeuroGGestEntities())
            {
                return context.Cita.Any(c => c.id_Especialista == cita.id_Especialista && c.fecha == cita.fecha && c.hora == cita.hora);
            }
        }

        public int GenerarIdEvaluacion()
        {
            using (var context = new NeuroGGestEntities())
            {
                if (!context.Evaluacion.Any())
                    return 1;

                return context.Evaluacion.Max(e => e.id) + 1;
            }
        }
    }
}
