using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Serialization;

namespace TracerUtils
{
    [Serializable]
    public class MethodData
    {
        public MethodData()
        {

        }

        public MethodData(MethodBase methodBase)
        {
            Name = methodBase.Name;
            Class = methodBase.ReflectedType.Name;
            Methods = new List<MethodData>();
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("class")]
        public string Class { get; set; }
        [JsonPropertyName("time")]
        public long ElapsedTime { get; set; }
        [JsonPropertyName("methods")]
        public List<MethodData> Methods { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public Stopwatch Stopwatch { get; set; }
    }
}
