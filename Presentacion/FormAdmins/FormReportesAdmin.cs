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
using System.Windows.Forms.DataVisualization.Charting;

namespace Presentacion.FormAdmins
{
    public partial class FormReportesAdmin : Form
    {
        private NCita nCita = new NCita();
        private NEstudiante nEstudiante = new NEstudiante();
        private NEspecialista nEspecialista = new NEspecialista();
        private NEvaluacion nEvaluacion = new NEvaluacion();
        private NInscripcion_Taller nInscripcion = new NInscripcion_Taller();
        public FormReportesAdmin()
        {
            InitializeComponent();
            CitasEstadoPie();
            AsistenciasFaltasTalleres();
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");
        }
        private void CargarMeses()
        {
            cmbMes.Items.Clear();
            cmbMes.Items.Clear();

            cmbMes.Items.Add("Enero");
            cmbMes.Items.Add("Febrero");
            cmbMes.Items.Add("Marzo");
            cmbMes.Items.Add("Abril");
            cmbMes.Items.Add("Mayo");
            cmbMes.Items.Add("Junio");
            cmbMes.Items.Add("Julio");
            cmbMes.Items.Add("Agosto");
            cmbMes.Items.Add("Septiembre");
            cmbMes.Items.Add("Octubre");
            cmbMes.Items.Add("Noviembre");
            cmbMes.Items.Add("Diciembre");

            cmbEspecialista.Items.Add("Enero");
            cmbEspecialista.Items.Add("Febrero");
            cmbEspecialista.Items.Add("Marzo");
            cmbEspecialista.Items.Add("Abril");
            cmbEspecialista.Items.Add("Mayo");
            cmbEspecialista.Items.Add("Junio");
            cmbEspecialista.Items.Add("Julio");
            cmbEspecialista.Items.Add("Agosto");
            cmbEspecialista.Items.Add("Septiembre");
            cmbEspecialista.Items.Add("Octubre");
            cmbEspecialista.Items.Add("Noviembre");
            cmbEspecialista.Items.Add("Diciembre");

            cmbMes.SelectedIndex = DateTime.Now.Month - 1;
            cmbEspecialista.SelectedIndex = DateTime.Now.Month - 1;
        }
        private void CargarEspecialistas()
        {
            List<Especialista> especialistas = nEspecialista.ListarTodo();

            cmbEspecialista.DataSource = null;
            cmbEspecialista.DataSource = especialistas;
            cmbEspecialista.ValueMember = "id";
            cmbEspecialista.DisplayMember = "nombre";
        }
       
     
        private void CargarAsistenciasYFaltas()
        {
            List<Inscripcion_Taller> inscripciones = nInscripcion.ListarTodo();

            var datos = inscripciones
                .GroupBy(i => i.asistencia)
                .Select(g => new
                {
                    Asistencia = g.Key,
                    Cantidad = g.Count()
                }).ToList();

            chAsistenciasYFaltasTalleres.Series.Clear();
            chAsistenciasYFaltasTalleres.ChartAreas.Clear();
            chAsistenciasYFaltasTalleres.ChartAreas.Add(new ChartArea("Area"));

            Series serie = new Series("Asistencias");
            serie.ChartType = SeriesChartType.Column;

            foreach (var item in datos)
            {
                serie.Points.AddXY(item.Asistencia, item.Cantidad);
            }

            chAsistenciasYFaltasTalleres.Series.Add(serie);
        }
        private void pictureBox15_Click(object sender, EventArgs e)
        {

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
            FormCitaAdmin form = new FormCitaAdmin();
            AbrirFormulario(form);
        }

        private void btnEvaluaciones_Click(object sender, EventArgs e)
        {
            FormEvaluacionesAdmin form = new FormEvaluacionesAdmin();
            AbrirFormulario(form);
        }

        private void button1_Click(object sender, EventArgs e)
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
            CitasEstadoPie();
          //  CargarEstudiantesAtendidosPorMes();
          //  CargarEvaluacionesPorMesYEspecialista();
            CargarAsistenciasYFaltas();
        }

        private void FormReportesAdmin_Load(object sender, EventArgs e)
        {
            CargarMeses();
            CargarEspecialistas();

            CitasEstadoPie();
          //  CargarEstudiantesAtendidosPorMes();
           // CargarEvaluacionesPorMesYEspecialista();
            CargarAsistenciasYFaltas();
        }

        private void cbMesEstudiantes_SelectedIndexChanged(object sender, EventArgs e)
        {
           // CargarEstudiantesAtendidosPorMes();

        }

