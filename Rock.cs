using System;
using System.Text;

// If you haven't done so already, you should read the README.txt
// file, first


namespace StarterProject
{
	class Rock : Projectile
	{
		override public bool DoUpdate()
		{
			m_ySpeed = ApplyGravity(m_ySpeed);

			return base.DoUpdate();
		}

		override public void GetUserInitInfo()
		{
			m_xSpeed = MenuCUI.GetValidUserInput("Initial X speed? (1-10)", 1, 11);
			m_ySpeed = MenuCUI.GetValidUserInput("Initial Y speed? (1-10)", 1, 11);
		}


		override public void Print()
		{
			Console.WriteLine("Rock");
			base.Print();
		}
	}
}
