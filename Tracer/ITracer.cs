namespace TracerUtils
{
    public interface ITracer
    {
        public void StartTrace();

        public void StopTrace();

        public TraceResult GetTraceResult();
    }
}