        private void cmbEspecialista_SelectedIndexChanged(object sender, EventArgs e)
        {
           // CargarEvaluacionesPorMesYEspecialista();

        }

        private void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {
           // CargarEvaluacionesPorMesYEspecialista();

        }

        private void btnBuscarMesyEspecialistta_Click(object sender, EventArgs e)
        {

            dgEvaluacionesMesEspecialista.DataSource = null;
            //evaluaciones por mes y especialista
            List<Evaluacion> evaluaciones = nEvaluacion.ListarTodo();

            int mes = cmbMes.SelectedIndex + 1;
            string idEspecialista = cmbEspecialista.SelectedValue.ToString();

            var datos = evaluaciones
                .Where(ev => ev.Cita.fecha.HasValue &&
                            ev.Cita.fecha.Value.Month == mes &&
                            ev.Cita.id_Especialista == idEspecialista)
                .ToList();

            dgEvaluacionesMesEspecialista.DataSource = datos;

        }

        private void CitasEstadoPie()
        {
            List<Cita> citas = nCita.ListarTodo();

            var datos = citas
                .Where(c => c.estado == "Realizada" || c.estado == "Programada")
                .GroupBy(c => c.estado)
                .Select(g => new
                {
                    Estado = g.Key,
                    Cantidad = g.Count()
                })
                .ToList();

            chartCitasEstado.Series.Clear();
            chartCitasEstado.ChartAreas.Clear();
            chartCitasEstado.Legends.Clear();

            chartCitasEstado.ChartAreas.Add(new ChartArea("Area"));
            chartCitasEstado.Legends.Add(new Legend("Estados"));

            Series serie = new Series("Citas");
            serie.ChartType = SeriesChartType.Pie;

            foreach (var item in datos)
            {
                serie.Points.AddXY(item.Estado, item.Cantidad);
            }

            // Mostrar el nombre del estado y el porcentaje
            serie.Label = "#VALX\n#PERCENT";
            serie.IsValueShownAsLabel = true;

            chartCitasEstado.Series.Add(serie);
        }

        private void btnEstudiantesMes_Click(object sender, EventArgs e)
        {
            List<Cita> citas = nCita.ListarTodo();

            int mes = cbmes.SelectedIndex + 1;

            int cantidad = citas
                .Where(c => c.estado == "Realizada"
                         && c.fecha.HasValue
                         && c.fecha.Value.Month == mes)
                .Select(c => c.id_Estudiante)
                .Distinct()               // Para no contar dos veces al mismo estudiante
                .Count();

            lbEstudiantesAtendidosPorMes.Items.Clear();

            lbEstudiantesAtendidosPorMes.Items.Add("════════════════════════════");
            lbEstudiantesAtendidosPorMes.Items.Add("    REPORTE DE ATENCIÓN");
            lbEstudiantesAtendidosPorMes.Items.Add("════════════════════════════");
            lbEstudiantesAtendidosPorMes.Items.Add("");
            lbEstudiantesAtendidosPorMes.Items.Add($"Mes: {cbmes.Text}");
            lbEstudiantesAtendidosPorMes.Items.Add("");
            lbEstudiantesAtendidosPorMes.Items.Add($"Estudiantes atendidos: {cantidad}");
            lbEstudiantesAtendidosPorMes.Items.Add("");
            lbEstudiantesAtendidosPorMes.Items.Add("════════════════════════════");
        }

        private void AsistenciasFaltasTalleres()
        {
            List<Inscripcion_Taller> inscripciones = nInscripcion.ListarTodo();

            var datos = inscripciones
                .Where(i => i.asistencia == "Asistió" || i.asistencia == "Faltó")
                .GroupBy(i => i.asistencia)
                .Select(g => new
                {
                    Asistencia = g.Key,
                    Cantidad = g.Count()
                })
                .ToList();

            chAsistenciasYFaltasTalleres.Series.Clear();
            chAsistenciasYFaltasTalleres.ChartAreas.Clear();
            chAsistenciasYFaltasTalleres.Legends.Clear();

            chAsistenciasYFaltasTalleres.ChartAreas.Add(new ChartArea("Area"));
            chAsistenciasYFaltasTalleres.Legends.Add(new Legend("Estados"));

            Series serie = new Series("Asistencia");
            serie.ChartType = SeriesChartType.Column;

            foreach (var item in datos)
            {
                serie.Points.AddXY(item.Asistencia, item.Cantidad);
            }

            serie.IsValueShownAsLabel = true;
            serie.Label = "#VAL";

            chAsistenciasYFaltasTalleres.Series.Add(serie);
        }
    }
}
