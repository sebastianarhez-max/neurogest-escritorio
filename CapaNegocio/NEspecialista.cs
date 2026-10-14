using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NEspecialista
    {

        DEspecialista dEspecialista = new DEspecialista();

        public string Registrar(Especialista especialista)
        {
            return dEspecialista.Registrar(especialista);
        }

        public List<Especialista> ListarTodo()
        {
            return dEspecialista.ListarTodo();
        }

        public string Modificar(Especialista especialista)
        {
            return dEspecialista.Modificar(especialista);
        }

        public string Eliminar(string id)
        {
            return dEspecialista.Eliminar(id);
        }

        public Especialista BuscarPorId(string id)
        {

            return dEspecialista.BuscarPorId(id);   

        }
    }
}
