using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Collections.Concurrent;

namespace TracerUtils
{
    [Serializable]
    public class TraceResult
    {
        public TraceResult()
        {

        }

        [JsonPropertyName("threads")]
        public List<ThreadData> Threads { get; set; }

        public TraceResult(ConcurrentDictionary<int, List<MethodData>> threads)
        {
            Threads = new List<ThreadData>();
            foreach (int threadId in threads.Keys)
            {
                long elapsedTime = 0;
                List<MethodData> methods;
                threads.TryGetValue(threadId, out methods);
                foreach (MethodData method in methods)
                {
                    elapsedTime += method.ElapsedTime;
                }
                Threads.Add(new ThreadData(threadId, elapsedTime, methods));
            }

        }

    }
}
