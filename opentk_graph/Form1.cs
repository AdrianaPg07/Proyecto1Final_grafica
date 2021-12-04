using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;


namespace opentk_graph
{
    public partial class Form1 : Form
    {
       
        private Frame frame;
        private List<String> objetos;
        private List<String> partes;
       
        private double angle;
        private double x;
        private double y;
        private double z;
        private String objectName;
        private String partName;
        private Transformacion transformation;
        private Elemento elemento;

        Thread backProcess;
        private bool state;

        public Form1()
        {
            
            InitializeComponent();
           
            objetos = null;
            partes = null;

            angle = x = y = z = 0;
            objectName = partName = "";
            transformation = Transformacion.none;
            elemento = Elemento.none;

            frame = new Frame(700, 700, "LearnOpenTK");
            backProcess = new Thread(new ThreadStart(onChange));
            state = true;
            backProcess.Start();


        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            frame.Run(60.0);
           

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {//permite seleccionar una escena

            if (this.comboBox1.SelectedItem.ToString().Equals("escena"))
            {
                
                loadObjetos();
                elemento = Elemento.escena;
            }else if (this.comboBox1.SelectedItem.ToString().Equals("none"))
            {
                
                clear();
            }
            
           
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            objetos = frame.objetos();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {//permite seleccionar un objeto
            loadPartes(this.listBox1.SelectedItem.ToString());
            elemento = Elemento.objeto;
            objectName = this.listBox1.SelectedItem.ToString();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {//permite seleccionar una parte
            elemento = Elemento.parte;
            partName = this.listBox2.SelectedItem.ToString();
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {//permite seleccionar una transformacion

            if (this.comboBox2.SelectedItem.ToString().Equals("translate"))
            {

                transformation = Transformacion.translate;

            } else if (this.comboBox2.SelectedItem.ToString().Equals("rotate"))
            {


                transformation = Transformacion.rotate;

            } else if (this.comboBox2.SelectedItem.ToString().Equals("scale"))
            {

                transformation = Transformacion.scale;

            }

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {//value of angle
            angle =(double) this.trackBar1.Value;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {// value of x
            x = (double)this.trackBar2.Value;
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {// value of y
            y = (double)this.trackBar3.Value;
        }

        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {// value of z
            z = (double)this.trackBar4.Value;
        }

        private void loadObjetos()
        {
            this.listBox1.Items.Clear();
            objetos = frame.objetos();
            foreach (String o in objetos)
            {
                this.listBox1.Items.Add(o);
            }

        }

        private void loadPartes(String objectname)
        {
            this.listBox2.Items.Clear();
            partes = frame.partes(objectname);
            foreach (String p in partes)
            {
                this.listBox2.Items.Add(p);
            }

        }

        private void clear()
        {
            angle = x = y = z = 0;
            objectName = partName = "";
            transformation = Transformacion.none;
            elemento = Elemento.none;
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
            this.trackBar1.Value = 1;
            this.trackBar2.Value = 1;
            this.trackBar3.Value = 1;
            this.trackBar4.Value = 1;
        }

        private void onChange()
        {

            while (state)
            {
               
                if (elemento == Elemento.escena)
                {
                    

                    if (transformation == Transformacion.translate)
                    {
                        frame.TranslateEscena(x, y, z);
                    }
                    else if (transformation == Transformacion.rotate)
                    {
                        frame.RotateEscena(angle, x, y, z);
                    }
                    else if (transformation == Transformacion.scale)
                    {
                        frame.EscaleEscena(x, y, z);
                    }

                }
                else if (elemento == Elemento.objeto)
                {

                    if (transformation == Transformacion.translate)
                    {
                        frame.TranslateObjeto(objectName, x, y, z);
                    }
                    else if (transformation == Transformacion.rotate)
                    {
                        frame.RotateObjeto(objectName, angle, x, y, z);
                    }
                    else if (transformation == Transformacion.scale)
                    {
                        frame.EscaleObjeto(objectName, x, y, z);
                    }

                }
                else if (elemento == Elemento.parte)
                {
                    //Console.WriteLine("parte");
                    if (transformation == Transformacion.translate)
                    {
                        frame.TranslateParte(objectName, partName, x, y, z);
                    }
                    else if (transformation == Transformacion.rotate)
                    {
                        frame.RotateParte(objectName, partName, angle, x, y, z);
                    }
                    else if (transformation == Transformacion.scale)
                    {
                        frame.EscaleParte(objectName, partName, x, y, z);
                    }

                }

            }

            

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            state = false;
           // backProcess.Suspend();
            //backProcess = null;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }
    }
}
