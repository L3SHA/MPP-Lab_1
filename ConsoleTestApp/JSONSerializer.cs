using System.IO;
using System.Text.Json;
using TracerUtils;

namespace ConsoleTestApp
{
    public class JSONSerializer : ISerializer
    {
        public void Serialize(TextWriter writer, TraceResult traceResult)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            writer.WriteLine(JsonSerializer.Serialize(traceResult, options));
        }
    }
}
