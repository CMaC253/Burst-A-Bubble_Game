using System;
using System.Text;

// If you haven't done so already, you should read the README.txt
// file, first


namespace StarterProject
{
    abstract public class Projectile : CircularThing
    {
		protected int m_xSpeed;
		protected int m_ySpeed;

		// Default ctor does default initialization
		public Projectile()
		{
		}

		// Provided in case you want to put the projectile
		// somewhere, with a different radius
		public Projectile(int xPos, int yPos, double r) 
			: base(xPos, yPos, r)
		{
		}

		abstract public void GetUserInitInfo();

		virtual public bool DoUpdate()
		{
			m_xPosition += m_xSpeed;
			m_yPosition += m_ySpeed;

			return false;
		}

		override public void Print()
		{
			base.Print();
			Console.WriteLine("\tX Speed:{0}", m_xSpeed);
			Console.WriteLine("\tY Speed:{0}", m_ySpeed);
		}

		virtual public int ApplyGravity(int ySpeed)
		{
			return ySpeed - 2;
		}
    }
}
