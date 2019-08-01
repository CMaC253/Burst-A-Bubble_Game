using System;
using System.Text;

// If you haven't done so already, you should read the README.txt
// file, first


namespace StarterProject
{
    public class Player
    {
        private int numRocks;
        private int numFireworks;

		public Player(int nRock, int nFire)
		{
			numRocks = nRock;
			numFireworks = nFire;
		}

		public void Print()
		{
			Console.WriteLine("Player has the following items left:");
			Console.WriteLine("\tRocks:\t\t{0}", numRocks);
			Console.WriteLine("\tFireworks:\t{0}", numFireworks);
		}

		public bool HasProjectilesLeft()
		{
			// return true if the player has at least
			// one thing left to throw...
			return  numRocks > 0 ||
					numFireworks > 0;
		}

        public int NumRocks
        {
            get
            {
                return numRocks;
            }
            set
            {
                numRocks = value;
            }
        }

        public int NumFireworks
        {
            get
            {
                return numFireworks;
            }
            set
            {
                numFireworks = value;
            }
        }

    }
}
