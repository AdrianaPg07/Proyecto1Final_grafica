using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace opentk_graph
{
    enum Elemento { escena, objeto, parte, none };
    enum Transformacion { translate, rotate, scale, none };
    public class Frame : GameWindow
    {
        private Escena escena;   

        private Transformacion transformacion;
        private Elemento elemento;

        private String objetName;
        private String partName;

        private double angle;
        private double x;
        private double y;
        private double z;

      
        public Frame(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
           
           escena = new Escena();
           escena.add("home",new Home(10, 30, 10, 0.0, 0.0, 0.0));
          
            angle = x = y = z = 0;
            objetName = partName = "";
            transformacion = Transformacion.none;
            elemento = Elemento.none;

        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            base.OnLoad(e);
        }


        protected override void OnResize(EventArgs e)
        {
            float aspectRatio = 1.0f;

            if ((Width > 0) && (Height > 0))
            {
                if (Width > Height)
                {
                    aspectRatio = Width / Height;
                }
                else if (Width < Height)
                {
                    aspectRatio = Height / Width;
                }
            }


            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            Matrix4 matrix = Matrix4.Perspective(45.0f, aspectRatio, 1.0f, 100.0f);
            GL.LoadMatrix(ref matrix);
            //GL.Ortho(0.0, 50.0, 0.0, 50.0, -1.0, 1.0);
            GL.MatrixMode(MatrixMode.Modelview);


            base.OnResize(e);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            if(angle>360)angle = 0; 
           // GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);                          

            escena.Translate(0.0, 0.0, -50.0);

            aplicarTransformacion();
        
            
            escena.Draw();

           
         
            //Code goes here.

            Context.SwapBuffers();
            base.OnRenderFrame(e);
            GL.Flush();

          
            escena.LoadIdentity();//resetTransfors

        }

    

       
       protected override void OnUnload(EventArgs e)
       {
            escena = null;
           base.OnUnload(e);
       }


        public void TranslateEscena(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            transformacion = Transformacion.translate;
            elemento = Elemento.escena;
        }
        public void TranslateObjeto(String objeto, double x, double y, double z)
        {
            this.objetName = objeto;
            this.x = x;
            this.y = y;
            this.z = z;
            transformacion = Transformacion.translate;
            elemento = Elemento.objeto;
        }
        public void TranslateParte(String objeto, String parte, double x, double y, double z)
        {
            this.objetName = objeto;
            this.partName = parte;
            this.x = x;
            this.y = y;
            this.z = z;
            transformacion = Transformacion.translate;
            elemento = Elemento.parte;
        }

        public void RotateEscena(double angle, double x, double y, double z)
        {
            this.angle = angle;
            this.x = x;
            this.y = y;
            this.z = z;
            transformacion = Transformacion.rotate;
            elemento = Elemento.escena;
        }
        public void RotateObjeto(String objeto,double angle, double x, double y, double z)
        {    
            this.objetName = objeto;
            this.angle = angle;
            this.x = x;
            this.y = y;
            this.z = z;
            transformacion = Transformacion.rotate;
            elemento = Elemento.objeto;
        }
        public void RotateParte(String objeto, String parte, double angle, double x, double y, double z)
        {
            this.objetName = objeto;
            this.partName = parte;
            this.angle = angle;
            this.x = x;
            this.y = y;
            this.z = z;
            transformacion = Transformacion.rotate;
            elemento = Elemento.parte;
        }

        public void EscaleEscena(double x, double y, double z)
        {     
            this.x = x;
            this.y = y;
            this.z = z;
            transformacion = Transformacion.scale;
            elemento = Elemento.escena;
        }
        public void EscaleObjeto(String objeto, double x, double y, double z)
        {
            this.objetName = objeto;
            this.x = x;
            this.y = y;
            this.z = z;
            transformacion = Transformacion.scale;
            elemento = Elemento.objeto;
        }
        public void EscaleParte(String objeto, String parte, double x, double y, double z)
        {
            this.objetName = objeto;
            this.partName = parte;
            this.x = x;
            this.y = y;
            this.z = z;
            transformacion = Transformacion.scale;
            elemento = Elemento.parte;
        }

        private void aplicarTransformacion()
        {

            if (elemento == Elemento.escena) {//

                if (transformacion == Transformacion.translate)
                {
                    escena.Translate(this.x, this.y, this.z);
                }
                else if (transformacion == Transformacion.rotate)
                {
                    escena.Rotate(angle, this.x, this.y, this.z);
                }
                else if (transformacion == Transformacion.scale)
                {
                    escena.Scale(this.x, this.y, this.z);
                }

            } else if (elemento == Elemento.objeto) {//

                if (transformacion == Transformacion.translate)
                {
                    if(escena.getObjeto(this.objetName)!=null)
                        escena.getObjeto(this.objetName).Translate(this.x, this.y, this.z);
                }
                else if (transformacion == Transformacion.rotate)
                {
                    if (escena.getObjeto(this.objetName) != null)
                        escena.getObjeto(this.objetName).Rotate(this.angle, this.x, this.y, this.z);
                }
                else if (transformacion == Transformacion.scale)
                {
                    if (escena.getObjeto(this.objetName) != null)
                        escena.getObjeto(this.objetName).Scale(this.x, this.y, this.z);
                }

            } else if (elemento == Elemento.parte) {//

                if (transformacion == Transformacion.translate)
                {
                    if ( (escena.getObjeto(this.objetName) != null) &&(escena.getObjeto(this.objetName).getParte(this.partName) != null) )
                        escena.getObjeto(this.objetName).getParte(this.partName).Translate(this.x, this.y, this.z);
                }
                else if (transformacion == Transformacion.rotate)
                {
                    if ((escena.getObjeto(this.objetName) != null) && (escena.getObjeto(this.objetName).getParte(this.partName) != null))
                        escena.getObjeto(this.objetName).getParte(this.partName).Rotate(this.angle, this.x, this.y, this.z);
                }
                else if (transformacion == Transformacion.scale)
                {
                    if ((escena.getObjeto(this.objetName) != null) && (escena.getObjeto(this.objetName).getParte(this.partName) != null))
                        escena.getObjeto(this.objetName).getParte(this.partName).Scale(this.x, this.y, this.z);
                }

            }

            clear();
        

        }
        private void clear() {

            angle = x = y = z = 0;
            objetName = partName = "";
            transformacion = Transformacion.none;
            elemento = Elemento.none;

        }

        public Objeto getObjeto(String objectname)
        {
            return escena.getObjeto(objectname);
            
        }
        public List<String> objetos() {
            return escena.lista();
        }

        public List<String> partes(String objectname)
        {
            if (escena.getObjeto(objectname) == null) return new List<String>();
            return escena.getObjeto(objectname).lista();
        }

      
    }
}
