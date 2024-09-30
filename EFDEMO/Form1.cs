using AccesoDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFDEMO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private CustomerRepository cr = new CustomerRepository();

        private void btnObtenerTodos_Click(object sender, EventArgs e)
        {
            var cliente = cr.ObtenerTodos();
            dgvCustomers.DataSource = cliente;
        }

        private void btnObtenerPorID_Click_1(object sender, EventArgs e)
        {
            var cliente = cr.ObtenerPorID(tboxObtenerID.Text);
            List<Customers> lista1 = new List<Customers> { cliente };
            if(cliente != null)
            {
                llenarCampos(cliente);
            }
            dgvCustomers.DataSource = lista1;
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            var cliente = crearCliente();
            var resultado = cr.InsertarCliente(cliente);
            MessageBox.Show($"Se inserto {resultado} cliente");
            dgvCustomers.DataSource = cr.ObtenerTodos();
        }

        private Customers crearCliente()
        {
            var cliente = new Customers()
            {
                CustomerID = tboxCustomerID.Text,
                CompanyName = tboxCompanyName.Text,
                ContactName = tboxContactName.Text,
                Address = tboxAddress.Text,
                ContactTitle = tboxContactTitle.Text
            };
            return cliente;
        }

        private void llenarCampos(Customers customers)
        {
            {
                tboxCustomerID.Text = customers.CustomerID;
                tboxCompanyName.Text = customers.CompanyName;
                tboxContactName.Text = customers.ContactName;
                tboxAddress.Text = customers.Address;
                tboxContactTitle.Text = customers.ContactTitle;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            var cliente = crearCliente();
            cr.UpdateCliente(cliente);
            var resultado = cr.ObtenerPorID(cliente.CustomerID);
            if (resultado != null)
            {
                List<Customers> lista1 = new List<Customers> { resultado };
                dgvCustomers.DataSource = lista1;
            }
            else
            {
                MessageBox.Show("Error: No se encontró el cliente actualizado.");
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var eliminadas = cr.DeleteCliente(tboxCustomerID.Text);
            MessageBox.Show($"Se elimino {eliminadas} filas");
            dgvCustomers.DataSource = cr.ObtenerTodos();
        }
    }
}