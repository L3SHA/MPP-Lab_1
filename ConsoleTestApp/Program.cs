using System;
using System.Threading;
using System.IO;
using TracerUtils;

namespace ConsoleTestApp
{
    public class Useless
    {
        private Tracer tracer;

        public Useless(Tracer tracer)
        {
            this.tracer = tracer;
        }

        public void Method1()
        {
            tracer.StartTrace();
            Method2();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        public void Method2()
        {
            tracer.StartTrace();
            Method3();
            Thread.Sleep(20);
            tracer.StopTrace();
        }

        public void Method3()
        {
            tracer.StartTrace();
            Thread.Sleep(10);
            tracer.StopTrace();
        }

        public void Method4()
        {
            tracer.StartTrace();
            Thread.Sleep(50);
            tracer.StopTrace();
        }
    }

    class Program
    {
        
        private static ISerializer serializer;
        private static Tracer tracer = new Tracer();
        private static TraceResult traceResult;
        
        static void Main(string[] args)
        {
            Useless useless = new Useless(tracer);

            Thread thread1 = new Thread(new ThreadStart(useless.Method1));
            Thread thread2 = new Thread(new ThreadStart(useless.Method2));

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            traceResult = tracer.GetTraceResult();

            TextWriter json = new StringWriter();
            serializer = new JSONSerializer();
            serializer.Serialize(json, traceResult);

            Console.WriteLine(json);

            using (var fs = new FileStream("test.json", FileMode.Create))
            using (var sw = new StreamWriter(fs))
            {
                serializer.Serialize(sw, traceResult);
            }

            TextWriter xml = new StringWriter();
            serializer = new XMLSerializer();
            serializer.Serialize(xml, traceResult);

            Console.WriteLine(xml);

            using (var fs = new FileStream("test.xml", FileMode.Create))
            using (var sw = new StreamWriter(fs))
            {
                serializer.Serialize(sw, traceResult);
            }

        }
    }

}
