using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos;
using CapaNegocio;

namespace Presentacion.FormEstudiante
{
    public partial class FormEvaluacionesEstudiante : Form
    {
        private NEvaluacion nEvaluacion = new NEvaluacion();
        private NEspecialista nEspecialista = new NEspecialista();
        private NCita nCita = new NCita();

        private string idEstudianteLogueado = FormLogin.usuarioLogin;

        private List<Evaluacion> misEvaluaciones = new List<Evaluacion>();
        private List<Cita> misCitas = new List<Cita>();
        private Estudiante estudianteActual;
        public FormEvaluacionesEstudiante(Estudiante estudianteActual)
        {
            InitializeComponent();
            this.estudianteActual = estudianteActual;
            CargarEspecialistas(nEspecialista.ListarTodo());
        }

        private void AbrirFormulario(Form formulario)
        {
            formulario.Show();
            this.Hide();
        }
        private void CargarEspecialistas(List<Especialista> especialistas)
        {
            if (especialistas.Count > 0)
            {
                cmbEspecialista.DataSource = null;
                cmbEspecialista.DataSource = especialistas;
                cmbEspecialista.ValueMember = "id";
                cmbEspecialista.DisplayMember = "nombre";
            }
        }

        private void CargarEvaluaciones()
        {
            try
            {
                misCitas = nCita.ListarTodo()
                    .Where(c => c.id_Estudiante == idEstudianteLogueado)
                    .ToList();

                List<string> idsCitas = misCitas
                    .Select(c => c.id)
                    .ToList();

                misEvaluaciones = nEvaluacion.ListarTodo()
                    .Where(ev => idsCitas.Contains(ev.id_Cita))
                    .ToList();

                MostrarEnGrid(misEvaluaciones);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar evaluaciones: " + ex.Message);
            }
        }
        private void MostrarEnGrid(List<Evaluacion> lista)
        {
            dgEvaluaciones.DataSource = null;
            dgEvaluaciones.DataSource = lista.Select(ev => new
            {
                ID = ev.id,
                Cita = ev.id_Cita,
                Especialista = misCitas
                    .Where(c => c.id == ev.id_Cita)
                    .Select(c => c.id_Especialista)
                    .FirstOrDefault(),
                Observaciones = ev.observaciones,
                Resultados = ev.resultados,
                Recomendacion = ev.recomendacion
            }).ToList();
        }
        private void btnMisEvaluaciones_Click(object sender, EventArgs e)
        {
            CargarEvaluaciones();
        }
        
        private void btnBuscarPorEspecialista_Click(object sender, EventArgs e)
        {
            if (cmbEspecialista.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un especialista");
                return;
            }

            string idEspecialista = cmbEspecialista.SelectedValue.ToString();

            List<string> idsCitasEspecialista = misCitas
                .Where(c => c.id_Especialista == idEspecialista)
                .Select(c => c.id)
                .ToList();

            List<Evaluacion> filtradas = misEvaluaciones
                .Where(ev => idsCitasEspecialista.Contains(ev.id_Cita))
                .ToList();

            MostrarEnGrid(filtradas);
        }

        private void btnTalleres_Click(object sender, EventArgs e)
        {
            FormTallleresEstudiante form = new FormTallleresEstudiante( estudianteActual);
            AbrirFormulario(form);
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            FormInicioEstudiante form = new FormInicioEstudiante(estudianteActual);
            AbrirFormulario(form);
        }
        private void btnCitas_Click(object sender, EventArgs e)
        {
            FormMisCitas form = new FormMisCitas(estudianteActual);
            AbrirFormulario(form);
        }

        private void btnEvaluaciones_Click(object sender, EventArgs e)
        {
            CargarEvaluaciones();
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            FormPerfilEstudiante form = new FormPerfilEstudiante(estudianteActual);
            AbrirFormulario(form);
        }

        private void FormEvaluacionesEstudiante_Load(object sender, EventArgs e)
        {
            dgEvaluaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgEvaluaciones.MultiSelect = false;
            lbPFnombre.Text = estudianteActual.nombre;
            CargarEvaluaciones();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormPerfilEstudiante form = new FormPerfilEstudiante(estudianteActual);
            AbrirFormulario(form);
        }
    }
}