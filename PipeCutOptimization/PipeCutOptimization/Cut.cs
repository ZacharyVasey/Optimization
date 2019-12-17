using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeCutOptimization
{
    class Cut
    {
        public int DB_CODE { get; set; }
        public string AlphaSize { get; set; }
        public List<Pipe> Pipes;
        public List<SourcePipe> SourcePipes;
        public List<SourcePipe> NeededSourcePipes;

    }
}
