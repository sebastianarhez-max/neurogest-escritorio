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
    public partial class FormPerfilEstudiante : Form
    {
        NEstudiante nEstudiante = new NEstudiante();

        private string idEstudianteLogueado = FormLogin.usuarioLogin;
        private Estudiante estudianteActual;
        public FormPerfilEstudiante(Estudiante estudianteActual)
        {
            InitializeComponent();
            this.estudianteActual = estudianteActual;
        }
        private void MostrarInformacionEstudiante()
        {
            try
            {
                lbEstudiante.Items.Clear();

                Estudiante estudiante = nEstudiante.ListarTodo()
                    .FirstOrDefault(est => est.id == idEstudianteLogueado);

                if (estudiante == null)
                {
                    MessageBox.Show("Estudiante no encontrado.");
                    return;
                }

                lbEstudiante.Items.Add("====== DATOS DEL ESTUDIANTE ======");
                lbEstudiante.Items.Add("Código: " + estudiante.id);
                lbEstudiante.Items.Add("Nombre: " + estudiante.nombre);
                lbEstudiante.Items.Add("Grado Académico: " + estudiante.gradoAcademico);
                lbEstudiante.Items.Add("Contacto Apoderado: " + estudiante.contactoApod);
                lbEstudiante.Items.Add("");
                lbEstudiante.Items.Add("====== AUDITORÍA ======");
                lbEstudiante.Items.Add("Creado por: " + estudiante.creadoPor);
                lbEstudiante.Items.Add("Fecha creación: " + estudiante.fechaCreacion);
                lbEstudiante.Items.Add("Modificado por: " + estudiante.modificadoPor);
                lbEstudiante.Items.Add("Fecha modificación: " + estudiante.fechaModific);

                tbnombre.Text = estudiante.nombre;
                tbgrado.Text = estudiante.gradoAcademico;
                tbcontacto.Text = estudiante.contactoApod;

                lbPFnombre.Text = estudiante.nombre;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar información: " + ex.Message);
            }
        }
        private void btnEditarInformacion_Click(object sender, EventArgs e)
        {
            if (tbnombre.Text.Trim() == "" || tbgrado.Text.Trim() == "" || tbcontacto.Text.Trim() == "")
            {
                MessageBox.Show("Complete todos los campos.");
                return;
            }

            Estudiante estudiante = nEstudiante.ListarTodo()
                .FirstOrDefault(est => est.id == idEstudianteLogueado);

            if (estudiante == null)
            {
                MessageBox.Show("Estudiante no encontrado.");
                return;
            }

            Estudiante modificado = new Estudiante();

            modificado.id = estudiante.id;
            modificado.nombre = tbnombre.Text.Trim();
            modificado.gradoAcademico = tbgrado.Text.Trim();
            modificado.contactoApod = tbcontacto.Text.Trim();
            modificado.modificadoPor = idEstudianteLogueado;
            modificado.fechaModific = DateTime.Now;

            MessageBox.Show(nEstudiante.Modificar(modificado));

            MostrarInformacionEstudiante();
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
            FormTallleresEstudiante form = new FormTallleresEstudiante(estudianteActual);
            AbrirFormulario(form);
        }
        private void btnInicio_Click(object sender, EventArgs e)
        {
            FormInicioEstudiante form = new FormInicioEstudiante(estudianteActual);
            AbrirFormulario(form);
        }
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            MostrarInformacionEstudiante();
        }

        private void FormPerfilEstudiante_Load(object sender, EventArgs e)
        {
            MostrarInformacionEstudiante();
            lbPFnombre.Text = estudianteActual.nombre;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormInicioEstudiante form = new FormInicioEstudiante(estudianteActual);
            AbrirFormulario(form);
        }
    }
}
