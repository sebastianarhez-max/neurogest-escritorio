using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NUsuario
    {
        DUsuario objDataUsuario = new DUsuario();
        public Usuario Logueo(Usuario objusuario)
        {
            return objDataUsuario.Logueo(objusuario);
        }
    }
}
