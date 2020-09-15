using System.IO;
using System.Xml.Serialization;
using TracerUtils;

namespace ConsoleTestApp
{
    public class XMLSerializer : ISerializer
    {
        public void Serialize(TextWriter writer, TraceResult traceResult)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TraceResult));
            xmlSerializer.Serialize(writer, traceResult);
        }
    }
}
