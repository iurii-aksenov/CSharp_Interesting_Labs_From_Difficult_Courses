using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class ProductInfo
    {
        public int Id { get; }
        public Shape Shape { get; }
        public Color Color { get; }
        public Label Label { get; }
        public Pack Pack { get; }

        public ProductInfo(int id, Shape shape, Color color, Label label, Pack pack)
        {
            Id = id;
            Shape = shape;
            Color = Color;
            Label = label;
            Pack = pack;
        }

        public override string ToString()
        {
            string shape = Shape == Shape.None ? "" : $" Shape: {Shape}. ";
            string color = Color == Color.None ? "" : $" Color: {Color}. ";
            string label = Label == Label.None ? "" : $" Label: {Label}. ";
            string pack = Pack == Pack.None ? "" : $" Pack: {Pack}. ";
            return $"Product №:{Id}. " + shape + color + label + pack;
        }
    }

    public static class Converter
    {
        public static ProductInfo ToProductInfo(this Product product)
        {
            return new ProductInfo(product.Id, product.Shape, product.Color, product.Label, product.Pack);
        }
    }
}
