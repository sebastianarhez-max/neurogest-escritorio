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
    public partial class FormEspecialistasAdmin : Form
    {
        NEspecialista nEspecialista = new NEspecialista();
        public FormEspecialistasAdmin()
        {
            InitializeComponent();
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");
        }
        private void CargarEspecialidades()
        {
            cmbxEspecialidad.Items.Clear();
            cmbxEspecialidad.Items.Add("Psicología");
            cmbxEspecialidad.Items.Add("Psicopedagogía");
            cmbxEspecialidad.Items.Add("Terapia de lenguaje");
            cmbxEspecialidad.Items.Add("Orientación educativa");

            cmbxEspecialidd.Items.Clear();
            cmbxEspecialidd.Items.Add("Psicología");
            cmbxEspecialidd.Items.Add("Psicopedagogía");
            cmbxEspecialidd.Items.Add("Terapia de lenguaje");
            cmbxEspecialidd.Items.Add("Orientación educativa");
        }
        private void ListarEspecialistas()
        {
            List<Especialista> lista = nEspecialista.ListarTodo();

            dgTodoslosespcialiastas.DataSource = null;
            dgTodoslosespcialiastas.DataSource = lista.Select(esp => new
            {
                Código = esp.id,
                Nombre = esp.nombre,
                Especialidad = esp.especialidad
            }).ToList();
        }
        private void Limpiar()
        {
            tbNombre.Clear();
            cmbxEspecialidad.SelectedIndex = -1;
        }
        private void ListarAuditoria()
        {
            List<Especialista> lista = nEspecialista.ListarTodo();

            dgAuditoriasdeespecialistas.DataSource = null;
            dgAuditoriasdeespecialistas.DataSource = lista.Select(esp => new
            {
                Código = esp.id,
                Nombre = esp.nombre,
                CreadoPor = esp.creadoPor,
                FechaCreacion = esp.fechaCreacion,
                ModificadoPor = esp.modificadoPor,
                FechaModificacion = esp.fechaModific
            }).ToList();
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (tbNombre.Text == "" || cmbxEspecialidad.Text == "")
            {
                MessageBox.Show("Complete todos los campos");
                return;
            }


            Especialista especialista = new Especialista();

            especialista.nombre = tbNombre.Text.Trim();
            especialista.especialidad = cmbxEspecialidad.Text;
            especialista.creadoPor = "Administrador";
            especialista.fechaCreacion = DateTime.Now;
            especialista.modificadoPor = "Administrador";
            especialista.fechaModific = DateTime.Now;

            MessageBox.Show(nEspecialista.Registrar(especialista));

            ListarEspecialistas();
            ListarAuditoria();
            Limpiar();
        }

        private void btnBuscarporespecialista_Click(object sender, EventArgs e)
        {
            if (tbBuscarCodEspecialista.Text == "")
            {
                MessageBox.Show("Escriba el código de un especialista");
                return;
            }

            List<Especialista> lista = nEspecialista.ListarTodo();

            dgTodoslosespcialiastas.DataSource = null;
            dgTodoslosespcialiastas.DataSource = lista
                .Where(esp => esp.id == tbBuscarCodEspecialista.Text).ToList();
             
        }

        private void btnBuscarPorEspecialidad_Click(object sender, EventArgs e)
        {
            List<Especialista> lista = nEspecialista.ListarTodo();

            dgTodoslosespcialiastas.DataSource = null;
            dgTodoslosespcialiastas.DataSource = lista
                .Where(esp => esp.especialidad == cmbxEspecialidd.Text)
                .Select(esp => new
                {
                    esp.id,
                    esp.nombre,
                    esp.especialidad
                }).ToList();
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

        private void btnTalleres_Click(object sender, EventArgs e)
        {
            FormTalleresAdmin form = new FormTalleresAdmin();
            AbrirFormulario(form);
        }

        private void btnEspecialistas_Click(object sender, EventArgs e)
        {
            ListarEspecialistas();
            ListarAuditoria();
        }

        private void FormEspecialistasAdmin_Load(object sender, EventArgs e)
        {
            CargarEspecialidades();
            ListarEspecialistas();
            ListarAuditoria();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgTodoslosespcialiastas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un especialista para eliminar");
                return;
            }

            string codigo = dgTodoslosespcialiastas.SelectedRows[0].Cells["Código"].Value.ToString();

            Especialista existente = nEspecialista.BuscarPorId(codigo);

            if (existente == null)
            {
                MessageBox.Show("No existe un especialista con ese código");
                return;
            }

            string mensaje = nEspecialista.Eliminar(codigo);
            MessageBox.Show(mensaje);

            ListarEspecialistas();
            ListarAuditoria();
            Limpiar();
        }

        private void btnEspecialistas_Click_1(object sender, EventArgs e)
        {
            ListarEspecialistas();
            ListarAuditoria();
        }

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            FormDashboardAdmin form = new FormDashboardAdmin();
            AbrirFormulario(form);
        }

        private void btnEstudiantes_Click_1(object sender, EventArgs e)
        {
            FormEstudiantesAdmin form = new FormEstudiantesAdmin();
            AbrirFormulario(form);
        }

        private void btnCitas_Click_1(object sender, EventArgs e)
        {
            FormCitaAdmin form = new FormCitaAdmin();
            AbrirFormulario(form);
        }

        private void btnEvaluaciones_Click_1(object sender, EventArgs e)
        {
            FormEvaluacionesAdmin form = new FormEvaluacionesAdmin();
            AbrirFormulario(form);
        }

        private void btnTalleres_Click_1(object sender, EventArgs e)
        {
            FormTalleresAdmin form = new FormTalleresAdmin();
            AbrirFormulario(form);
        }

        private void btnReportes_Click_1(object sender, EventArgs e)
        {
            FormReportesAdmin form = new FormReportesAdmin();
            AbrirFormulario(form);
        }
    }
}
