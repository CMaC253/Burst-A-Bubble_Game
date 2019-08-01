using System;
using System.Text;

// If you haven't done so already, you should read the README.txt
// file, first


namespace StarterProject
{
	public struct PlayingSpace
	{
		private int m_maxX;
		private int m_minX;
		private int m_maxY;
		private int m_minY;

		public PlayingSpace(int minX, int maxX, int minY, int maxY)
		{
			m_minX = minX;
			m_maxX = maxX;
			m_minY = minY;
			m_maxY = maxY;
		}
		/// <summary>
		/// Is the projectile outside of the bounds of the game?
		/// </summary>
		/// <returns>true if the projectile has either hit the ground,
		/// or outside the bounds set by the PlayingSpace object
		/// Returns false otherwise.</returns>
		public bool IsOutOfBounds( Projectile p)
		{
			if (p.X < m_minX || p.X > m_maxX ||
				p.Y < m_minY || p.Y > m_maxY)
				return true;
			else 
				return false;
		}
	}
}
