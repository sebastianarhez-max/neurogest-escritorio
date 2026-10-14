using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DUsuario
    {
        public Usuario Logueo(Usuario objusuario)
        {

            using (var contexto = new NeuroGGestEntities())
            {
                var resultado = (from alguien in contexto.Usuario
                                 where alguien.login == objusuario.login
                                 && alguien.clave == objusuario.clave
                                 && alguien.activo == true
                                 select alguien).SingleOrDefault();
                return resultado;
            }
        }
    }
}
