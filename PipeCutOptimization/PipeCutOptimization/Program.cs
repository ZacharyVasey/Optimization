using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeCutOptimization
{
    class Program
    {
        static void Main(string[] args)
        {
            //get random values to cut and sort
            Pipe[] testPipeValues = Pipe.GetRandomValues(12, 50.0).ToArray();
            Array.Sort(testPipeValues);

            //Console.WriteLine(ListToString(testPipeValues.ToList<Pipe>()));
            
            //get source pipe values and store in Source pipe array
            SourcePipe[] sourcePipes = { new SourcePipe(250), new SourcePipe(150), new SourcePipe(100)};
            List<List<Pipe>> matrix;
            List<Pipe> bestCombonation;
            List<Pipe> temp;
            foreach (SourcePipe sp in sourcePipes)
            {
                matrix = Optimizer.GetAvailableTotalPipes(sp, testPipeValues.ToList<Pipe>());
                bestCombonation = Optimizer.GetOptimalCombonation(sp, matrix);
                sp.AddSubPipe(bestCombonation);
                temp = testPipeValues.ToList<Pipe>();
                foreach (Pipe p in bestCombonation)
                {
                    temp.Remove(p);
                }
                testPipeValues = temp.ToArray();
            }

            foreach (SourcePipe sp in sourcePipes)
            {
                Console.Write("Pipe Length: " + sp.Length + ", ");
                Console.Write("Waste: " + sp.GetWaste() + ", \n");
                
            }
            
            /*
            //Console.WriteLine(Convert.ToString((int) Math.Pow(2, testPipeValues.Length) - 1, 2));
            SourcePipe sourcePipe = new SourcePipe(90.0);
            List<List<Pipe>> matrix = Optimizer.GetAvailableTotalPipes(sourcePipe, testPipeValues.ToList<Pipe>());
            /*foreach(List<Pipe> l in matrix)
            {
                foreach(Pipe i in l.ToArray())
                {
                    Console.Write(i);
                    Console.WriteLine();
                }
            }*/
            /*
            //Console.WriteLine(Optimizer.DisplayMatrix(matrix));
            Console.WriteLine("\n\nValue of Best Combonation");
            List<Pipe> bestCombonation = Optimizer.GetOptimalCombonation(sourcePipe, matrix);
            Console.WriteLine(ListToString(bestCombonation));
            Console.WriteLine(Pipe.GetTotalLengths(bestCombonation) + " out of " + sourcePipe.Length + ". Waste = " + (sourcePipe.Length - Pipe.GetTotalLengths(bestCombonation)));
            */
            /*Pipe a = new Pipe(50.0);
            Pipe b = new Pipe(25.0);
            Console.WriteLine(a.CompareTo(b));*/
            Console.ReadKey(true);
        }

        public static string ListToString(List<Pipe> l)
        {
            string temp = "{";
            foreach (var pipe in l)
            {
                temp = temp + pipe.ToString() + ", ";
            }
            temp = temp + "}";
            return temp;
        }
    }
}
