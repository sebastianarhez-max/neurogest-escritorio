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
    public partial class FormEvaluacionesAdmin : Form
    {
        private NEvaluacion nEvaluacion = new NEvaluacion();

        public FormEvaluacionesAdmin()
        {
            InitializeComponent();
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");
        }

        private void FormEvaluacionesAdmin_Load_1(object sender, EventArgs e)
        {
            ListarEvaluaciones();
        }

        private void ListarEvaluaciones()
        {
            try
            {
                List<Evaluacion> lista = nEvaluacion.ListarTodo();

                dgTodasLasevaluaciones.DataSource = null;
                dgTodasLasevaluaciones.DataSource = lista.Select(ev => new
                {
                    Id = ev.id,
                    IdCita = ev.id_Cita,
                    Observaciones = ev.observaciones,
                    Resultados = ev.resultados,
                    Recomendacion = ev.recomendacion
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las evaluaciones: " + ex.Message);
            }
        }
        private void AbrirFormulario(Form formulario)
        {
            formulario.Show();
            this.Hide();
        }


        private void btnBuscarPorCodigo_Click(object sender, EventArgs e)
        {
            try
            {
                string busqueda = tbBuscarPorCodigo.Text.Trim();

                if (busqueda == "")
                {
                    ListarEvaluaciones();
                    return;
                }

                var filtrado = nEvaluacion.ListarTodo()
                    .Where(ev => ev.id.ToString().Contains(busqueda)
                              || (ev.id_Cita != null && ev.id_Cita.Contains(busqueda)))
                    .Select(ev => new
                    {
                        Id = ev.id,
                        IdCita = ev.id_Cita,
                        Observaciones = ev.observaciones,
                        Resultados = ev.resultados,
                        Recomendacion = ev.recomendacion
                    }).ToList();

                dgTodasLasevaluaciones.DataSource = null;
                dgTodasLasevaluaciones.DataSource = filtrado;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la búsqueda: " + ex.Message);
            }

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
            FormCitaAdmin form = new FormCitaAdmin();
            AbrirFormulario(form);
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