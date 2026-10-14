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
    public partial class FormEstudiantesAdmin : Form
    {
        NEstudiante nEstudiante = new NEstudiante();
        NEspecialista nEspecialista = new NEspecialista();
        NCita nNCita = new NCita();
        public FormEstudiantesAdmin()
        {
            InitializeComponent();
            MostrarCMBEspecialistas(nEspecialista.ListarTodo());
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");
        }
        private void CargarGrados()
        {
            cmbGradoAcademico.Items.Clear();
            cmbGradoAcademico.Items.Add("Inicial");
            cmbGradoAcademico.Items.Add("Primaria");
            cmbGradoAcademico.Items.Add("Secundaria");
        }

        private void MostrarCMBEspecialistas(List<Especialista> especialistas)
        {
            if(especialistas.Count > 0)
            {
                cmbEspecialista.DataSource= especialistas;
                cmbEspecialista.DisplayMember = "nombre";
                cmbEspecialista.ValueMember = "id";
            }
        }
        private void ListarEstudiantes()
        {
            List<Estudiante> lista = nEstudiante.ListarTodo();

            dgvTodosEstudiantes.DataSource = null;
            dgvTodosEstudiantes.DataSource = lista.Select(est => new
            {
                Id = est.id,
                Nombre = est.nombre,
                GradoAcademico = est.gradoAcademico,
                ContactoApoderado = est.contactoApod
            }).ToList();
        }
        private void Limpiar()
        {
            tbNombre.Clear();
            cmbGradoAcademico.SelectedIndex = -1;
            tbContactoApoderado.Clear();
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (tbNombre.Text == "" || cmbGradoAcademico.Text == "" || tbContactoApoderado.Text == "")
            {
                MessageBox.Show("Complete todos los campos");
                return;
            }

            Estudiante estudiante = new Estudiante();

            estudiante.nombre = tbNombre.Text.Trim();
            estudiante.gradoAcademico = cmbGradoAcademico.Text;
            estudiante.contactoApod = tbContactoApoderado.Text.Trim();
            estudiante.creadoPor = "Administrador";
            estudiante.fechaCreacion = DateTime.Now;
            estudiante.modificadoPor = "Administrador";
            estudiante.fechaModific = DateTime.Now;

            MessageBox.Show(nEstudiante.Registrar(estudiante));

            ListarEstudiantes();
            Limpiar();
        }

        private void btnBuscarPorNombre_Click(object sender, EventArgs e)
        {
            string texto = txtBuscarPorNombre.Text.Trim().ToLower();

            List<Estudiante> lista = nEstudiante.ListarTodo();

            if (texto == "")
            {
                ListarEstudiantes();
                return;
            }

            dgvTodosEstudiantes.DataSource = null;
            dgvTodosEstudiantes.DataSource = lista
                .Where(est => est.nombre.ToLower().Contains(texto) || est.id.ToLower().Contains(texto))
                .Select(est => new
                {
                    Id = est.id,
                    Nombre = est.nombre,
                    GradoAcademico = est.gradoAcademico,
                    ContactoApoderado = est.contactoApod
                }).ToList();
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (dgvTodosEstudiantes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un estudiante");
                return;
            }

            string id_especialista = cmbEspecialista.ValueMember;
            string id_estudiante = dgvTodosEstudiantes.CurrentRow.Cells["id"].Value.ToString();
            Cita cita = new Cita();
            cita.id_Estudiante = id_estudiante;
            cita.id_Especialista = id_especialista;
            string mensaje =nNCita.Registrar(cita);
            

            MessageBox.Show("Asignación seleccionada: " + id_estudiante + " -" + id_especialista);
        }
        private void AbrirFormulario(Form formulario)
        {
            formulario.Show();
            this.Hide();
        }

        private void FormEstudiantesAdmin_Load(object sender, EventArgs e)
        {
            CargarGrados();
            ListarEstudiantes();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void btnEvaluaciones_Click_1(object sender, EventArgs e)
        {
            FormEvaluacionesAdmin frm = new FormEvaluacionesAdmin();
            AbrirFormulario(frm);
        }

        private void btnDeshacer_Click(object sender, EventArgs e)
        {
            ListarEstudiantes();
        }

        private void btnCitas_Click_1(object sender, EventArgs e)
        {
            FormCitaAdmin frm = new FormCitaAdmin();
            AbrirFormulario(frm);
        }

        private void btnTalleres_Click_1(object sender, EventArgs e)
        {
            FormTalleresAdmin frm = new FormTalleresAdmin();
            AbrirFormulario(frm);
        }

        private void btnEspecialistas_Click_1(object sender, EventArgs e)
        {
            FormEspecialistasAdmin frm = new FormEspecialistasAdmin();
            AbrirFormulario(frm);
        }

        private void btnReportes_Click_1(object sender, EventArgs e)
        {
            FormReportesAdmin frm = new FormReportesAdmin();
            AbrirFormulario(frm);
        }

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            FormDashboardAdmin frm = new FormDashboardAdmin();
            AbrirFormulario(frm);
        }

        private void btnEstudiantes_Click_1(object sender, EventArgs e)
        {
            FormEstudiantesAdmin frm = new FormEstudiantesAdmin();
            AbrirFormulario(frm);
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTodosEstudiantes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un estudiante");
                return;
            }

            string codigo = dgvTodosEstudiantes.CurrentRow.Cells["id"].Value.ToString();
            Estudiante existente = nEstudiante.BuscarPorId(codigo);

            if (existente == null)
            {
                MessageBox.Show("No existe un estudiante con ese código");
                return;
            }

            string mensaje = nEstudiante.Eliminar(codigo);
            MessageBox.Show(mensaje);

            ListarEstudiantes();
            Limpiar();
        }
    }
}
