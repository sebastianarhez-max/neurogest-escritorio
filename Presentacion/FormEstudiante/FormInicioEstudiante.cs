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

namespace Presentacion.FormEstudiante
{
    public partial class FormInicioEstudiante : Form
    {
        private NCita nCita = new NCita();
        private NEvaluacion nEvaluacion = new NEvaluacion();
        private NTaller nTaller = new NTaller();
        private NEstudiante nEstudiante = new NEstudiante();
        private NInscripcion_Taller nInscripcion = new NInscripcion_Taller();

        private string idEstudianteLogueado = FormLogin.usuarioLogin;
        private Estudiante estudianteActual;

        public FormInicioEstudiante(Estudiante estudianteActual)
        {
            InitializeComponent();
            this.estudianteActual = estudianteActual;
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");
        }
        private void CargarDashboardEstudiante()
        {
            try
            {
                Estudiante estudiante = nEstudiante.ListarTodo()
                    .FirstOrDefault(est => est.id == idEstudianteLogueado);

                if (estudiante != null)
                {
                    lbestudiantenombre.Text = "Bienvenida, " + estudiante.nombre;
                    lbNombreEstudiante.Text = estudiante.nombre;
                }

                List<Cita> misCitas = nCita.ListarTodo()
                    .Where(c => c.id_Estudiante == idEstudianteLogueado)
                    .ToList();

                List<Evaluacion> misEvaluaciones = nEvaluacion.ListarTodo()
                    .Where(ev => misCitas.Any(c => c.id == ev.id_Cita))
                    .ToList();

                List<Taller> todosLosTalleres = nTaller.ListarTodo();

                List<Inscripcion_Taller> misTalleresAsistidos = nInscripcion.ListarTodo()
                    .Where(i => i.id_Estudiante == idEstudianteLogueado && i.asistencia == "Asistió")
                    .ToList();

                lbcitas.Text = misCitas.Count.ToString();
                lblEvaluaciones.Text = misEvaluaciones.Count.ToString();
                lblTalleresAsistidos.Text = misTalleresAsistidos.Count.ToString();

                CargarProximaCita(misCitas);
                CargarEvaluacionesRecientes(misEvaluaciones);
                CargarTalleresDisponibles(todosLosTalleres);
                CargarUltimaEvaluacion(misEvaluaciones);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el menú de inicio: " + ex.Message);
            }
        }

        private void CargarProximaCita(List<Cita> misCitas)
        {
            lvProximaCita.Items.Clear();

            List<Cita> citasProgramadas = misCitas
                .Where(c => c.estado == "Programada")
                .OrderBy(c => c.fecha)
                .ToList();

            foreach (Cita cita in citasProgramadas)
            {
                ListViewItem item = new ListViewItem(cita.id);
                item.SubItems.Add(cita.fecha.ToString());
                item.SubItems.Add(cita.hora.ToString());
                item.SubItems.Add(cita.id_Especialista);

                lvProximaCita.Items.Add(item);
            }
        }

        private void CargarEvaluacionesRecientes(List<Evaluacion> misEvaluaciones)
        {
            dgEvaluacionesRecientes.DataSource = null;
            dgEvaluacionesRecientes.DataSource = misEvaluaciones
                .Select(ev => new
                {
                    Código = ev.id,
                    Cita = ev.id_Cita,
                    Resultados = ev.resultados
                }).ToList();
        }


        private void CargarTalleresDisponibles(List<Taller> talleres)
        {
            dgtalleresdisponibles.DataSource = null;
            dgtalleresdisponibles.DataSource = talleres
                .Select(t => new
                {
                    Taller = t.nombre,
                    Fecha = t.fechaTaller.ToString(),
                    Capacidad = t.ocupacionMaxima
                }).ToList();
        }
        private void CargarUltimaEvaluacion(List<Evaluacion> misEvaluaciones)
        {
            lvUltimaEvaluacion.Items.Clear();

            Evaluacion ultimaEvaluacion = misEvaluaciones
                .OrderBy(ev => ev.id)
                .LastOrDefault();

            if (ultimaEvaluacion != null)
            {
                ListViewItem item = new ListViewItem(ultimaEvaluacion.id.ToString());
                item.SubItems.Add(ultimaEvaluacion.observaciones);
                item.SubItems.Add(ultimaEvaluacion.recomendacion);

                lvUltimaEvaluacion.Items.Add(item);
            }
        }

        private void AbrirFormulario(Form formulario)
        {
            formulario.Show();
            this.Hide();
        }

        private void lbestudiantes_Click(object sender, EventArgs e) { }

        private void FormInicioEstudiante_Load_1(object sender, EventArgs e)
        {
            ConfigurarListViews();
            CargarDashboardEstudiante();
            lbNombreEstudiante.Text = estudianteActual.nombre;
        }
        private void ConfigurarListViews()
        {
            lvProximaCita.View = View.Details;
            lvProximaCita.FullRowSelect = true;
            lvProximaCita.GridLines = true;
            lvProximaCita.Columns.Clear();
            lvProximaCita.Columns.Add("Código", 80);
            lvProximaCita.Columns.Add("Fecha", 100);
            lvProximaCita.Columns.Add("Hora", 80);
            lvProximaCita.Columns.Add("Especialista", 100);

            lvUltimaEvaluacion.View = View.Details;
            lvUltimaEvaluacion.FullRowSelect = true;
            lvUltimaEvaluacion.GridLines = true;
            lvUltimaEvaluacion.Columns.Clear();
            lvUltimaEvaluacion.Columns.Add("Código", 80);
            lvUltimaEvaluacion.Columns.Add("Observación", 180);
            lvUltimaEvaluacion.Columns.Add("Recomendación", 180);
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void lbcitas_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Form formLogin = Application.OpenForms["FormLogin"];
            if (formLogin != null)
            {
                formLogin.Show();
            }
            this.Close();
        }

        private void btnSoliticarCita2_Click(object sender, EventArgs e)
        {
            FormMisCitas form = new FormMisCitas(estudianteActual);
            AbrirFormulario(form);
        }
        private void btnCitas_Click(object sender, EventArgs e)
        {
            FormMisCitas form = new FormMisCitas(estudianteActual);
            AbrirFormulario(form);
        }

        private void btnEvaluaciones_Click(object sender, EventArgs e)
        {
            FormEvaluacionesEstudiante form = new FormEvaluacionesEstudiante(estudianteActual);
            AbrirFormulario(form);
        }

        private void btnTalleres_Click(object sender, EventArgs e)
        {
            FormTallleresEstudiante form = new FormTallleresEstudiante(estudianteActual);
            AbrirFormulario(form);
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            FormPerfilEstudiante form = new FormPerfilEstudiante(estudianteActual);
            AbrirFormulario(form);
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            CargarDashboardEstudiante();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }
    }
}