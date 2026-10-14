using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DTaller
    {
        public string Registrar(Taller taller)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    context.Taller.Add(taller);
                    context.SaveChanges();
                    return "Registro correcto";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Taller> ListarTodo()
        {
            using (var context = new NeuroGGestEntities())
            {
                return context.Taller.ToList();
            }
        }

        public string Modificar(Taller taller)
        {
            try
            {
                using (var context = new NeuroGGestEntities())
                {
                    var modificado = context.Taller.Find(taller.id);
                    if (modificado == null)
                    {
                        return "No se encontró el taller";
                    }
                    modificado.nombre = taller.nombre;
                    modificado.tipoTaller = taller.tipoTaller;
                    modificado.ocupacionMaxima = taller.ocupacionMaxima;
                    modificado.fechaTaller = taller.fechaTaller;
                    modificado.modificadoPor = taller.modificadoPor;
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
                    var eliminado = context.Taller.Find(id);
                    if (eliminado == null)
                    {
                        return "No se encontró el taller";
                    }
                    context.Taller.Remove(eliminado);
                    context.SaveChanges();
                    return "Se eliminó correctamente";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Taller BuscarPorId(string id)
        {
            using (var context = new NeuroGGestEntities())
            {
                return context.Taller.FirstOrDefault(t => t.id == id);
            }
        }

    }
}
