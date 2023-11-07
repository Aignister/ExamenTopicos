using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TablaPacientes();
        }

        SqlConnection conexion = new SqlConnection("Data Source=DARKWURM;Initial Catalog=dbVeterinaria;Integrated Security=True");

        public void Limpiar()
        {
            txtNombre.Text = "";
            txtEspecie.Text = "";
            txtRaza.Text = "";
            txtPropietario.Text = "";
            txtId.Text = "";
            dateTime.Value = DateTime.Now;
        }

        public void TablaPacientes()
        {
            SqlDataAdapter data = new SqlDataAdapter("SELECT * FROM Paciente", conexion);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataTable.DataSource = dt;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtEspecie.Text) || string.IsNullOrEmpty(txtRaza.Text) || string.IsNullOrEmpty(txtPropietario.Text) || dateTime.Value == null || string.IsNullOrEmpty(txtPropietario.Text))
            {
                MessageBox.Show("Faltan campos por llenar");
                return; 
            }

            conexion.Open();

            SqlCommand registar = new SqlCommand("Crud_Paciente", conexion);
            registar.CommandType = CommandType.StoredProcedure;

            registar.Parameters.Add(new SqlParameter("@Opc", SqlDbType.NVarChar, 30)).Value = "Ingresar";
            registar.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int )).Value = 0;
            registar.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar, 50)).Value = txtNombre.Text;
            registar.Parameters.Add(new SqlParameter("@Especie", SqlDbType.NVarChar, 50)).Value = txtEspecie.Text;
            registar.Parameters.Add(new SqlParameter("@Raza", SqlDbType.NVarChar, 50)).Value = txtRaza.Text;
            registar.Parameters.Add(new SqlParameter("@Fecha_Nacimiento", SqlDbType.Date)).Value = dateTime.Value;
            registar.Parameters.Add(new SqlParameter("@Propietario", SqlDbType.NVarChar, 50)).Value = txtPropietario.Text;

            try
            {
                registar.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conexion.Close();
            Limpiar();
            TablaPacientes();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexion.Open();

            SqlCommand eliminar = new SqlCommand("Crud_Paciente", conexion);
            eliminar.CommandType = CommandType.StoredProcedure;

            eliminar.Parameters.Add(new SqlParameter("@Opc", SqlDbType.NVarChar, 30)).Value = "Eliminar";
            eliminar.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = int.Parse(txtId.Text);
            eliminar.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar, 50)).Value = txtNombre.Text;
            eliminar.Parameters.Add(new SqlParameter("@Especie", SqlDbType.NVarChar, 50)).Value = txtEspecie.Text;
            eliminar.Parameters.Add(new SqlParameter("@Raza", SqlDbType.NVarChar, 50)).Value = txtRaza.Text;
            eliminar.Parameters.Add(new SqlParameter("@Fecha_Nacimiento", SqlDbType.Date)).Value = dateTime.Value;
            eliminar.Parameters.Add(new SqlParameter("@Propietario", SqlDbType.NVarChar, 50)).Value = txtPropietario.Text;

            try
            {
                int rowsAffected = eliminar.ExecuteNonQuery();

                if (rowsAffected > 0)
                {

                }
                else
                {
                    MessageBox.Show("El Id ingresada es incorrecta o no existe");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conexion.Close();
            Limpiar();
            TablaPacientes();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtEspecie.Text) || string.IsNullOrEmpty(txtRaza.Text) || string.IsNullOrEmpty(txtPropietario.Text) || dateTime.Value == null || string.IsNullOrEmpty(txtPropietario.Text))
            {
                MessageBox.Show("Faltan campos por llenar");
                return;
            }

            conexion.Open();

            SqlCommand modificar = new SqlCommand("Crud_Paciente", conexion);
            modificar.CommandType = CommandType.StoredProcedure;

            modificar.Parameters.Add(new SqlParameter("@Opc", SqlDbType.NVarChar, 30)).Value = "Modificar";
            modificar.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = int.Parse(txtId.Text);
            modificar.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar, 50)).Value = txtNombre.Text;
            modificar.Parameters.Add(new SqlParameter("@Especie", SqlDbType.NVarChar, 50)).Value = txtEspecie.Text;
            modificar.Parameters.Add(new SqlParameter("@Raza", SqlDbType.NVarChar, 50)).Value = txtRaza.Text;
            modificar.Parameters.Add(new SqlParameter("@Fecha_Nacimiento", SqlDbType.Date)).Value = dateTime.Value;
            modificar.Parameters.Add(new SqlParameter("@Propietario", SqlDbType.NVarChar, 50)).Value = txtPropietario.Text;

            try
            {
                modificar.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conexion.Close();

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox text = ctrl as TextBox;
                    text.Clear();
                }
            }
                Limpiar();
                TablaPacientes();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            conexion.Open();

            SqlCommand buscar = new SqlCommand("Crud_Paciente", conexion);
            buscar.CommandType = CommandType.StoredProcedure;

            buscar.Parameters.Add(new SqlParameter("@Opc", SqlDbType.NVarChar, 30)).Value = "Buscar";
            buscar.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = int.Parse(txtId.Text);
            buscar.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar, 50)).Value = txtNombre.Text;
            buscar.Parameters.Add(new SqlParameter("@Especie", SqlDbType.NVarChar, 50)).Value = txtEspecie.Text;
            buscar.Parameters.Add(new SqlParameter("@Raza", SqlDbType.NVarChar, 50)).Value = txtRaza.Text;
            buscar.Parameters.Add(new SqlParameter("@Fecha_Nacimiento", SqlDbType.Date)).Value = dateTime.Value;
            buscar.Parameters.Add(new SqlParameter("@Propietario", SqlDbType.NVarChar, 50)).Value = txtPropietario.Text;

            SqlDataReader leer = buscar.ExecuteReader();

            if (leer.HasRows)
            {
                while (leer.Read())
                {
                    txtId.Text = leer[0].ToString();
                    txtNombre.Text = leer[1].ToString();
                    txtEspecie.Text = leer[2].ToString();
                    txtRaza.Text = leer[3].ToString();
                    txtPropietario.Text = leer[5].ToString();
                    dateTime.Value = DateTime.Parse(leer[4].ToString());
                }
            }
            else
            {
                MessageBox.Show("El Id ingresada es incorrecta o no existe");
            }

            conexion.Close();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void txtEspecie_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void txtRaza_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void txtPropietario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; 
            }
        }
    }
}
