using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    class Rnd
    {
        private static readonly Random rnd = new Random();
        private static readonly object syncLock = new object();
        private int min;
        private int max;
        public int RandomNumber()
        {
            lock (syncLock)
            {
                return rnd.Next(min, max);
            }
        }
        public Rnd(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
