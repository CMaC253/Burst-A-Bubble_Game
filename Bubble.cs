using System;
using System.Text;

// If you haven't done so already, you should read the README.txt
// file, first


namespace StarterProject
{
	public class Bubble : CircularThing
	{
		public Bubble(int xPos, int yPos, double r) : base(xPos, yPos, r)
		{
		}

		override public void Print()
		{
			Console.WriteLine("Bubble:");
			base.Print();
		}
	}
}
