using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Cars2D
{
    class Target
    {
        public Vector2 position = new Vector2();
        public Target(float x , float y)
        {
            position.X = x;
            position.Y = y;
        }
        public Target(Vector2 vector)
        {
            position.X = vector.X;
            position.Y = vector.Y;
        }

        public static implicit operator Target(Vector2 v)
        {
            throw new NotImplementedException();
        }
    }
}
