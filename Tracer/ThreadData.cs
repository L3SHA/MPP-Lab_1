using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TracerUtils
{
    [Serializable]
    public class ThreadData
    {
        public ThreadData()
        {

        }

        public ThreadData(int id, long time, List<MethodData> methods)
        {
            Id = id;
            Time = time;
            Methods = methods;
        }

        [JsonPropertyName("time")]
        public long Time { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("methods")]
        public List<MethodData> Methods { get; set; }
    }
}
