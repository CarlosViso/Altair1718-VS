﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicaNegocioyADatos.Entidades;
using LogicaNegocioyADatos;

namespace InterfazUsuario
{
    public partial class AnadirUsuario : Form
    {
        Usuario usu;

        public Usuario Usu
        {
            get
            {
                return usu;
            }

            set
            {
                usu = value;
            }
        }

        public AnadirUsuario()
        {
            InitializeComponent();
        }

        private void AnadirUsuario_Load(object sender, EventArgs e)
        {
            cbAcceso.Items.Insert(0, "0. Deshabilitado");
            cbAcceso.Items.Insert(1, "1. Admin");
            cbAcceso.Items.Insert(2, "2. Usuario");
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            if (!HayErrorEnFormulario())
            {
                this.usu = new Usuario();

                usu.Login = txbUsuario.Text;
                usu.Password = txbPass.Text;
                usu.Nombre = txbNombre.Text;
                usu.Apellidos = txbApellidos.Text;
                cbAcceso.SelectedIndex = Convert.ToInt32(usu.Acceso);

                if (LNyAD.BuscarUsuario(txbUsuario.Text) != null)
                {
                    errorProvider1.SetError(txbUsuario, "El usuario está repetido\n");
                    MessageBox.Show("Ya existe un usuario con el mismo nombre registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txbPass.Text = String.Empty;
                    btnRegistro.Focus();
                }

                else

                {
                    LNyAD.AgregarUsuario(usu);
                    MessageBox.Show("El usuario se ha insertado correctamente", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private bool HayErrorEnFormulario()
        {
            errorProvider1.Clear();

            string texto = "";
            bool error = false;

            if (txbUsuario.ForeColor == Color.DarkRed || txbUsuario.Text == String.Empty)
            {
                texto = "El nombre de usuario está vacío\n";
                errorProvider1.SetError(txbUsuario, "El campo está vacío");
                btnRegistro.Focus();
                error = true;
            }

            if (txbPass.ForeColor == Color.DarkRed || txbPass.Text == String.Empty)
            {
                texto = "La contraseña está vacía\n";
                errorProvider1.SetError(txbPass, "El campo está vacío");
                btnRegistro.Focus();
                error = true;
            }

            if (txbNombre.ForeColor == Color.DarkRed || txbNombre.Text == String.Empty)
            {
                texto = "El nombre está vacío\n";
                errorProvider1.SetError(txbNombre, "El campo está vacío");
                btnRegistro.Focus();
                error = true;
            }

            if (txbApellidos.ForeColor == Color.DarkRed || txbApellidos.Text == String.Empty)
            {
                texto = "El nombre está vacío\n";
                errorProvider1.SetError(txbApellidos, "El campo está vacío");
                btnRegistro.Focus();
                error = true;
            }

            if (texto != String.Empty)
            {
                MessageBox.Show(texto, "Los campos están vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                error = true;
            }

            return error;
        }
    }
}
