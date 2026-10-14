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
    public partial class FormMisCitas : Form
    {
        private NCita nCita = new NCita();
        private NEspecialista nEspecialista = new NEspecialista();

        private string idEstudianteLogueado = FormLogin.usuarioLogin;

        private List<Cita> misCitas = new List<Cita>();
        Estudiante estudianteActual;
        public FormMisCitas(Estudiante estudianteActual)
        {
            InitializeComponent();
            this.estudianteActual = estudianteActual;
        }
        private void AbrirFormulario(Form formulario)
        {
            formulario.Show();
            this.Hide();
        }

        private void CargarEspecialistas()
        {
            List<Especialista> especialistas = nEspecialista.ListarTodo();

            cmbEspecialista.DataSource = null;
            cmbEspecialista.DataSource = especialistas;
            cmbEspecialista.ValueMember = "id";
            cmbEspecialista.DisplayMember = "nombre";

            cmbEspecialista2.DataSource = null;
            cmbEspecialista2.DataSource = especialistas.ToList();
            cmbEspecialista2.ValueMember = "id";
            cmbEspecialista2.DisplayMember = "nombre";
        }

        private void CargarMisCitas()
        {
            try
            {
                misCitas = nCita.ListarTodo()
                    .Where(c => c.id_Estudiante == idEstudianteLogueado)
                    .ToList();

                MostrarCitas(misCitas);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar citas: " + ex.Message);
            }
        }
        private void MostrarCitas(List<Cita> lista)
        {
            dgMisCitas.DataSource = null;
            dgMisCitas.DataSource = lista.Select(c => new
            {
                Código = c.id,
                Especialista = c.id_Especialista,
                Fecha = c.fecha.ToString(),
                Hora = c.hora.ToString(),
                Estado = c.estado
            }).ToList();
        }

        private void FormMisCitas_Load(object sender, EventArgs e)
        {
            dgMisCitas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgMisCitas.MultiSelect = false;
            lbPFnombre.Text = estudianteActual.nombre;
            CargarEspecialistas();
            CargarMisCitas();
        }

        private void btnRegistrarCita_Click(object sender, EventArgs e)
        {
            if (cmbEspecialista.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un especialista");
                return;
            }

            Cita cita = new Cita();

            cita.id_Estudiante = estudianteActual.id;
            cita.id_Especialista = cmbEspecialista.SelectedValue.ToString();
            cita.fecha = dtFecha.Value.Date;
            cita.hora = dtFecha.Value.TimeOfDay;

            MessageBox.Show(nCita.Registrar(cita));

            CargarMisCitas();
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
        private void btnCitas_Click(object sender, EventArgs e)
        {
            CargarMisCitas();
        }
        private void btnEvaluaciones_Click(object sender, EventArgs e)
        {
            FormEvaluacionesEstudiante form = new FormEvaluacionesEstudiante(estudianteActual);
            AbrirFormulario(form);  
        }
        private void btnTalleres_Click(object sender, EventArgs e)
        {
            FormTallleresEstudiante form = new FormTallleresEstudiante(estudianteActual);
            AbrirFormulario(form);
        }

        private void btnBuscarPorEspecialista_Click(object sender, EventArgs e)
        {
            if (cmbEspecialista2.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un especialista");
                return;
            }

            string idEspecialista = cmbEspecialista2.SelectedValue.ToString();

            List<Cita> filtradas = misCitas
                .Where(c => c.id_Especialista == idEspecialista)
                .ToList();

            MostrarCitas(filtradas);
        }

        
    }
}