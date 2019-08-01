using System;
using System.Text;

// If you haven't done so already, you should read the README.txt
// file, first


// In retrospect, making a class that will compartmentalize
// the logic behind a Console User Interface (CUI) menu, when the
// program has only 1 menu, was kinda silly.  D'oh!
//
// The upside is that it does serve as a good example of how one 
// might encapsulate this sort of logic....
namespace StarterProject
{
	public delegate bool FnxToCall(Player p, BurstABubble game);

	public struct menuItem
    {
        public string m_text;
        public FnxToCall m_action;
        public bool m_active;

        public menuItem(string text, FnxToCall action)
        {
            m_text = text;
            m_action = action;
            m_active = true;
        }
    }

	public class MenuCUI
	{
        BurstABubble m_mainGameData;

		/// <summary>
		/// 
		/// </summary>
		public MenuCUI(BurstABubble b)
		{
            m_mainGameData = b;
		}

        /// <summary>
        /// This will return a string that can be used to print out the menu.
        /// We do this by concatenating each line onto the starting string,
        /// and then returning the whole thing.
        /// </summary>
        static public String getMenuAsString()
        {
            String sMenu = "Please select from the following:\n"; // start with this string
                // The \n at the end means "newline" (all one word - Google for it :) )
                // newline means end the line at the \n and go to the start of the next one
            // it's like pressing the 'Enter' key while 

            sMenu = sMenu + "0 - Throw a normal rock\n"; // take the existing string and glue/concatenate
            // "Throw a normal rock" onto it
            sMenu = sMenu + "1 - Light a firework at the bubbles\n";

            sMenu += "2 - Wait a round\n"; // remember that x +=1 is the same as x = x + 1.  Same thing with strings
            // (so this line does the same as the prior two)
            sMenu += "3 - Quit the game\n";
            sMenu += "Your choice?";

            return sMenu;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool GetAndExecuteMenuChoice(Player p, BurstABubble game)
		{
            int userChoice = GetValidUserInput(getMenuAsString(), 0, 4);
            switch( userChoice)
            {
                case 0: // "0 - Throw a normal rock"
                    return game.CreateRock(p);
                    // break isn't needed because the return on the previous line will end this case
                case 1: // "1 - Light a firework at the bubbles"
                    return game.CreateFireWorks(p);
                    break; // We can leave the break in, but we'll get a compiler warning :)
                case 2: // "2 - Wait a round"
                    return game.Wait(p);
                case 3: // "3 - Quit the game"
                    return game.Quit(p);
                default:
                    // We should never get here
                    // but just in case we'll return true
                    // (which means that we want the game to continue running - false would exit the game)
                    return true;
            }
		}

		static public int GetValidUserInput(string s, int min, int max)
		{
			if (min == Int32.MinValue)
				throw new Exception("min needs to be " + (Int32.MinValue + 1) + " or higher!");

			int iChoice = min - 1;
			while (iChoice < min || iChoice >= max)
			{
				try
				{
					Console.WriteLine(s);

                    iChoice = Int32.Parse(Console.ReadLine());
					if (iChoice < min)
						Console.WriteLine("You have to choose a number higher than {0}", min-1);
					else if (iChoice >= max)
						Console.WriteLine("You have to choose a number lower than {0}", max);
				}
				catch (Exception) // variable name omitted to avoid compiler warning
				{
					Console.WriteLine("You need to type in a number!");
					iChoice = min - 1;
				}
			}
			return iChoice;
		}

	}
}
