using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Text;


namespace ex04
{
    public abstract class Shape
    {
        public string Colour { get; set; }
        public abstract double Area
        {
            get;
        }
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }

        public override double Area
        {
            get => Math.Pow(Radius, 2.0) * Math.PI; 
        }
    }

    public class Rectangle : Shape
    {
        public double Height { get; set; }
        public double Width { get; set; }

        public override double Area
        {
            get => Height * Width;
        }
    }

    public class Shapes
    {
        [XmlElement(typeof(Circle))]
        [XmlElement(typeof(Rectangle))]
        public List<Shape> shapes {get; set;}
    }

    class App
    {
        static void Main(string [] args)
        {
            Shapes listOfShapes = new Shapes();
            listOfShapes.shapes = new List<Shape>
            {
                new Circle{ Colour = "Red", Radius = 2.5 },
                new Rectangle { Colour = "Purple", Height = 20.0, Width = 10.0 },
                new Circle{ Colour = "Green", Radius = 8.0 },
                new Circle{ Colour = "Purple", Radius = 12.3 },
                new Rectangle { Colour = "Blue", Height = 45.0, Width = 18.0 },
            };

            // Serialize 
            XmlSerializer serializerXml = new XmlSerializer(typeof(Shapes));
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            serializerXml.Serialize(sw, listOfShapes);
            string res = sw.GetStringBuilder().ToString();
            File.WriteAllText("foo.xml", res);

            // Deserialize 
            File.ReadAllText("foo.xml");
            var loadedShapes = serializerXml.Deserialize(XmlReader.Create(new StringReader(File.ReadAllText("wowo.xml")))) as Shapes;

            foreach(Shape item in loadedShapes.shapes)
            {
                Console.WriteLine("{0} is {1} and has an area of {2:N2}",
                        item.GetType().Name,
                        item.Colour,
                        item.Area);
            }


        }
    }
}

