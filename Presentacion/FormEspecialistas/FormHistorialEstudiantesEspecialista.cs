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
    public partial class FormHistorialEstudiantesEspecialista : Form
    {
        private NEstudiante nEstudiante = new NEstudiante();
        private NCita nCita = new NCita();
        private NEvaluacion nEvaluacion = new NEvaluacion();
        private NEspecialista nEspecialista = new NEspecialista();

        private string loginEspecialista = FormLogin.usuarioLogin;
        private string idEstudianteSeleccionado;

        public string IdEstudiante { get; set; }
        private Especialista especialistaActual;
        public FormHistorialEstudiantesEspecialista(Especialista especialistaActual, string idEstudiante) 
        {
            InitializeComponent();
            this.especialistaActual = especialistaActual;
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");
            idEstudianteSeleccionado = idEstudiante;
        }
        private void FormHistorialEstudiantesEspecialista_Load(object sender, EventArgs e)
        {
            var especialista = nEspecialista.ListarTodo()
                .FirstOrDefault(esp => esp.id == loginEspecialista);
            if (especialista != null)
            {
                lbPFnombre.Text = especialista.nombre;
            }
            CargarHistorial();
        }

        private void CargarHistorial()
        {
            try
            {
                lbEstudiante.Items.Clear();

                if (idEstudianteSeleccionado == null || idEstudianteSeleccionado == "")
                {
                    lbEstudiante.Items.Add("No se seleccionó ningún estudiante.");
                    return;
                }

                var estudiante = nEstudiante.ListarTodo()
                    .FirstOrDefault(est => est.id == idEstudianteSeleccionado);

                if (estudiante == null)
                {
                    lbEstudiante.Items.Add("Estudiante no encontrado.");
                    return;
                }

                lbEstudiante.Items.Add("=== DATOS DEL ESTUDIANTE ===");
                lbEstudiante.Items.Add("Código: " + estudiante.id);
                lbEstudiante.Items.Add("Nombre: " + estudiante.nombre);
                lbEstudiante.Items.Add("Grado: " + estudiante.gradoAcademico);
                lbEstudiante.Items.Add("Apoderado: " + estudiante.contactoApod);
                lbEstudiante.Items.Add("");

                var citas = nCita.ListarTodo()
                    .Where(c => c.id_Estudiante == idEstudianteSeleccionado
                             && c.id_Especialista == loginEspecialista)
                    .ToList();

                lbEstudiante.Items.Add("=== HISTORIAL DE CITAS ===");

                if (citas.Count == 0)
                {
                    lbEstudiante.Items.Add("Sin citas registradas con este especialista.");
                }
                else
                {
                    foreach (var cita in citas)
                    {
                        lbEstudiante.Items.Add("Cita: " + cita.id);
                        lbEstudiante.Items.Add("Fecha: " + cita.fecha.ToString());
                        lbEstudiante.Items.Add("Hora: " + cita.hora.ToString());
                        lbEstudiante.Items.Add("Estado: " + cita.estado);
                        lbEstudiante.Items.Add("");
                    }
                }

                var evaluaciones = nEvaluacion.ListarTodo()
                    .Where(ev => citas.Any(c => c.id == ev.id_Cita))
                    .ToList();

                lbEstudiante.Items.Add("=== EVALUACIONES ===");

                if (evaluaciones.Count == 0)
                {
                    lbEstudiante.Items.Add("Sin evaluaciones registradas.");
                }
                else
                {
                    foreach (var ev in evaluaciones)
                    {
                        lbEstudiante.Items.Add("Cita: " + ev.id_Cita);
                        lbEstudiante.Items.Add("Observaciones: " + ev.observaciones);
                        lbEstudiante.Items.Add("Resultados: " + ev.resultados);
                        lbEstudiante.Items.Add("Recomendación: " + ev.recomendacion);
                        lbEstudiante.Items.Add("");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial: " + ex.Message);
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

        private void lbEstudiante_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            FormPerfilEspecialista form = new FormPerfilEspecialista(especialistaActual );
            AbrirFormulario(form);
        }
    }
}
