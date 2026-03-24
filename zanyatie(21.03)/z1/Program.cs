using System;

namespace z1
{
    abstract class Shape
    {
        public abstract double GetArea();
    }

    class Circle : Shape
    {
        public double Radius;

        public Circle(double radius)
        {
            Radius = radius;
        }

        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
    }

    class Rectangle : Shape
    {
        public double Width;
        public double Height;

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double GetArea()
        {
            return Width * Height;
        }
    }

    class Triangle : Shape
    {
        public double Base;
        public double Height;

        public Triangle(double baseLength, double height)
        {
            Base = baseLength;
            Height = height;
        }

        public override double GetArea()
        {
            return 0.5 * Base * Height;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Shape[] shapes = new Shape[3];
            shapes[0] = new Circle(5);
            shapes[1] = new Rectangle(4, 6);
            shapes[2] = new Triangle(3, 4);

            Console.WriteLine("Площади фигур:");
            for (int i = 0; i < shapes.Length; i++)
            {
                Console.WriteLine(shapes[i].GetArea());
            }

            Console.ReadLine();
        }
    }
}