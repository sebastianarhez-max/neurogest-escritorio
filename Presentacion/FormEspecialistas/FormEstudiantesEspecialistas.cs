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

namespace Presentacion.FormEspecialistas
{
    public partial class FormEstudiantesEspecialistas : Form
    {
        private NEstudiante nEstudiante = new NEstudiante();
        private NEspecialista nEspecialista = new NEspecialista();
        private NCita nCita = new NCita();

        private string loginEspecialista = FormLogin.usuarioLogin;
        private List<Estudiante> todosLosEstudiantes = new List<Estudiante>();
        private Especialista especialistaActual;
        public FormEstudiantesEspecialistas(Especialista especialistaActual)
        {
            InitializeComponent();
            this.especialistaActual = especialistaActual;
            MostrarDataGrid(nEstudiante.ListarTodo());
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");


        }

        private void FormEstudiantesEspecialistas_Load(object sender, EventArgs e)
        {
            lbPFnombre.Text = especialistaActual.nombre;
            CargarEstudiantes();
        }
        private void MostrarDataGrid(List<Estudiante> estudiantes)
        {
            dgEstudiantes.DataSource = null;
            if (estudiantes.Count > 0)
            {
                dgEstudiantes.DataSource = estudiantes;
            }
        }
        private void CargarEstudiantes()
        {
            try
            {
                List<string> idsEstudiantes = nCita.ListarTodo()
                    .Where(c => c.id_Especialista == loginEspecialista)
                    .Select(c => c.id_Estudiante)
                    .Distinct()
                    .ToList();

                todosLosEstudiantes = nEstudiante.ListarTodo()
                    .Where(est => idsEstudiantes.Contains(est.id))
                    .ToList();

                dgEstudiantes.DataSource = null;
                dgEstudiantes.DataSource = todosLosEstudiantes.Select(est => new
                {
                    Código = est.id,
                    Nombre = est.nombre,
                    Grado = est.gradoAcademico,
                    Apoderado = est.contactoApod
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estudiantes: " + ex.Message);
            }
        }

        private void AbrirFormulario(Form formulario)
        {
            formulario.Show();
            this.Hide();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FormDashboardEspecialista form = new FormDashboardEspecialista(especialistaActual);
            AbrirFormulario(form);
        }

        private void btnEstudiantes_Click(object sender, EventArgs e)
        {

        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            FormCitasEspecialistas form = new FormCitasEspecialistas(especialistaActual);
            AbrirFormulario(form);
        }

        private void btnEvaluaciones_Click(object sender, EventArgs e)
        {
            FormEvaluacionesEspecialistas form = new FormEvaluacionesEspecialistas(especialistaActual);
            AbrirFormulario(form);
        }

        private void btnTalleres_Click(object sender, EventArgs e)
        {
            FormTalleresEspecialistas form = new FormTalleresEspecialistas(especialistaActual);
            AbrirFormulario(form);
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            if (dgEstudiantes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un estudiante para ver su historial.");
                return;
            }
            string idEstudiante = dgEstudiantes.SelectedRows[0].Cells["Código"].Value.ToString();
            FormHistorialEstudiantesEspecialista form = new FormHistorialEstudiantesEspecialista(especialistaActual, idEstudiante);
            AbrirFormulario(form);
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            FormPerfilEspecialista form = new FormPerfilEspecialista(especialistaActual);
            AbrirFormulario(form);
        }
    }
}
