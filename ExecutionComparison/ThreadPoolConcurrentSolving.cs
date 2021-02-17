using System;
using System.Threading;
using ExecutionComparison;

namespace ExecutionComparison
{
    public class ThreadPoolConcurrentSolving : BaseConcurrentSolving
    {
        private readonly AutoResetEvent _notifier;

        public event Action SolvingFinished;

        public ThreadPoolConcurrentSolving(int threadAmount, int bound = 100, int iterations = 10_000) :
            base(threadAmount, bound, iterations) => _notifier = new AutoResetEvent(false);

        public override void Start()
        {
            for (int i = 0; i < ThreadAmount; i++)
            {
                ThreadPool.QueueUserWorkItem(WorkItem);
            }

            Stopwatch.Start();
            _notifier.WaitOne();
            Stopwatch.Stop();
            Console.WriteLine($"[ThreadPool] Elapsed: {Stopwatch.ElapsedMilliseconds}ms");

            SolvingFinished?.Invoke();
        }

        private void WorkItem(object state)
        {
            while (true)
            {
                if (TaskQueue.TryDequeue(out var rootCallback))
                {
                    rootCallback();
                }
                else
                {
                    _notifier.Set();
                }
            }
        }
    }
}
