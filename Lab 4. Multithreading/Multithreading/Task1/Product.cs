using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Product
    {
        public int Id { get; set; } = -1;
        public Shape Shape { get; set; } = Shape.None;
        public Color Color { get; set; } = Color.None;
        public Label Label { get; set; } = Label.None;
        public Pack Pack { get; set; } = Pack.None;

        public override string ToString()
        {
            string shape = Shape == Shape.None ? "" : $" Shape: {Shape}. ";
            string color = Color == Color.None ? "" : $" Color: {Color}. ";
            string label = Label == Label.None ? "" : $" Label: {Label}. ";
            string pack = Pack == Pack.None ? "" : $" Pack: {Pack}. ";
            return $"Product №:{Id}. "+shape+color+label+pack;
        }

    }

    public enum Shape
    {
        None = 0,
        Circle,
        Square,
        Oval,
        Triangle,
        Rectangle
    }

    public enum Color
    {
        None = 0,
        Red,
        Pink,
        Orange,
        Yellow,
        Green,
        Sky,
        Purple,
        Blue,
        Brown,
        White,
        Black
    }

    public enum Label
    {
        None = 0,
        L1111 = 1,
        L1110 = 2,
        L1101 = 3,
        L1011 = 4,
        L0111 = 5
    }

    public enum Pack
    {
        None = 0,
        Box,
        Cellophane,
        Paper,
        Plastic,
        Membrane
    }
}
