using System;
using System.Text;

namespace StarterProject
{

    // STUDENTS: You will need to implement this.
	public class Fireworks : Projectile
	{
        private int m_Detonate;
        override public bool DoUpdate()
        {
            if (m_Detonate == 0)
                return true;
            if (m_Detonate == 1)
            {
                m_radius = m_radius + 20;
                m_Detonate--;
                return false;
            }
            m_Detonate--;
            return base.DoUpdate();

        }

        override public void GetUserInitInfo()
        {
            m_xSpeed = MenuCUI.GetValidUserInput("Initial X speed? (1-10)", 1, 11);
            m_ySpeed = MenuCUI.GetValidUserInput("Initial Y speed? (1-10)", 1, 11);
            m_Detonate = MenuCUI.GetValidUserInput("Rounds Until Detonation (1-6)", 1, 7)+1;
        }


        override public void Print()
        {
            Console.WriteLine("Firework");
            base.Print();

            if(m_Detonate==0)
                Console.WriteLine("Fireworks has detonated (Note the increased radius!)");
            else
                Console.WriteLine("There are {0} rounds until detonation",m_Detonate);
        }


    }
}
