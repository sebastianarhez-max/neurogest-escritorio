using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaDatos;

namespace Presentacion.FormAdmins
{
    public partial class FormDashboardAdmin : Form
    {
        private NEstudiante nEstudiante = new NEstudiante();
        private NCita nCita = new NCita();
        private NEvaluacion nEvaluacion = new NEvaluacion();
        private NTaller nTaller = new NTaller();
        private NEspecialista nEspecialista = new NEspecialista();

        public FormDashboardAdmin()
        {
            InitializeComponent();
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");
        }

        private void CargarEstadisticasDashboard()
        {
            try
            {
                List<Estudiante> estudiantes = nEstudiante.ListarTodo();
                List<Cita> todasLasCitas = nCita.ListarTodo();
                List<Evaluacion> evaluaciones = nEvaluacion.ListarTodo();
                List<Taller> talleres = nTaller.ListarTodo();
                List<Especialista> especialistas = nEspecialista.ListarTodo();

                lbestudiantes.Text = estudiantes.Count.ToString();
                label17.Text = evaluaciones.Count.ToString();
                label20.Text = talleres.Count.ToString();

                DateTime hoy = DateTime.Today;

                List<Cita> citasDeHoy = todasLasCitas
                    .Where(c => c.fecha != null && c.fecha.Value.Date == hoy)
                    .ToList();

                lbcitas.Text = citasDeHoy.Count.ToString();

                dgdasboardcitas.DataSource = null;
                dgdasboardcitas.DataSource = citasDeHoy.Select(c => new
                {
                    Código = c.id,
                    Estudiante = c.id_Estudiante,
                    Especialista = c.id_Especialista,
                    Hora = c.hora,
                    Estado = c.estado
                }).ToList();

                dgcitasestado.DataSource = null;
                dgcitasestado.DataSource = todasLasCitas
                    .GroupBy(c => c.estado)
                    .Select(g => new
                    {
                        Estado = g.Key,
                        Cantidad = g.Count()
                    }).ToList();

                dgespecialistasactivos.DataSource = null;
                dgespecialistasactivos.DataSource = especialistas.Select(esp => new
                {
                    Código = esp.id,
                    Nombre = esp.nombre,
                    Especialidad = esp.especialidad
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los datos del Dashboard: " + ex.Message);
            }
        }

        private void AbrirFormulario(Form formulario)
        {
            formulario.Show();
            this.Hide();
        }

        private void lbestudiantes_Click(object sender, EventArgs e) 
        { 
        }

        private void FormDashboardAdmin_Load(object sender, EventArgs e)
        {
            CargarEstadisticasDashboard();
        }

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            CargarEstadisticasDashboard();
        }

        private void btnEstudiantes_Click_1(object sender, EventArgs e)
        {
            FormEstudiantesAdmin form = new FormEstudiantesAdmin();
            AbrirFormulario(form);
        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            FormCitaAdmin form = new FormCitaAdmin();
            AbrirFormulario(form);
        }

        private void btnEvaluaciones_Click(object sender, EventArgs e)
        {
            FormEvaluacionesAdmin form = new FormEvaluacionesAdmin();
            AbrirFormulario(form);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FormTalleresAdmin form = new FormTalleresAdmin();
            AbrirFormulario(form);
        }

        private void btnEspecialistas_Click(object sender, EventArgs e)
        {
            FormEspecialistasAdmin form = new FormEspecialistasAdmin();
            AbrirFormulario(form);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            FormReportesAdmin form = new FormReportesAdmin();
            AbrirFormulario(form);
        }

        private void BT_REGRESA_Click_1(object sender, EventArgs e)
        {
            Form formLogin = Application.OpenForms["FormLogin"];
            if (formLogin != null) formLogin.Show();
            
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }
    }
}