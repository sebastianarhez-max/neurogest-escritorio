using CapaDatos;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Presentacion.FormAdmins;
using Presentacion.FormEspecialistas;
using Presentacion.FormEstudiante;

namespace Presentacion
{
    public partial class FormLogin : Form
    {
        NUsuario objNegUsuario = new NUsuario();
        NEstudiante nEstudiante = new NEstudiante();
        NEspecialista NEspecialista = new NEspecialista();
        public static string usuarioLogin;
        public static string usuarioRol;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {

            if (ValidarLogin())
            {
                Usuario objUsuario = new Usuario();
                objUsuario.login = txbUsuario.Text.Trim();
                objUsuario.clave = txbContraseña.Text.Trim();
                var res = objNegUsuario.Logueo(objUsuario);

                if (res == null)
                {
                    MessageBox.Show("Error, datos de logueo no validos");
                    return;
                }
                else
                {
                    usuarioLogin = res.login;
                    usuarioRol = res.rol;
                    FileLogger objlogger = new FileLogger();
                    objlogger.Log(" Usuario : " + usuarioLogin
                                 + " Fecha : " + DateTime.Now.ToShortDateString()
                                 + " Hora : " + DateTime.Now.ToLongTimeString());
                    if (usuarioRol == "Administrador")
                    {
                        FormDashboardAdmin frmadmin = new FormDashboardAdmin();
                        this.Hide();
                        frmadmin.ShowDialog();
                        this.Show();
                    }
                    else if (usuarioRol == "Estudiante")
                    {
                        Estudiante estudiante = nEstudiante.BuscarPorId(objUsuario.login);

                        FormInicioEstudiante frm = new FormInicioEstudiante(estudiante);
                        frm.Show();
                        this.Show();
                    }
                    else if(usuarioRol == "Especialista")
                    {
                        Especialista especialista = NEspecialista.BuscarPorId(objUsuario.login);

                        FormDashboardEspecialista frmespecialista = new FormDashboardEspecialista( especialista);

                        frmespecialista.Show();
                        this.Show();
                    }

                    
                }
            }
        }

        bool ValidarLogin()
        {
            if (txbUsuario.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese un Usuario valido");
                return false;
            }
            else
            {
                if (txbContraseña.Text.Trim() == "")
                {
                    MessageBox.Show("Ingrese una Contraseña valida");
                    return false;
                }

            }
            return true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

