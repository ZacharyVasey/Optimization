using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeCutOptimization
{
    class SourcePipe : Pipe
    {
        protected List<Pipe> SubPipes;
        public SourcePipe()
        {
            SubPipes = new List<Pipe>();
        }
        public SourcePipe(double length)
        {
            Length = length;
            SubPipes = new List<Pipe>();
        }
        public void AddSubPipe(List<Pipe> Pipes)
        {
            foreach (Pipe p in Pipes)
            {
                SubPipes.Add(p);
            }
        }
        public void AddSubPipe(Pipe p)
        {
            SubPipes.Add(p);
        }
        public double GetWaste()
        {
            double temp = 0;
            foreach (Pipe sp in SubPipes)
            {
                temp = temp + sp.Length;
            }
            return this.Length - temp;
        }
    }
}
