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
    class Escena: IObjeto
    {

        private Hashtable h; //hashtable<Objeto>
        private Vector3d center;
        
        public Escena()
        {
            h = new Hashtable();
            center = new Vector3d(0.0,0.0,0.0);
        }

        public Vector3d getCenter() { return center; }
        public void setCenter(double x, double y, double z) { center.X = x; center.Y = y; center.Z = z; }

        public void add(String key, Objeto o)
        {

            try
            {
                h.Add(key, o);
            }
            catch
            {
                //errores ???
            }

        }

        public void change(String key, Objeto o)
        {

            if (h.ContainsKey(key))
            {
                h[key] = o;
            }

        }

        public void remove(String key)
        {

            if (h.ContainsKey(key))
            {
                h.Remove(key);
            }
        }

        public ICollection getCollection()
        {
            return h.Values;
        }

        public List<String> lista()
        {
            List<String> l = new List<string>();

            ICollection values = h.Keys;
            foreach (String o in values)
            {
                l.Add(o);
            }
            return l;
        }

        public Objeto getObjeto(String key)
        {

            if (h.ContainsKey(key))
            {
                return (Objeto)h[key];
            }
            return null;
        }

        public void Draw()
        {
            ICollection values = this.getCollection();

            foreach (Objeto o in values)
            {
                o.Draw();
            }

        }

        public void Rotate(double angle, double x, double y, double z)
        {
            ICollection values = h.Values;

            foreach (Objeto o in values)
            {
                o.Rotate(angle, x, y, z, center);
            }

        }

        public void Scale(double x, double y, double z)
        {
           
            ICollection values = h.Values;

            foreach (Objeto o in values)
            {
                o.Scale( x, y, z, center);
            }

        }

        public void Translate(double x, double y, double z)
        {

            ICollection values = h.Values;

            foreach (Objeto o in values)
            {
                o.Translate(x, y, z);
            }

        }


        public void LoadIdentity() {
            ICollection values = h.Values;

            foreach (Objeto o in values)
            {
                o.LoadIdentity();
            }
        }


    }
}
