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
    public class Parte : IObjeto
    {
        private Vector3d center;
        private Vector3 color;
        private Matrix4d matrix;
        private bool transfomation;

        private double dx;
        private double dy;
        private double dz;

        private double cx;
        private double cy;
        private double cz;


        public Parte(double dx, double dy, double dz, double cx, double cy, double cz) {
            this.dx = dx;
            this.dy = dy;
            this.dz = dz;

            this.cx = cx;
            this.cy = cy;
            this.cz = cz;
            center = new Vector3d(0.0, 0.0, 0.0);
            color = new Vector3(1.0f, 1.0f, 1.0f);
            matrix = Matrix4d.Identity;
            transfomation = false;

        }

        public Vector3d getCenter() { return center; }
        public void setCenter(double x, double y, double z) { center.X = x; center.Y = y; center.Z = z; }
        public void setColor(float x, float y, float z) { color.X = x; color.Y = y; color.Z = z; }

        public void Draw(){

            
            double Dy = dy / 2;

            GL.PushMatrix();
           

            GL.LoadMatrix(ref matrix);
        
          
            GL.Color4(color.X, color.Y, color.Z,1f);

            GL.Begin(BeginMode.Polygon);//plano1 superior

            GL.Vertex3(cx - dx, cy + Dy, cz - dz);
            GL.Vertex3(cx - dx, cy + Dy, cz + dz);
            GL.Vertex3(cx + dx, cy + Dy, cz + dz);
            GL.Vertex3(cx + dx, cy + Dy, cz - dz);

            GL.End();

            GL.Begin(BeginMode.Polygon);//plano2 inferior

            GL.Vertex3(cx - dx, cy - Dy, cz - dz);
            GL.Vertex3(cx - dx, cy - Dy, cz + dz);
            GL.Vertex3(cx + dx, cy - Dy, cz + dz);
            GL.Vertex3(cx + dx, cy - Dy, cz - dz);

            GL.End();

            GL.Begin(BeginMode.Polygon);//plano1 frontal

            GL.Vertex3(cx - dx, cy + Dy, cz + dz);
            GL.Vertex3(cx + dx, cy + Dy, cz + dz);
            GL.Vertex3(cx + dx, cy - Dy, cz + dz);
            GL.Vertex3(cx - dx, cy - Dy, cz + dz);

            GL.End();

            GL.Begin(BeginMode.Polygon);//plano2 atras

            GL.Vertex3(cx - dx, cy + Dy, cz - dz);
            GL.Vertex3(cx + dx, cy + Dy, cz - dz);
            GL.Vertex3(cx + dx, cy - Dy, cz - dz);
            GL.Vertex3(cx - dx, cy - Dy, cz - dz);

            GL.End();


            GL.Begin(BeginMode.Polygon);//plano1 izquierda

            GL.Vertex3(cx - dx, cy + Dy, cz + dz);
            GL.Vertex3(cx - dx, cy + Dy, cz - dz);
            GL.Vertex3(cx - dx, cy - Dy, cz - dz);
            GL.Vertex3(cx - dx, cy - Dy, cz + dz);

            GL.End();

            GL.Begin(BeginMode.Polygon);//plano2 derecha

            GL.Vertex3(cx + dx, cy + Dy, cz + dz);
            GL.Vertex3(cx + dx, cy + Dy, cz - dz);
            GL.Vertex3(cx + dx, cy - Dy, cz - dz);
            GL.Vertex3(cx + dx, cy - Dy, cz + dz);

            GL.End();
                          
            GL.PopMatrix();

          


        }

        public void Rotate(double angle, double x, double y, double z)
        {

            Vector3d v = new Vector3d(x, y, z);
            Matrix4d rotate = Matrix4d.Rotate(v, MathHelper.DegreesToRadians(angle));//angulo en grados
            Matrix4d trans = Matrix4d.CreateTranslation(-center.X, -center.Y, -center.Z);
            Matrix4d trans2 = Matrix4d.CreateTranslation(center.X, center.Y, center.Z);

            Matrix4d aux = matrix;
            matrix = Matrix4d.Identity;

            matrix = Matrix4d.Mult(matrix, trans);
            matrix = Matrix4d.Mult(matrix, rotate);
            matrix = Matrix4d.Mult(matrix, trans2);

            if (transfomation) matrix = Matrix4d.Mult(matrix, aux);

            transfomation = true;


        }

        public void Rotate(double angle, double x, double y, double z, Vector3d cm)
        {

            Vector3d v = new Vector3d(x, y, z);
            Matrix4d rotate = Matrix4d.Rotate(v, MathHelper.DegreesToRadians(angle));//angulo en grados          
            Matrix4d trans = Matrix4d.CreateTranslation(-cm.X, -cm.Y, -cm.Z);
            Matrix4d trans2 = Matrix4d.CreateTranslation(cm.X, cm.Y, cm.Z);

            Matrix4d aux = matrix;
            matrix = Matrix4d.Identity;

            matrix = Matrix4d.Mult(matrix, trans);
            matrix = Matrix4d.Mult(matrix, rotate);
            matrix = Matrix4d.Mult(matrix, trans2);

            if (transfomation) matrix = Matrix4d.Mult(matrix, aux);
            
            transfomation = true;

        }

        public void Scale(double x, double y, double z)
        {

            Matrix4d scale = Matrix4d.Scale(x, y, z);

            Matrix4d trans = Matrix4d.CreateTranslation(-center.X, -center.Y, -center.Z);
            Matrix4d trans2 = Matrix4d.CreateTranslation(center.X, center.Y, center.Z);

            Matrix4d aux = matrix;
            matrix = Matrix4d.Identity;

            matrix = Matrix4d.Mult(matrix, trans);
            matrix = Matrix4d.Mult(matrix, scale);
            matrix = Matrix4d.Mult(matrix, trans2);

            if (transfomation) matrix = Matrix4d.Mult(matrix, aux);

            transfomation = true;

        }

        public void Scale(double x, double y, double z, Vector3d cm)
        {

            Matrix4d scale = Matrix4d.Scale(x, y, z);                      
            Matrix4d trans = Matrix4d.CreateTranslation(-cm.X, -cm.Y, -cm.Z);
            Matrix4d trans2 = Matrix4d.CreateTranslation(cm.X, cm.Y, cm.Z);

            Matrix4d aux = matrix;
            matrix = Matrix4d.Identity;

            matrix = Matrix4d.Mult(matrix, trans);
            matrix = Matrix4d.Mult(matrix, scale);
            matrix = Matrix4d.Mult(matrix, trans2);

            if (transfomation) matrix = Matrix4d.Mult(matrix, aux);

            transfomation = true;

        }

        public void Translate(double x, double y, double z)
        {
            matrix = Matrix4d.Mult(matrix, Matrix4d.CreateTranslation(x, y, z));
            transfomation = true;

        }

        public void LoadIdentity()
        {
            matrix = Matrix4d.Identity;
            transfomation = false;
        }

    }
}
