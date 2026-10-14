using System;
using CapaDatos;
using CapaNegocio;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.FormEspecialistas
{
    public partial class FormPerfilEspecialista : Form
    {
        private NEspecialista nEspecialista = new NEspecialista();

        private string loginEspecialista = FormLogin.usuarioLogin;
        private Especialista especialistaActual;
        public FormPerfilEspecialista(Especialista especialistaActual)
        {
            InitializeComponent();
            this.especialistaActual = especialistaActual;
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");

        }

        private void CargarPerfil()
        {
            var especialista = nEspecialista.ListarTodo()
                .FirstOrDefault(esp => esp.id == loginEspecialista);

            if (especialista == null)
            {
                MessageBox.Show("No se encontró el especialista logueado.");
                return;
            }

            lbPFnombre.Text = especialista.nombre;

            lbUsuarioPerfil.Items.Clear();
            lbUsuarioPerfil.Items.Add("=== PERFIL DEL ESPECIALISTA ===");
            lbUsuarioPerfil.Items.Add("Código: " + especialista.id);
            lbUsuarioPerfil.Items.Add("Nombre: " + especialista.nombre);
            lbUsuarioPerfil.Items.Add("Especialidad: " + especialista.especialidad);
            lbUsuarioPerfil.Items.Add("");
            lbUsuarioPerfil.Items.Add("=== AUDITORÍA ===");
            lbUsuarioPerfil.Items.Add("Creado por: " + especialista.creadoPor);
            lbUsuarioPerfil.Items.Add("Fecha creación: " + especialista.fechaCreacion);
            lbUsuarioPerfil.Items.Add("Modificado por: " + especialista.modificadoPor);
            lbUsuarioPerfil.Items.Add("Fecha modificación: " + especialista.fechaModific);

            tbnombre.Text = especialista.nombre;
            tbespecialidad.Text = especialista.especialidad;
        }

        private void btnEditarInformacion_Click(object sender, EventArgs e)
        {
            if (tbnombre.Text.Trim() == "" || tbespecialidad.Text.Trim() == "")
            {
                MessageBox.Show("Complete todos los campos.");
                return;
            }

            Especialista especialista = new Especialista();

            especialista.id = loginEspecialista;
            especialista.nombre = tbnombre.Text.Trim();
            especialista.especialidad = tbespecialidad.Text.Trim();
            especialista.modificadoPor = loginEspecialista;
            especialista.fechaModific = DateTime.Now;

            MessageBox.Show(nEspecialista.Modificar(especialista));

            CargarPerfil();
        }

        private void FormPerfilEspecialista_Load(object sender, EventArgs e)
        {
            lbPFnombre.Text = especialistaActual.nombre;
            CargarPerfil();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            FormDashboardEspecialista form = new FormDashboardEspecialista(especialistaActual);
            form.Show();
            this.Hide();
        }
        private void AbrirFormulario(Form formulario)
        {
            formulario.Show();
            this.Hide();
        }
        private void btnPerfil_Click(object sender, EventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FormDashboardEspecialista form = new FormDashboardEspecialista(especialistaActual);
            AbrirFormulario(form);
        }

        private void btnEstudiantes_Click(object sender, EventArgs e)
        {
            FormEstudiantesEspecialistas form = new FormEstudiantesEspecialistas(especialistaActual);
            AbrirFormulario(form);
        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            FormCitasEspecialistas form = new FormCitasEspecialistas(especialistaActual);
            AbrirFormulario(form);
        }

        private void btnEvaluaciones_Click(object sender, EventArgs e)
        {
            FormEvaluacionesEspecialistas form = new FormEvaluacionesEspecialistas(especialistaActual   );
            AbrirFormulario(form);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormDashboardEspecialista form = new FormDashboardEspecialista(especialistaActual);
            AbrirFormulario(form);
        }
    }
}
