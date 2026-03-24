using System;

namespace z4
{
    interface IDraw
    {
        void ApplyColor(string color);
    }

    interface IPaint
    {
        void ApplyColor(string color);
    }

    class GraphicEditor : IDraw, IPaint
    {
        void IDraw.ApplyColor(string color)
        {
            Console.WriteLine("Рисование цветом: " + color);
        }

        void IPaint.ApplyColor(string color)
        {
            Console.WriteLine("Закрашивание цветом: " + color);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GraphicEditor editor = new GraphicEditor();

            IDraw draw = editor;
            draw.ApplyColor("красный");

            IPaint paint = editor;
            paint.ApplyColor("синий");

            Console.ReadLine();
        }
    }
}