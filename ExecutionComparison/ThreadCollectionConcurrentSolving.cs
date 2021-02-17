using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ExecutionComparison;

namespace ExecutionComparison
{
    public class ThreadCollectionConcurrentSolving : BaseConcurrentSolving
    {
        private int _threadCounter;
        private readonly List<Thread> _threads;

        public event Action SolvingFinished;

        public ThreadCollectionConcurrentSolving(int threadAmount, int bound = 100, int iterations = 10_000) :
            base(threadAmount, bound, iterations)
        {
            _threads = Enumerable.Range(0, ThreadAmount)
                .Select(i => new Thread(WorkItem))
                .ToList();
        }

        public override void Start()
        {
            _threads.ForEach(thread => thread.Start());
            Stopwatch.Start();
        }

        private void WorkItem()
        {
            while (true)
            {
                if (TaskQueue.TryDequeue(out var rootCallback))
                {
                    rootCallback();
                }
                else
                {
                    Interlocked.Increment(ref _threadCounter);
                    if (_threadCounter == ThreadAmount)
                    {
                        Stopwatch.Stop();
                        Console.WriteLine($"[Thread Collection] Elapsed: {Stopwatch.ElapsedMilliseconds}ms");

                        SolvingFinished?.Invoke();
                    }
                    break;
                }
            }
        }
    }
}
