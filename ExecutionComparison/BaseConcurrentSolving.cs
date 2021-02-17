using System;
using System.Collections.Concurrent;

namespace ExecutionComparison
{
    public abstract class BaseConcurrentSolving : BaseSolving
    {
        protected ConcurrentQueue<Action> TaskQueue { get; }
        protected int ThreadAmount { get; }

        protected BaseConcurrentSolving(int threadAmount, int bound = 100, int iterations = 10_000) : base(bound, iterations)
        {
            ThreadAmount = threadAmount;

            TaskQueue = new ConcurrentQueue<Action>();
            for (int i = 0; i < iterations; i++)
            {
                TaskQueue.Enqueue(() => FindRoots(GetRandom(), GetRandom(), GetRandom()));
            }
        }
    }
}
