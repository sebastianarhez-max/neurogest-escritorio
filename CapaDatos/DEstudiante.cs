using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DEstudiante
    {

        public string Registrar(Estudiante estudiante)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    string ultimoId = context.Estudiante
                                     .OrderByDescending(e => e.id)
                                     .Select(e => e.id)
                                     .FirstOrDefault();
                    int numero = int.Parse(ultimoId.Substring(3));
                    estudiante.id = "EST" + (numero + 1).ToString("D3");
                    Usuario usuario = new Usuario()
                    {
                        login = estudiante.id,
                        clave = (numero + 1).ToString(),
                        rol = "Estudiante",
                        activo = true
                    };
                    if (context.Estudiante.Any(e=>e.id == estudiante.id))
                    {
                        return "El estudiante ya existe";
                    }
                    context.Usuario.Add(usuario);
                    context.Estudiante.Add(estudiante);
                    context.SaveChanges();
                    return "Registro correcto";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        public List<Estudiante> ListarTodo()
        {
            using (var context = new NeuroGGestEntities())
            {
                return context.Estudiante.ToList();
            }
        }

        public string Modificar(Estudiante estudiante)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    var modificado = context.Estudiante.Find(estudiante.id);

                    if (modificado == null)
                    {
                        return "No se encontró el estudiante";
                    }

                    modificado.nombre = estudiante.nombre;
                    modificado.gradoAcademico = estudiante.gradoAcademico;
                    modificado.contactoApod = estudiante.contactoApod;
                    modificado.modificadoPor = estudiante.modificadoPor;
                    modificado.fechaModific = DateTime.Now;

                    context.SaveChanges();
                    return "Se modificó correctamente";
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
                    var eliminado = context.Estudiante.Find(id);
                    if (eliminado == null)
                    {
                        return "No se encontró el estudiante";
                    }
                    var usuario = context.Usuario.Find(id);
                    context.Usuario.Remove(usuario);
                    context.Estudiante.Remove(eliminado);
                    context.SaveChanges();
                    return "Se eliminó correctamente";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public Estudiante BuscarPorId(string id)
        {

            using (var context = new NeuroGGestEntities())
            {
                Estudiante estudianteencontrado = context.Estudiante.Find(id);
                return estudianteencontrado;
            }

        }
    }
}
