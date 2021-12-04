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
    class Home:Objeto
    {
       

        public Home(double dx, double dy,double dz, double cx, double cy, double cz) : base()
        {
            this.setCenter(cx, cy, cz);  // punto de origen x, y, z
            
            double px, py, pz;//px="prima de x"......


            px = cx;
            py = cy + (dy / 4)+ (dy * 0.05);
            pz = cz - (dz - (dz * 0.1));
            this.add("ventana", new Parte(dx-6, dy/4, (dz*0.1), px+4, py-16,  pz+18)  );
            this.getParte("ventana").setColor(0.0f,0.5f,1.0f);
            this.getParte("ventana").setCenter(px,cy + dy * 0.05, pz);//y+=5%
           // Console.WriteLine("v=" + (cy + (dy * 0.05)));

            pz = cz - (dz/2);
            this.add("techo1", new Parte(dx, dy*0.1, dz, cx, cy, cz) );
            this.getParte("techo1").setColor(0.5f, 0.5f, 1.5f);
            this.getParte("techo1").setCenter(cx, cy, pz-(dz*0.4));

            //piso
            this.add("piso1", new Parte(dx, (dy * 0.1)-2, dz, cx, cy-16, cz));
            this.getParte("piso1").setColor(1.0f, 1.0f, 1.0f);
            this.getParte("piso1").setCenter(cx, cy, pz - (dz * 0.4));

            double width = dx * 0.1;//recalculo del nuevo ancho de x  DE 0.1
            double height = dy / 2;//recalculo del nuevo alto de y
            double depth = dz * 0.1;//recalculo del nuevo ancho de z

            double x = dx / 4;//recalculo del nuevo ancho respecto x
            double z = dz /4; //recalculo de la nueva profundidad respecto a z         

            px = cx - (dx - width);
            py = cy - (dy * 0.05);
            pz = cz - (dz - depth);//
            this.add("pared1", new Parte(width+8, height, depth, px+8, cy - (dy / 4) - dy * 0.05, pz));
            this.getParte("pared1").setColor(1.0f, 0.0f, 0.0f);  //RED
            this.getParte("pared1").setCenter(px, py, pz);
            
            pz = cz + (dz - depth);//
            this.add("pared2", new Parte(width, height, depth-10, px, cy - (dy / 4) - dy * 0.05, pz-8));
            this.getParte("pared2").setColor(0.0f, 1.0f, 0.0f);  //GREEN
            this.getParte("pared2").setCenter(px, py, pz);
            
            px = cx + (dx - width);
            this.add("pared3", new Parte(width+5, height, depth, px-5, cy - (dy / 4) - dy * 0.05, pz));
            this.getParte("pared3").setColor(1.0f, 1.0f, 0.0f);   //YELLOW
            this.getParte("pared3").setCenter(px, py, pz);
            
            pz = cz - (dz - depth);//
            this.add("pared4", new Parte(width, height, depth+8, px, (cy - (dy / 4) - dy * 0.05), pz+8));
            this.getParte("pared4").setColor(0.0f, 0.0f, 1.0f);
            this.getParte("pared4").setCenter(px, py, pz);
            

        }
 
        

        
        
    }
}
