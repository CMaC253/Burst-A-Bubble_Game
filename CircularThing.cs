using System;
using System.Text;

// If you haven't done so already, you should read the README.txt
// file, first

namespace StarterProject
{
	public class CircularThing
	{
		public CircularThing() : this(0, 0, 1.0)
		{
		}

		public CircularThing(int xPos, int yPos, double r)
		{
			m_xPosition = xPos;
			m_yPosition = yPos;
			m_radius = r;
			m_ID = m_nextID++;
		}

		private static int m_nextID;
		protected int m_xPosition;
		protected int m_yPosition;
		protected double m_radius;
		protected int m_ID;

		public int ID
		{
			get { return m_ID; }
		}

		public int X
		{
			get { return m_xPosition; }
		}
		public int Y
		{
			get { return m_yPosition; }
		}

		public bool OverLapsOtherThing(CircularThing other)
		{
			double distance = Math.Sqrt( Math.Pow(m_xPosition - other.m_xPosition, 2) + Math.Pow(m_yPosition - other.m_yPosition, 2));
			if( distance < m_radius + other.m_radius)
				return true;
			else
				return false;
		}

		virtual public void Print()
		{
			Console.WriteLine("\tID #: {0}", m_ID);
			Console.WriteLine("\tX Position: {0}", m_xPosition);
			Console.WriteLine("\tY Position: {0}", m_yPosition);
			Console.WriteLine("\tRadius: {0}", m_radius);
		}
	}
}
