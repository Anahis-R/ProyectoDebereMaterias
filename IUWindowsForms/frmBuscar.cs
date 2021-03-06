﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IUWindowsForms
{
    public partial class frmBuscar : Form
    {
        public frmBuscar()
        {
            InitializeComponent();
        }
        private void cargarComboEstudiantes()
        {
            this.cmbcodigo.DataSource = ProyectoMaterias.MateriasDAO.getAll();
            this.cmbcodigo.DisplayMember = "estudiantes";
            this.cmbcodigo.ValueMember = "codMaterias";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String codMaterias = this.cmbcodigo.Text;

            ProyectoMaterias.Materias p = new ProyectoMaterias.Materias();
            p = ProyectoMaterias.MateriasDAO.GetPersona(codMaterias);

            //cargar datos en los cuadros de texto
            this.txtcodigo.Text = p.codMaterias;
            this.txtNombre.Text = p.nombreMateria;
            this.cmbNivel.Text = p.nivel.ToString();
            this.cmbCarrera.Text = p.carrera;
        }

        private void frmBuscar_Load(object sender, EventArgs e)
        {
            this.cargarComboEstudiantes();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (this.txtcodigo.Text.Length == 0)
            {
                MessageBox.Show("No hay cedula seleccionada");
                return;
            }
            ProyectoMaterias.Materias persona = new ProyectoMaterias.Materias();
            persona.codMaterias = this.txtcodigo.Text;
            persona.nombreMateria = this.txtNombre.Text;
            persona.nivel = int.Parse(this.cmbNivel.Text);
            persona.carrera = this.cmbCarrera.Text;


            int x = ProyectoMaterias.MateriasDAO.actualizar(persona);

            if (x > 0)
            {
                this.cargarComboEstudiantes();
                MessageBox.Show("Registro Aactualizar con exito!");
            }

            else
                MessageBox.Show("No se pudo actualizar el registro!");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Confirme", "Esta seguro que desea eliminar este registro?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.No)
            {
                return;
            }
            int x = ProyectoMaterias.MateriasDAO.eliminar(this.txtcodigo.Text);
            if (x > 0)
            {
                this.encerar();
                this.cargarComboEstudiantes();
                MessageBox.Show("Registro eliminado con exito");
            }
            else
                MessageBox.Show("No se pudo borrar el registro eliminado con exito");
        }
        private void encerar()
        {
            this.txtcodigo.Text = "";
            this.txtNombre.Text = "";
            this.cmbNivel.Text = "0";
            this.cmbCarrera.Text = "";
        }
    }
}
