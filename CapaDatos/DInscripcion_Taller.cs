using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DInscripcion_Taller
    {
        public string Registrar(Inscripcion_Taller inscripcion)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    var encontrado = context.Inscripcion_Taller.Find(
                        inscripcion.id_Taller,
                        inscripcion.id_Estudiante
                    );

                    if (encontrado != null)
                    {
                        return "El estudiante ya está inscrito en este taller";
                    }

                    inscripcion.asistencia = "Inscrito";
                    context.Inscripcion_Taller.Add(inscripcion);
                    context.SaveChanges();

                    return "Registro correcto";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Inscripcion_Taller> ListarTodo()
        {
            using (var context = new NeuroGGestEntities())
            {
                return context.Inscripcion_Taller.ToList();
            }
        }

        public List<Inscripcion_Taller> ListarPorTaller(string idTaller)
        {
            using (var context = new NeuroGGestEntities())
            {
                return context.Inscripcion_Taller.Where(i => i.id_Taller == idTaller).ToList();
            }
        }

        public string MarcarAsistencia(string idTaller, string idEstudiante)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    var inscripcion = context.Inscripcion_Taller.Find(idTaller, idEstudiante);

                    if (inscripcion == null)
                    {
                        return "No se encontró la inscripción";
                    }

                    inscripcion.asistencia = "Asistió";

                    context.SaveChanges();

                    return "Asistencia registrada correctamente";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Eliminar(string idTaller, string idEstudiante)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    var eliminado = context.Inscripcion_Taller.Find(idTaller, idEstudiante);

                    if (eliminado == null)
                    {
                        return "No se encontró la inscripción";
                    }

                    context.Inscripcion_Taller.Remove(eliminado);
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
