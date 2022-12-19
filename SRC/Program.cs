// See https://aka.ms/new-console-template for more information
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;


string[] arguments;
string JsonData;
XmlDocument XmlData = new XmlDocument();
System.IO.StreamWriter StreamW;
System.IO.StreamReader StreamR;

StringBuilder StrBuilder = new StringBuilder();
XmlWriterSettings XmlSettings = new XmlWriterSettings
{
    Indent = true,
    IndentChars = "  ",
    NewLineChars = "\r\n",
    NewLineHandling = NewLineHandling.Replace
};

arguments = Environment.GetCommandLineArgs();

if (arguments.Length == 1)
{
    Console.WriteLine("*********************");
    Console.WriteLine("*    JsonX  V.0.03  *");
    Console.WriteLine("*********************");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("Simple command line Json file converter to Xml file.  By Angel Mier");
    Console.WriteLine("");
    Console.WriteLine("Usage:");
    Console.WriteLine("JsonX <Json File> <Output Path>");
    Console.WriteLine("Example:");
    Console.WriteLine("JsonX PIC16F17126.json PIC16F17126.xml");
}
else if (arguments.Length == 3)
{
    try 
    {
        StreamR = File.OpenText(arguments[1]);
        JsonData = StreamR.ReadToEnd();
        StreamR.Close();

        using (var reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(JsonData), XmlDictionaryReaderQuotas.Max))
    {
        XElement xml = XElement.Load(reader);
        XmlData.LoadXml(xml.ToString());
    }

        using (XmlWriter writer = XmlWriter.Create(StrBuilder, XmlSettings))
        {
            XmlData.Save(writer);
        }


        StreamW = File.CreateText(arguments[2]);
        StreamW.WriteLine(StrBuilder.ToString());
        StreamW.Close();

    }
    catch
    {
        Console.WriteLine("Error while converting Json file.");
    }
}
else
{
    Console.WriteLine("Wrong arguments.");
    Console.WriteLine("");
    Console.WriteLine("Usage:");
    Console.WriteLine("JsonX <Json File> <Output Path>");
}



