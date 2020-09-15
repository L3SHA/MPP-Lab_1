using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;


namespace TracerUtils
{
    public class Tracer
    {
        public ConcurrentDictionary<int, List<MethodData>> threads;

        private ConcurrentDictionary<int, Stack<MethodData>> threadStacks;

        public Tracer()
        {
            threads = new ConcurrentDictionary<int, List<MethodData>>();
            threadStacks = new ConcurrentDictionary<int, Stack<MethodData>>();
        }

        public void StartTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            var methodData = new MethodData(methodBase);

            if (threads.TryAdd(threadId, null))
            {
                threads.TryUpdate(threadId, new List<MethodData>(), null);
                threadStacks.TryAdd(threadId, new Stack<MethodData>());

            }

            Stack<MethodData> threadStack;

            threadStacks.TryGetValue(threadId, out threadStack);

            if (threadStack.Count != 0)
            {
                MethodData previousMethod = threadStack.Pop();
                previousMethod.Methods.Add(methodData);
                threadStack.Push(previousMethod);
                threadStack.Push(methodData);
            }
            else
            {
                threadStack.Push(methodData);
            }


        }

        public void StopTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Stack<MethodData> threadStack;
            threadStacks.TryGetValue(threadId, out threadStack);
            MethodData methodData = threadStack.Pop();
            methodData.Stopwatch.Stop();
            methodData.ElapsedTime = methodData.Stopwatch.ElapsedMilliseconds;
            if (threadStack.Count == 0)
            {
                List<MethodData> methods;
                threads.TryGetValue(threadId, out methods);
                methods.Add(methodData);
            }
        }

        public TraceResult GetTraceResult()
        {
            return new TraceResult(threads);
        }
    }
}
