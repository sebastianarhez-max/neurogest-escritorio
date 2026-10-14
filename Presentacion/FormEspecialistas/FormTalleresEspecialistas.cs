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
    public partial class FormTalleresEspecialistas : Form
    {
        private NTaller nTaller = new NTaller();
        private NEspecialista nEspecialista = new NEspecialista();

        private string loginEspecialista = FormLogin.usuarioLogin;
        private Especialista especialistaActual;
        public FormTalleresEspecialistas(Especialista especialistaActual)
        {
            InitializeComponent();
            this.especialistaActual = especialistaActual;
            lbAuditoria.Items.Add("══════════════════════════════");
            lbAuditoria.Items.Add("      AUDITORÍA DEL TALLER");
            lbAuditoria.Items.Add("══════════════════════════════");
            lbAuditoria.Items.Add("");
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");

        }

        private void lbAuditoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReportes_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbcodigotaller.Text.Trim() == "" || tbNombre.Text.Trim() == "")
                {
                    MessageBox.Show("Complete el código y nombre del taller.");
                    return;
                }

                bool existe = nTaller.ListarTodo().Any(t => t.id == tbcodigotaller.Text.Trim().ToUpper());
                if (existe)
                {
                    MessageBox.Show("Ya existe un taller con ese código.");
                    return;
                }

                Taller nuevoTaller = new Taller();
                nuevoTaller.id = tbcodigotaller.Text.Trim().ToUpper();
                nuevoTaller.nombre = tbNombre.Text.Trim();
                nuevoTaller.tipoTaller = cmbTipo.Text;
                nuevoTaller.ocupacionMaxima = int.Parse(tbCupo.Text);
                nuevoTaller.fechaTaller = datetpFecha.Value;
                nuevoTaller.creadoPor = loginEspecialista;
                nuevoTaller.fechaCreacion = DateTime.Now;

                string mensaje = nTaller.Registrar(nuevoTaller);

                if (mensaje == "Registro correcto")
                {
                    MessageBox.Show("Taller registrado correctamente.");
                    tbcodigotaller.Text = "";
                    tbNombre.Text = "";
                    CargarTalleres();
                    LlenarAuditoria(nuevoTaller);
                }
                else
                {
                    MessageBox.Show("No se pudo registrar: " + mensaje);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message);
            }
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
            FormCitasEspecialistas form = new FormCitasEspecialistas(especialistaActual);
            AbrirFormulario(form);
        }

        private void btnEvaluaciones_Click(object sender, EventArgs e)
        {
            FormEvaluacionesEspecialistas form = new FormEvaluacionesEspecialistas(especialistaActual);
            AbrirFormulario(form);
        }

        private void btnTalleres_Click(object sender, EventArgs e)
        {
        }

        private void FormTalleresEspecialistas_Load(object sender, EventArgs e)
        {
            lbPFnombre.Text = especialistaActual.nombre;

            CargarTalleres();




        }
        private void AbrirFormulario(Form formulario)
        {
            formulario.Show();
            this.Hide();
        }
        private void CargarTalleres()
        {
            try
            {
                var talleres = nTaller.ListarTodo();
                dgtalleres.DataSource = null;
                dgtalleres.DataSource = talleres.Select(t => new
                {
                    Código = t.id,
                    Nombre = t.nombre,
                    Tipo = t.tipoTaller,
                    Cupos = t.ocupacionMaxima,
                    Fecha = t.fechaTaller.HasValue ? t.fechaTaller.Value.ToShortDateString() : "-"
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar talleres: " + ex.Message);
            }
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            FormPerfilEspecialista form = new FormPerfilEspecialista(especialistaActual);
            AbrirFormulario(form);
        }

        private void LlenarAuditoria(Taller taller)
        {
            lbAuditoria.Items.Clear();



            lbAuditoria.Items.Add($"Creado por      : {taller.creadoPor}");
            lbAuditoria.Items.Add($"Fecha creación  : {taller.fechaCreacion:dd/MM/yyyy HH:mm}");
            lbAuditoria.Items.Add("");
            lbAuditoria.Items.Add("Modificado por  : " + especialistaActual.nombre);
            lbAuditoria.Items.Add("");
            lbAuditoria.Items.Add("══════════════════════════════");
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (tbcodigotaller.Text == "")
            {
                MessageBox.Show("Ingrese el código del taller");
                return;
            }

            string codigo = tbcodigotaller.Text.Trim().ToUpper();

            Taller existente = nTaller.BuscarPorId(codigo);

            if (existente == null)
            {
                MessageBox.Show("No existe un taller con ese código");
                return;
            }

            string mensaje = nTaller.Eliminar(codigo);
            MessageBox.Show(mensaje);

            CargarTalleres();
            tbcodigotaller.Text = "";
            tbNombre.Text = "";
        }
    }
}
