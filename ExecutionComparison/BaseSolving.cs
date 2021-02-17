using System;
using System.Diagnostics;

namespace ExecutionComparison
{
    public abstract class BaseSolving
    {
        protected int Bound { get; }
        protected Random Rand { get; }
        protected int Iterations { get; }
        protected Stopwatch Stopwatch { get; }

        protected BaseSolving(int bound = 100, int iterations = 10_000)
        {
            Bound = bound;
            Rand = new Random();
            Iterations = iterations;
            Stopwatch = new Stopwatch();
        }

        public abstract void Start();

        protected void FindRoots(int a, int b, int c)
        {
            if (a == 0)
            {
                Console.WriteLine("Equation is not quadratic");
                return;
            }

            float determinant = b * b - 4 * a * c;
            double sqrtDet = Math.Sqrt(Math.Abs(determinant));

            Console.WriteLine(determinant >= 0
                ? $"{0.5 * (-b + sqrtDet) / a}; {0.5 * (-b - sqrtDet) / a}"
                : $"{-b / (2 * a)} +- {sqrtDet / (2 * a)} * i");
        }

        protected int GetRandom() => Rand.Next(-Bound, Bound);
    }
}
