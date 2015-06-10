using System;
using System.Windows;

namespace Pencil
{
    abstract class MyBaseShape
    {
        protected Guid _id;

        public abstract Guid Id { get; set; }

        public abstract double X { get; set; }

        public abstract double Y { get; set; }
        
        public abstract void Draw(int x1, int y1, int x2, int y2);
    }
}
