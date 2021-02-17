using System;

namespace ExecutionComparison
{
    public class NonConcurrentSolving : BaseSolving
    {
        public event Action SolvingFinished;

        public NonConcurrentSolving(int bound = 100, int iterations = 10_000) : base(bound, iterations) { }

        public override void Start()
        {
            Stopwatch.Start();

            for (int i = 0; i < Iterations; i++)
            {
                FindRoots(GetRandom(), GetRandom(), GetRandom());
            }

            Stopwatch.Stop();
            Console.WriteLine($"[Non-concurrent] Elapsed: {Stopwatch.ElapsedMilliseconds}");

            SolvingFinished?.Invoke();
        }
    }
}
