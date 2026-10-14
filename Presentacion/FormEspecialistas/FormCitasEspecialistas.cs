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
    public partial class FormCitasEspecialistas : Form
    {
        private NCita nCita = new NCita();
        private NEspecialista nEspecialista = new NEspecialista();
        private NEstudiante nEstudiante = new NEstudiante();

        private string loginEspecialista = FormLogin.usuarioLogin;
        private List<Cita> misCitas = new List<Cita>();
        private Especialista especialistaActual;
        public FormCitasEspecialistas(Especialista especialistaActual)
        {
            InitializeComponent();
            this.especialistaActual = especialistaActual;
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");

        }

        private void FormCitasEspecialistas_Load(object sender, EventArgs e)
        {
            lbPFnombre.Text = especialistaActual.nombre;
            cmbEstudiante.DataSource=nEstudiante.ListarTodo();
            cmbEstudiante.DisplayMember = "nombre";
            cmbEstudiante.ValueMember = "id";
            CargarCitas();
        }

        private void CargarCitas()
        {
            try
            {
                misCitas = nCita.ListarTodo()
                    .Where(c => c.id_Especialista == loginEspecialista)
                    .ToList();

                MostrarEnGrid(misCitas);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar citas: " + ex.Message);
            }
        }

        private void MostrarEnGrid(List<Cita> lista)
        {
            dgCitas.DataSource = null;
            dgCitas.DataSource = lista.Select(c => new
            {
                Código = c.id,
                Estudiante = c.id_Estudiante,
                Fecha = c.fecha.ToString(),
                Hora = c.hora.ToString(),
                Estado = c.estado
            }).ToList();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            string texto = cmbEstudiante.SelectedValue.ToString();

            if (texto == "")
            {
                MostrarEnGrid(misCitas);
                return;
            }

            var filtradas = misCitas
                .Where(c => c.id_Estudiante.ToUpper().Contains(texto))
                .ToList();

            MostrarEnGrid(filtradas);
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

        private void btnAceptarCita_Click(object sender, EventArgs e)
        {
            if (dgCitas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una cita");
                return;
            }

            string idCita = dgCitas.CurrentRow.Cells["Código"].Value.ToString();
            string estado = dgCitas.CurrentRow.Cells["Estado"].Value.ToString();

            if (estado != "Programada")
            {
                MessageBox.Show("La cita no ha sido programada y no se puede marcar como realizada.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Desea marcar la cita como realizada?",
                "Confirmar cita",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (respuesta == DialogResult.Yes)
            {
                MessageBox.Show(nCita.MarcarRealizada(idCita));
                CargarCitas();
            }
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            FormPerfilEspecialista form = new FormPerfilEspecialista(especialistaActual);
            AbrirFormulario(form);
        }

        private void btnMarcarProgramada_Click(object sender, EventArgs e)
        {
            if (dgCitas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una cita");
                return;
            }

            string idCita = dgCitas.CurrentRow.Cells["Código"].Value.ToString();
            string estado = dgCitas.CurrentRow.Cells["Estado"].Value.ToString();

            if (estado == "Programada")
            {
                MessageBox.Show("La cita ya está marcada como programada.");
                return;
            }
            if (estado == "Realizada")
            {
                MessageBox.Show("La cita ya ha sido realizada y no se puede marcar como programada.");
                return;
            }
            DialogResult respuesta = MessageBox.Show(
                "¿Desea aceptar la cita?",
                "Confirmar cita",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (respuesta == DialogResult.Yes)
            {
                MessageBox.Show(nCita.MarcarProgramada(idCita));
                CargarCitas();
            }
        }
    }
}
