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
    public partial class FormTalleresAdmin : Form
    {
        NTaller nTaller = new NTaller();
        public FormTalleresAdmin()
        {
            InitializeComponent();
            lbhoy.Text = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy");
        }

        private void ListarTalleres()
        {
            List<Taller> lista = nTaller.ListarTodo();

            dgTodoslostalleres.DataSource = null;
            dgTodoslostalleres.DataSource = lista.Select(tal => new
            {
                Id = tal.id,
                Nombre = tal.nombre,
                TipoTaller = tal.tipoTaller,
                OcupacionMaxima = tal.ocupacionMaxima,
                FechaTaller = tal.fechaTaller
            }).ToList();
        }

        private void ListarAuditoria()
        {
            List<Taller> lista = nTaller.ListarTodo();

            dgAuditoriaDelostalleres.DataSource = null;
            dgAuditoriaDelostalleres.DataSource = lista.Select(tal => new
            {
                Id = tal.id,
                Nombre = tal.nombre,
                CreadoPor = tal.creadoPor,
                FechaCreacion = tal.fechaCreacion,
                ModificadoPor = tal.modificadoPor,
                FechaModificacion = tal.fechaModific
            }).ToList();
        }
        private void Limpiar()
        {
            tbCodigoTalleres.Clear();
            tbNombre.Clear();
            txtOcupacionMaxima.Clear();
            dtpFecha.Value = DateTime.Now;
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (tbCodigoTalleres.Text == "" || tbNombre.Text == "" || txtOcupacionMaxima.Text == "")
            {
                MessageBox.Show("Complete todos los campos");
                return;
            }

            string id = tbCodigoTalleres.Text.Trim().ToUpper();

            Taller existente = nTaller.ListarTodo()
                .FirstOrDefault(tal => tal.id == id);

            if (existente != null)
            {
                MessageBox.Show("Ya existe un taller con ese ID");
                return;
            }

            int ocupacionMaxima;

            if (int.TryParse(txtOcupacionMaxima.Text, out ocupacionMaxima) == false)
            {
                MessageBox.Show("La ocupación máxima debe ser un número");
                return;
            }

            if (ocupacionMaxima <= 0)
            {
                MessageBox.Show("La ocupación máxima debe ser mayor a 0");
                return;
            }

            Taller taller = new Taller();

            taller.id = id;
            taller.nombre = tbNombre.Text.Trim();
            taller.tipoTaller = "General";
            taller.ocupacionMaxima = ocupacionMaxima;
            taller.tipoTaller=cmbTipoTaller.Text;
            taller.fechaTaller = dtpFecha.Value;
            taller.creadoPor = "Administrador";
            taller.fechaCreacion = DateTime.Now;
            taller.modificadoPor = "Administrador";
            taller.fechaModific = DateTime.Now;

            MessageBox.Show(nTaller.Registrar(taller));

            ListarTalleres();
            ListarAuditoria();
            Limpiar();
        }

        private void AbrirFormulario(Form formulario)
        {
            formulario.Show();
            this.Hide();
        }

        

        private void FormTalleresAdmin_Load(object sender, EventArgs e)
        {
            ListarTalleres();
            ListarAuditoria();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (tbCodigoTalleres.Text == "")
            {
                MessageBox.Show("Ingrese el código del taller");
                return;
            }

            string codigo = tbCodigoTalleres.Text.Trim().ToUpper();

            Taller existente = nTaller.BuscarPorId(codigo);

            if (existente == null)
            {
                MessageBox.Show("No existe un taller con ese código");
                return;
            }

            string mensaje = nTaller.Eliminar(codigo);
            MessageBox.Show(mensaje);

            ListarTalleres();
            ListarAuditoria();
            Limpiar();
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
            ListarTalleres();
            ListarAuditoria();
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
