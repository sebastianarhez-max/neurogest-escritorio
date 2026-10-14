using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DEspecialista
    {
        public string Registrar(Especialista especialista)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    string ultimoId = context.Especialista
                                     .OrderByDescending(e => e.id)
                                     .Select(e => e.id)
                                     .FirstOrDefault();
                    int numero = int.Parse(ultimoId.Substring(3));
                    especialista.id = "ESP" + (numero + 1).ToString("D3");
                    Usuario usuario = new Usuario()
                    {
                        login = especialista.id,
                        clave = (numero+1).ToString(),
                        rol = "Especialista",
                        activo = true
                    };
                    if (context.Especialista.Any(e => e.id == especialista.id))
                    {
                        return "El especialista ya existe";
                    }
                    context.Usuario.Add(usuario);
                    context.Especialista.Add(especialista);
                    context.SaveChanges();
                    return "Registro correcto";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public List<Especialista> ListarTodo()
        {
            using (var context = new NeuroGGestEntities())
            {
                return context.Especialista.ToList();
            }
        }

        public string Modificar(Especialista especialista)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    var modificado = context.Especialista.Find(especialista.id);
                    if (modificado == null)
                    {
                        return "No se encontró el especialista";
                    }
                    modificado.nombre = especialista.nombre;
                    modificado.especialidad = especialista.especialidad;
                    modificado.modificadoPor = especialista.modificadoPor;
                    modificado.fechaModific = DateTime.Now;
                    modificado.id= especialista.id;
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
                    var eliminado = context.Especialista.Find(id);
                    if (eliminado == null)
                    {
                        return "No se encontró el especialista";
                    }
                    var usuario = context.Usuario.Find(id);
                    context.Usuario.Remove(usuario);
                    context.Especialista.Remove(eliminado);
                    context.SaveChanges();
                    return "Se eliminó correctamente";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Especialista BuscarPorId(string id)
        {

            using (var context = new NeuroGGestEntities())
            {
                Especialista especialistaencontrado = context.Especialista.Find(id);
                return especialistaencontrado;
            }

        }

    }
}
