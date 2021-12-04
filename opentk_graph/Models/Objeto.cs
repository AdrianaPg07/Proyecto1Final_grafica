using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace opentk_graph
{
    public abstract class Objeto: IObjeto
    {
        private Hashtable h; //hashtable<Parte>
        private Vector3d center;

        public Objeto() { 
            h= new Hashtable();
            center = new Vector3d(0.0, 0.0, 0.0);
        }

        public Vector3d getCenter() { return center; }
        public void setCenter(double x, double y, double z) { center.X = x; center.Y = y; center.Z = z; }

        public void add(String key ,Parte p) {

            try
            {
                h.Add(key, p);
            }
            catch
            {
                //errores ???
            }
            
        }

        public void change(String key, Parte p)
        {
            if (h.ContainsKey(key))
            {
                h[key] = p;
            }

        }

        public void remove(String key)
        {
            
            if (h.ContainsKey(key))
            {
                h.Remove(key);
            }
        }

        public ICollection getCollection() {
            return h.Values;
        }

        public List<String> lista()
        {
            List<String> l = new List<string>();

            ICollection values = h.Keys;
            foreach (String p in values)
            {
                l.Add(p);
            }
            return l; 
        }

        public Parte getParte(String key)
        {
            if (h.ContainsKey(key))
            {
               return (Parte)h[key];
            }
            return null;
        }


        /////// implementacion de la interfaz IObjeto //////////////

        public void Draw()
        {   
            ICollection values = h.Values;
 
            foreach (Parte p in values)
            {
                p.Draw();
            }

        }

        public void Rotate(double angle, double x, double y, double z)
        {
            ICollection values = h.Values;

            foreach (Parte p in values)
            {
                p.Rotate(angle, x, y, z, center);
            }

        }

        public void Rotate(double angle, double x, double y, double z, Vector3d cm)
        {
            ICollection values = h.Values;

            foreach (Parte p in values)
            {
                p.Rotate(angle, x, y, z, cm);
            }

        }

        public void Scale(double x, double y, double z)
        {
            ICollection values = h.Values;

            foreach (Parte p in values)
            {
                p.Scale(x, y, z, center);
            }

        }

        public void Scale(double x, double y, double z, Vector3d cm)
        {
            ICollection values = h.Values;

            foreach (Parte p in values)
            {
                p.Scale(x, y, z,cm);
            }

        }

        public void Translate(double x, double y, double z)
        {
            ICollection values = h.Values;

            foreach (Parte p in values)
            {
                p.Translate(x, y, z);
            }

        }

        public void LoadIdentity()
        {
            ICollection values = h.Values;

            foreach (Parte p in values)
            {
                p.LoadIdentity();
            }
        }

    }
}
