/*  Zachary Vasey
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeCutOptimization
{
    class Optimizer
    {
        public Optimizer()
        {

        }

        public static List<Pipe> GetOptimalCombination(SourcePipe sourcePipe, List<Pipe> pipeList)
        {
            //Algorithm based on the Bin Packing Algorithm
            //https://www.youtube.com/watch?v=kiMFyTWqLhc

            // Sorts Input List
            pipeList.Sort();

            //return value
            List<Pipe> optimalCombination = new List<Pipe>();

            // Used to get the total length remaining from the source pipe
            double tempRemaining = sourcePipe.Length;

            for (int i = pipeList.Count - 1; i >= 0; i--)
            {
                if (pipeList.ElementAt(i).Length < tempRemaining)
                {
                    tempRemaining -= pipeList.ElementAt(i).Length;
                    sourcePipe.AddSubPipe(pipeList.ElementAt(i));
                    pipeList.RemoveAt(i);
                }
            }
            return optimalCombination;
        }

        public static List<Pipe> GetOptimalCombination(SourcePipe sourcePipe, List<Pipe> pipeList, double bladeThickness)
        {
            //Algorithm based on the Bin Packing Algorithm
            //https://www.youtube.com/watch?v=kiMFyTWqLhc

            // Sorts Input List
            pipeList.Sort();

            //return value
            List<Pipe> optimalCombination = new List<Pipe>();

            // Used to get the total length remaining from the source pipe
            double tempRemaining = sourcePipe.Length;

            for (int i = pipeList.Count - 1; i >= 0; i--)
            {
                if (pipeList.ElementAt(i).Length + bladeThickness < tempRemaining)
                {
                    tempRemaining -= pipeList.ElementAt(i).Length + bladeThickness;
                    sourcePipe.AddSubPipe(pipeList.ElementAt(i));
                    pipeList.RemoveAt(i);
                }
            }
            return optimalCombination;
        }

        public static void GetOptimalCombination(List<SourcePipe> sourcePipes, List<Pipe> pipeList)
        {
            sourcePipes.Sort();
            foreach (SourcePipe sp in sourcePipes)
            {
                sp.AddSubPipe(Optimizer.GetOptimalCombination(sp, pipeList));
                pipeList.Sort();
            }
        }

        public static string DisplayMatrix(List<List<Pipe>> l)
        {
            //TEST
            //Console.WriteLine("In DisplayMatrix");
            //END TEST

            List<string> temp = new List<string>();

            //TEST
            //Console.WriteLine(l.ToArray().Length);
            //END TEST

            foreach (var arr in l)
            {
                //Console.WriteLine("Hello");
                //For formatting
                temp.Add("{");
                foreach(var elm in arr)
                {
                    temp.Add(elm.ToString());

                    //TEST
                    //Console.WriteLine(elm.ToString());
                    //END TEST
                    
                    //For formatting
                    temp.Add(", ");
                }
                //For formatting
                temp.Add("}\n");
            }
            //Convert the whole list into a string
            string returnVal = String.Join("", temp.ToArray());

            //TEST
            //Console.WriteLine("Out DisplayMatrix");
            //END TEST

            return returnVal;
        }

        private static double[] GetTotalValueArray(List<List<Pipe>> valueMatrix)
        {
            List<double> temp = new List<double>();
            double totalTemp = 0;

            foreach (var arr in valueMatrix)
            {
                foreach (var elm in arr)
                {
                    totalTemp = totalTemp + elm.Length;
                }
                temp.Add(totalTemp);
                totalTemp = 0;
            }
            return temp.ToArray();
        }
    }
}
