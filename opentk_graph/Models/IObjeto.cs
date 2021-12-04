using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opentk_graph
{  
    public interface IObjeto
    {
        void Rotate(double angle, double x, double y, double z);
        void Translate(double x, double y, double z);
        void Scale(double x, double y, double z);
        void Draw();

    }  
}
