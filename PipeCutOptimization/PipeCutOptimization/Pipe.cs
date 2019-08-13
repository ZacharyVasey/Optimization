using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeCutOptimization
{
    class Pipe : IComparable
    {
        public double Length { get; protected set; }
        public int UniqueId { get; protected set; }
        public double Cost { get; protected set; }
        public int DbCode { get; protected set; }
        public string LongDescription { get; protected set; }
        public double AlphaSize { get; protected set; }
        public Pipe(int id, double length)
        {
            UniqueId = id;
            Length = length;
        }
        public Pipe(double length)
        {
            Length = length;
        }
        public Pipe()
        {
            Length = 0;
        }

        public int CompareTo(object obj)
        {
            if (null == this)
            {
                throw new NotImplementedException();
            }
            Pipe otherPipe = obj as Pipe;
            if (otherPipe != null)
                return this.Length.CompareTo(otherPipe.Length);
            else
                throw new ArgumentException("Object is not a Pipe");
        }
        public override string ToString()
        {
            string temp = "[";
            temp = temp + "Unique ID: " + this.UniqueId + ", ";
            temp = temp + "Length: " + this.Length;
            temp = temp + "]";
            return temp;
        }
        public static double GetTotalLengths(List<Pipe> l)
        {
            double temp = 0;
            foreach (Pipe pipe in l)
            {
                temp = temp + pipe.Length;
            }
            return temp;
        }

        /* 
         * Used Math.Truncate(myDoubleValue * 100) / 100; from https://stackoverflow.com/questions/2453951/c-sharp-double-tostring-formatting-with-two-decimal-places-but-no-rounding
         * to format the Length value to two decimal places
         */
        public static List<Pipe> GetRandomValues(int number, double maximumSize)
        {
            List<Pipe> temp = new List<Pipe>();
            double tempLength = 0;
            Random r = new Random();
            for (int i = 0; i < number; i++)
            {
                //get initial random length 0 - maximumSize
                tempLength = (double)(r.NextDouble() * maximumSize);
                
                //format to two decimal places
                tempLength = Math.Truncate(tempLength * 100) / 100;
                
                //add to random list
                temp.Add(new Pipe(tempLength));
            }
            return temp;
        }
    }
}
