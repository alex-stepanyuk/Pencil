using System;
using System.Windows;

namespace Pencil
{
    abstract class MyBaseShape
    {
        protected Guid _id;
        
        public abstract void Draw(int x1, int y1, int x2, int y2);
    }
}
