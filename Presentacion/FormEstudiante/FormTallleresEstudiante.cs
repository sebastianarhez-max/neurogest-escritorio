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

namespace Presentacion.FormEstudiante
{
    public partial class FormTallleresEstudiante : Form
    {
        NEstudiante nEstudiante = new NEstudiante();
        NTaller nTaller = new NTaller();
        NInscripcion_Taller nInscripcion = new NInscripcion_Taller();

        private string idEstudianteLogueado = FormLogin.usuarioLogin;
        private Estudiante estudianteActual;
        public FormTallleresEstudiante(Estudiante estudianteActual)
        {
            InitializeComponent();
            dgTalleres.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgTalleres.MultiSelect = false;

            CargarTalleres();
            this.estudianteActual = estudianteActual;
        }
        private void CargarTalleres()
        {
            List<Taller> talleres = nTaller.ListarTodo();

            dgTalleres.DataSource = null;
            dgTalleres.DataSource = talleres.Select(t => new
            {
                Código = t.id,
                Nombre = t.nombre,
                Tipo = t.tipoTaller,
                OcupacionMaxima = t.ocupacionMaxima,
                Fecha = t.fechaTaller
            }).ToList();
        }
        private void MostrarEnDataGrid(List<Inscripcion_Taller> lista)
        {
            if (lista.Count == 0)
            {
                return;
            }
            dgTalleres.DataSource = null;
            dgTalleres.DataSource = lista;
        }
        private void AbrirFormulario(Form formulario)
        {
            formulario.Show();
            this.Hide();
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
            CargarTalleres();
        }
        private void btnInicio_Click(object sender, EventArgs e)
        {
            FormInicioEstudiante form = new FormInicioEstudiante(estudianteActual);
            AbrirFormulario(form);
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            FormPerfilEstudiante form = new FormPerfilEstudiante(estudianteActual);
            AbrirFormulario(form);
        }

        private void btnInscribirse_Click(object sender, EventArgs e)
        {
            if (dgTalleres.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un taller para inscribirse.");
                return;
            }

            string idTaller = dgTalleres.CurrentRow.Cells["Código"].Value.ToString();

            Inscripcion_Taller inscripcion = new Inscripcion_Taller();

            inscripcion.id_Estudiante = idEstudianteLogueado;
            inscripcion.id_Taller = idTaller;
            inscripcion.asistencia = "Inscrito";

            MessageBox.Show(nInscripcion.Registrar(inscripcion));

            CargarTalleres();
        }

        private void FormTallleresEstudiante_Load(object sender, EventArgs e)
        {
            lbPFnombre.Text = estudianteActual.nombre;
        }
    }
}
