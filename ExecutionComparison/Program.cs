using System;
using System.Threading;

namespace ExecutionComparison
{
    public class Program
    {
        private const int ThreadAmount = 5;
        private const int EndWait = 5000;

        public static void Main(string[] args)
        {
            var threadCollection = new ThreadCollectionConcurrentSolving(ThreadAmount);
            var threadPool = new ThreadPoolConcurrentSolving(ThreadAmount);
            var nonConcurrent = new NonConcurrentSolving();

            threadCollection.SolvingFinished += () =>
            {
                Thread.Sleep(EndWait);
                threadPool.Start();
            };
            threadPool.SolvingFinished += () =>
            {
                Thread.Sleep(EndWait);
                nonConcurrent.Start();
            };
            nonConcurrent.SolvingFinished += () => Console.ReadLine();

            threadCollection.Start();
        }
    }
}
