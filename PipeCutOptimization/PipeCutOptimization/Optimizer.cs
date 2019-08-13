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

        // Get all available combinations you can cut from a source pipe
        // 
        public static List<List<Pipe>> GetAvailableTotalPipes(Pipe sourcePipe, List<Pipe> pipeList)
        {
            //TEST
            //Console.WriteLine("In GetAvailibleTotalPipes");
            //END TEST

            //Convert List of Pipes to array
            Pipe[] pipes = pipeList.ToArray();

            //Sort array from low - high
            Array.Sort(pipes);

            //return value
            List<List<Pipe>> valueMatrix = new List<List<Pipe>>();

            // List to store binary total
            List<string> binaryPipeList = new List<string>();

            //used to store bin of current combonation
            string binValOfNum;
            
            // Used to get the total of the pipes selected
            double tempTotal = 0;

            for (int i = 1; i < (int)Math.Pow(2, (double)pipes.Length); i++)
            {
                //TEST
                //Console.WriteLine("i: " + i);
                //END TEST

                binValOfNum = Convert.ToString(i, 2);
                
                //TEST
                //Console.WriteLine("binValOfNum: " + binValOfNum);
                //END TEST

                for (int j = binValOfNum.Length - 1; j >= 0; j--)
                {
                    //TEST
                    /*Console.WriteLine("j: " + j);
                    double temp0 = binValOfNum.Length - 1 - j;
                    double temp1 = pipes[binValOfNum.Length - 1 - j];
                    double temp2 = Convert.ToInt32(binValOfNum[j]) - 48;    // covert to ASCII VALUE
                    */
                    //END TEST

                    tempTotal = tempTotal + pipes[binValOfNum.Length - 1 - j].Length * (Convert.ToInt32(binValOfNum[j]) - 48);
                    
                    //TEST
                    //Console.WriteLine("pipes[binValOfNum.Length - 1 - j]: " + pipes[binValOfNum.Length - 1 - j]);
                    //Console.WriteLine("binValOfNum[j]: " + (Convert.ToInt32(binValOfNum[j]) - 48));
                    //END TEST
                    
                    // see if the combonation is possible after all the values have been added
                    if (j - 1 < 0)
                    {
                        //TEST
                        //Console.WriteLine("tempTotal: " + tempTotal);
                        //TEST END
                        if (tempTotal <= sourcePipe.Length)
                        {
                            binaryPipeList.Add(binValOfNum);
                        }
                    }
                }
                tempTotal = 0;
            }

            // temp list to add each combonation to the valueMatrix
            List<Pipe> tempPipeValueList = new List<Pipe>();

            // foreach binary representation of the list, convert it into the actual values
            foreach (var element in binaryPipeList)
            {
                for (int i = element.Length - 1; i >= 0; i--)
                {
                    if (Convert.ToInt32(element[i]) - 48 != 0)
                    {
                        tempPipeValueList.Add(pipes[element.Length - 1 - i]);
                    }
                }

                // add the combonation to the valueMatrix
                valueMatrix.Add(new List<Pipe>(tempPipeValueList));

                //TEST
                /*foreach (var i in tempPipeValueList)
                {
                    Console.Write(i + ", ");
                }
                Console.WriteLine();*/
                //END TEST

                // clear the list for the next iteration
                tempPipeValueList.Clear();
            }
            //TEST
            /*Console.WriteLine("Begin valueMatrix");
            foreach (var i in valueMatrix)
            {
                foreach (var elm in i)
                {
                    Console.Write(elm + ", ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("End valueMatrix");*/
            //END TEST

            //TEST
            //Console.WriteLine("Out GetAvailibleTotalPipes");
            //END TEST

            return valueMatrix;
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

        public static List<Pipe> GetOptimalCombonation(Pipe sourcePipe, List<List<Pipe>> valueMatrix)
        {
            double[] totalValuesOfCombonations = GetTotalValueArray(valueMatrix);
            DisplayMatrix(valueMatrix);
            double min;
            try
            {
                min = sourcePipe.Length - totalValuesOfCombonations[0];
            }
            catch(IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex);
                return new List<Pipe>();
            }
            int indexOfBestCombonation = 0;

            for (int i = 1; i < totalValuesOfCombonations.Length; i++)
            {
                if(sourcePipe.Length - totalValuesOfCombonations[i] < min)
                {
                    min = sourcePipe.Length - totalValuesOfCombonations[i];
                    indexOfBestCombonation = i;
                }
            }

            return valueMatrix.ElementAt(indexOfBestCombonation);
        }

        public static void GetOptimalSourcePipeCombonaition()
        {

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
