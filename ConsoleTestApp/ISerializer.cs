using System.IO;
using TracerUtils;

namespace ConsoleTestApp
{
    public interface ISerializer
    {
        void Serialize(TextWriter writer, TraceResult traceResult);
    }
}
