using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NTaller
    {

        DTaller dTaller = new DTaller();

        public string Registrar(Taller taller)
        {
            return dTaller.Registrar(taller);
        }

        public List<Taller> ListarTodo()
        {
            return dTaller.ListarTodo();
        }

        public string Modificar(Taller taller)
        {
            return dTaller.Modificar(taller);
        }

        public string Eliminar(string id)
        {
            return dTaller.Eliminar(id);
        }

        public Taller BuscarPorId(string id)
        {
           return dTaller.BuscarPorId(id);
        }
    }
}
