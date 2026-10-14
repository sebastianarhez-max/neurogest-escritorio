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
    public partial class FormDashboardEspecialista : Form
    {
        private NEstudiante nEstudiante = new NEstudiante();
        private NCita nCita = new NCita();
        private NEvaluacion nEvaluacion = new NEvaluacion();
        private NTaller nTaller = new NTaller();
        private NEspecialista nEspecialista = new NEspecialista();

        private string loginEspecialista = FormLogin.usuarioLogin;
        private Especialista especialistaActual;
        public FormDashboardEspecialista(Especialista especialistaActual)
        {
            InitializeComponent();
            this.especialistaActual = especialistaActual;
            EvaluacionesPendientesRegistrar();
            CargarIndicadores();
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");
        }

        private void FormDashboardEspecialista_Load(object sender, EventArgs e)
        {
            CargarDashboard();
            lbPFnombre.Text = especialistaActual.nombre;
        }

        private void CargarDashboard()
        {
            try
            {
                var especialista = nEspecialista.ListarTodo()
                    .FirstOrDefault(esp => esp.id == loginEspecialista);

                if (especialista != null)
                {
                    lbnombredoctor.Text = especialista.nombre;
                    lbPFnombre.Text = especialista.nombre;
                }

                lbestudiantes.Text = nEstudiante.ListarTodo().Count.ToString();
                lbEvaluacionesPendientesRegistrar.Text = nEvaluacion.ListarTodo().Count.ToString();
                lbtalleres.Text = nTaller.ListarTodo().Count.ToString();

                DateTime hoy = DateTime.Today;
                List<Cita> todasLasCitas = nCita.ListarTodo();

                List<Cita> citasDeHoy = todasLasCitas
                    .Where(c => c.fecha != null
                             && c.fecha.Value.Date == hoy
                             && c.id_Especialista == loginEspecialista && c.estado=="Programada")
                    .ToList();

                lbcitas.Text = citasDeHoy.Count.ToString();

                dgagenda.DataSource = null;
                dgagenda.DataSource = citasDeHoy.Select(c => new
                {
                    Código = c.id,
                    Estudiante = c.id_Estudiante,
                    Hora = c.hora.ToString(),
                    Estado = c.estado
                }).ToList();

                List<Evaluacion> evaluaciones = nEvaluacion.ListarTodo();
                List<string> citasConEvaluacion = evaluaciones
                    .Where(ev => ev.id_Cita != null)
                    .Select(ev => ev.id_Cita)
                    .ToList();

                List<Cita> citasPendientesEval = todasLasCitas
                    .Where(c => c.estado == "Realizada"
                             && c.id_Especialista == loginEspecialista
                             && !citasConEvaluacion.Contains(c.id))
                    .ToList();

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el dashboard: " + ex.Message);
            }
        }

        private void AbrirFormulario(Form formulario)
        {
            formulario.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void dgevaluacionespendientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Form formLogin = Application.OpenForms["FormLogin"];
            if (formLogin != null) formLogin.Show();
            this.Close();
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
            FormEvaluacionesEspecialistas form = new FormEvaluacionesEspecialistas(especialistaActual);
            AbrirFormulario(form);
        }

        private void btnTalleres_Click(object sender, EventArgs e)
        {
            FormTalleresEspecialistas form = new FormTalleresEspecialistas(especialistaActual);
            AbrirFormulario(form);
        }

        private void btnRegistrarAhora_Click(object sender, EventArgs e)
        {
            if (dgevaluacionespendientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una cita de la lista para registrar su evaluación.");
                return;
            }

            FormEvaluacionesEspecialistas form = new FormEvaluacionesEspecialistas(especialistaActual);
            AbrirFormulario(form);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            CargarDashboard();
        }

        private void btnEvaluacion_Click(object sender, EventArgs e)
        {
            FormEvaluacionesEspecialistas form = new FormEvaluacionesEspecialistas(especialistaActual);
            AbrirFormulario(form);
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            FormPerfilEspecialista form = new FormPerfilEspecialista(especialistaActual);
            AbrirFormulario(form);
        }

        private void EvaluacionesPendientesRegistrar()
        {
            dgevaluacionespendientes.DataSource = null;
            
            var evaluaciones = nEvaluacion.ListarTodo()
        .Where(e => e.Cita.id_Especialista == especialistaActual.id &&
               (string.IsNullOrWhiteSpace(e.observaciones) ||
                string.IsNullOrWhiteSpace(e.resultados) ||
                string.IsNullOrWhiteSpace(e.recomendacion)))
        .Select(e => new
        {
            IdEvaluacion = e.id,
            Fecha = e.Cita.fecha,
            Estudiante = e.Cita.Estudiante.nombre
        })
        .ToList();

            dgevaluacionespendientes.DataSource = evaluaciones;
        }
        private void CargarIndicadores()
        {
            // Total de estudiantes registrados
            lbestudiantes.Text = nEstudiante.ListarTodo().Count.ToString();

            // Citas pendientes (Programadas) del especialista
            lbcitas.Text = nCita.ListarTodo()
                .Count(c => c.id_Especialista == especialistaActual.id &&
                            c.estado == "Programada")
                .ToString();

            // Evaluaciones pendientes del especialista
            lbEvaluacionesPendientesRegistrar.Text = nEvaluacion.ListarTodo()
                .Count(e => e.Cita.id_Especialista == especialistaActual.id &&
                       (string.IsNullOrWhiteSpace(e.observaciones) ||
                        string.IsNullOrWhiteSpace(e.resultados) ||
                        string.IsNullOrWhiteSpace(e.recomendacion)))
                .ToString();

            // Total de talleres registrados
            lbtalleres.Text = nTaller.ListarTodo().Count.ToString();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }
    }
}
