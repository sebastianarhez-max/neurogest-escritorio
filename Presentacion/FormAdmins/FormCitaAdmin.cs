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

namespace Presentacion.FormAdmins
{
    public partial class FormCitaAdmin : Form
    {
        private NCita nCita = new NCita();

        private List<Cita> todasLasCitas = new List<Cita>();
        public FormCitaAdmin()
        {
            InitializeComponent();
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");
        }
        private void CargarEstados()
        {
            cmbxEstado.Items.Clear();
            cmbxEstado.Items.Add("Todos");
            cmbxEstado.Items.Add("Programada");
            cmbxEstado.Items.Add("Realizada");
            cmbxEstado.SelectedIndex = 0;
        }
        private void CargarCitas()
        {
            try
            {
                todasLasCitas = nCita.ListarTodo();

                MostrarTodasLasCitas(todasLasCitas);
                MostrarSolicitudesPendientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar citas: " + ex.Message);
            }
        }
        private void MostrarTodasLasCitas(List<Cita> lista)
        {
            dgTodasLascitas.DataSource = null;
            dgTodasLascitas.DataSource = lista.Select(c => new
            {
                Código = c.id,
                Estudiante = c.id_Estudiante,
                Especialista = c.id_Especialista,
                Fecha = c.fecha.ToString(),
                Hora = c.hora.ToString(),
                Estado = c.estado
            }).ToList();
        }
        private void MostrarSolicitudesPendientes()
        {
            List<Cita> pendientes = todasLasCitas
                .Where(c => c.estado == "Solicitada")
                .ToList();

            dgSolicitudesPendientes.DataSource = null;
            dgSolicitudesPendientes.DataSource = pendientes.Select(c => new
            {
                Código = c.id,
                Estudiante = c.id_Estudiante,
                Especialista = c.id_Especialista,
                Fecha = c.fecha.ToString(),
                Hora = c.hora.ToString(),
                Estado = c.estado
            }).ToList();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void FormCitaAdmin_Load(object sender, EventArgs e)
        {
            dgTodasLascitas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgTodasLascitas.MultiSelect = false;

            dgSolicitudesPendientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgSolicitudesPendientes.MultiSelect = false;

            CargarEstados();
            CargarCitas();
        }

        private void btnBuscarPorEstudiante_Click(object sender, EventArgs e)
        {
            string codigoEstudiante = tbCodigoEstudiante.Text.Trim().ToUpper();

            if (codigoEstudiante == "")
            {
                MessageBox.Show("Ingrese el código del estudiante");
                return;
            }

            List<Cita> filtradas = todasLasCitas
                .Where(c => c.id_Estudiante.ToUpper().Contains(codigoEstudiante))
                .ToList();

            MostrarTodasLasCitas(filtradas);
        }

        private void btnOrdenardesdeLadechaMasreciente_Click(object sender, EventArgs e)
        {
            List<Cita> lista = todasLasCitas;

            if (cmbxEstado.Text != "Todos")
            {
                lista = lista
                    .Where(c => c.estado == cmbxEstado.Text)
                    .ToList();
            }

            lista = lista
                .OrderByDescending(c => c.fecha)
                .ToList();

            MostrarTodasLasCitas(lista);
        }

        private void btnAceptarCitas_Click(object sender, EventArgs e)
        {
            if (dgSolicitudesPendientes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una cita solicitada");
                return;
            }

            string idCita = dgSolicitudesPendientes.CurrentRow.Cells["Código"].Value.ToString();

            DialogResult respuesta = MessageBox.Show(
                "¿Desea marcar esta cita como programada?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (respuesta == DialogResult.Yes)
            {
                MessageBox.Show(nCita.MarcarProgramada(idCita));
                CargarCitas();
            }
        }

        private void AbrirFormulario(Form formulario)
        {
            formulario.Show();
            this.Hide();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FormDashboardAdmin form = new FormDashboardAdmin();
            AbrirFormulario(form);
        }

        private void btnEstudiantes_Click(object sender, EventArgs e)
        {
            FormEstudiantesAdmin form = new FormEstudiantesAdmin();
            AbrirFormulario(form);
        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            CargarCitas();
        }

        private void btnEvaluaciones_Click(object sender, EventArgs e)
        {
            FormEvaluacionesAdmin form = new FormEvaluacionesAdmin();
            AbrirFormulario(form);
        }

        private void btnTalleres_Click(object sender, EventArgs e)
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
    }
}
