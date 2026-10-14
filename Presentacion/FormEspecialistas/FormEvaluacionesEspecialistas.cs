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
    public partial class FormEvaluacionesEspecialistas : Form
    {
        private NEvaluacion nEvaluacion = new NEvaluacion();
        private NEspecialista nEspecialista = new NEspecialista();
        private NEstudiante nEstudiante = new NEstudiante();
        private NCita nCita = new NCita();

        private string loginEspecialista = FormLogin.usuarioLogin;
        private List<Evaluacion> todasLasEvaluaciones = new List<Evaluacion>();
        private List<Cita> citasDelEspecialista = new List<Cita>();
        private Especialista especialistaActual;
        public FormEvaluacionesEspecialistas(Especialista especialistaActual)
        {
            InitializeComponent();
            this.especialistaActual = especialistaActual;

            Cargarcmbestudiantes(nEstudiante.ListarTodo());
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");

        }
        private void Cargarcmbestudiantes(List<Estudiante> estudiantes)
        {
            cmbEstudiante.DataSource = null;
            if (estudiantes.Count > 0)
            {
                cmbEstudiante.DataSource = estudiantes;
                cmbEstudiante.DisplayMember = "nombre";
                cmbEstudiante.ValueMember = "id";
            }
        }
        private void CargarEstudiantesCombo()
        {
            List<string> idsEstudiantes = citasDelEspecialista
                .Select(c => c.id_Estudiante)
                .Distinct()
                .ToList();

            List<Estudiante> estudiantes = nEstudiante.ListarTodo()
                .Where(est => idsEstudiantes.Contains(est.id))
                .ToList();

            cmbEstudiante.DataSource = null;
            cmbEstudiante.DataSource = estudiantes;
            cmbEstudiante.DisplayMember = "nombre";
            cmbEstudiante.ValueMember = "id";
        }

        private void FormEvaluacionesEspecialistas_Load(object sender, EventArgs e)
        {
            dgEvaluaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgEvaluaciones.MultiSelect = false;

            lbPFnombre.Text = especialistaActual.nombre;

            MostrarEnGrid(nEvaluacion.ListarTodo().Where(esp => esp.Cita.id_Especialista == especialistaActual.id)
            .ToList());
        }

        private void CargarEvaluaciones()
        {
            dgEvaluaciones.DataSource = null;
            var evaluaciones = nEvaluacion.ListarTodo()
            .Where(e => e.Cita.id_Especialista == especialistaActual.id)
            .ToList();

            dgEvaluaciones.DataSource = evaluaciones;
        }

        private void MostrarEnGrid(List<Evaluacion> lista)
        {
            dgEvaluaciones.DataSource = null;
            dgEvaluaciones.DataSource = lista.Select(ev => new
            {
                ID = ev.id,
                Cita = ev.id_Cita,
                Estudiante = citasDelEspecialista
                    .Where(c => c.id == ev.id_Cita)
                    .Select(c => c.id_Estudiante)
                    .FirstOrDefault(),
                Observaciones = ev.observaciones,
                Resultados = ev.resultados,
                Recomendacion = ev.recomendacion
            }).ToList();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgEvaluaciones.DataSource = null;
            string idEstudiante = cmbEstudiante.SelectedValue.ToString();

            var evaluaciones = nEvaluacion.ListarTodo()
                .Where(ev => ev.Cita.id_Especialista == especialistaActual.id &&
                            ev.Cita.id_Estudiante == idEstudiante)
                .Select(ev => new
                {
                    ID = ev.id,
                    Fecha = ev.Cita.fecha,
                    Estudiante = ev.Cita.Estudiante.nombre,
                    Observaciones = ev.observaciones,
                    Resultados = ev.resultados,
                    Recomendacion = ev.recomendacion
                })
                .ToList();

            dgEvaluaciones.DataSource = evaluaciones;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dgEvaluaciones.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una evaluación de la lista para modificar.");
                return;
            }

            if (tbObservaciones.Text.Trim() == "" ||
                tbResultados.Text.Trim() == "" ||
                tbRecomendaciones.Text.Trim() == "")
            {
                MessageBox.Show("Complete todos los campos antes de guardar.");
                return;
            }

            try
            {
                int idEvaluacion = Convert.ToInt32(dgEvaluaciones.CurrentRow.Cells["ID"].Value);

                Evaluacion evaluacion = new Evaluacion();
                evaluacion.id = idEvaluacion;
                evaluacion.observaciones = tbObservaciones.Text.Trim();
                evaluacion.resultados = tbResultados.Text.Trim();
                evaluacion.recomendacion = tbRecomendaciones.Text.Trim();

                string mensaje = nEvaluacion.Modificar(evaluacion);

                if (mensaje == "Se modificó correctamente")
                {
                    MessageBox.Show("Evaluación modificada correctamente.");
                    tbObservaciones.Text = "";
                    tbResultados.Text = "";
                    tbRecomendaciones.Text = "";
                    MostrarEnGrid(nEvaluacion.ListarTodo().Where(esp => esp.Cita.id_Especialista == especialistaActual.id)
            .ToList());
                }
                else
                {
                    MessageBox.Show("No se pudo modificar: " + mensaje);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
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
        }

        private void btnTalleres_Click(object sender, EventArgs e)
        {
            FormTalleresEspecialistas form = new FormTalleresEspecialistas(especialistaActual   );
            AbrirFormulario(form);
        }

        private void dgEvaluaciones_SelectionChanged(object sender, EventArgs e)
        {
            if (dgEvaluaciones.SelectedRows.Count == 0) return;

            var fila = dgEvaluaciones.SelectedRows[0];
            tbObservaciones.Text = fila.Cells["Observaciones"].Value?.ToString();
            tbResultados.Text = fila.Cells["Resultados"].Value?.ToString();
            tbRecomendaciones.Text = fila.Cells["Recomendacion"].Value?.ToString();
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            FormPerfilEspecialista form = new FormPerfilEspecialista(especialistaActual);
            AbrirFormulario(form);
        }
    }
}
