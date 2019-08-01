using System;
using System.Text;

// If you haven't done so already, you should read the README.txt
// file, first


namespace StarterProject
{
    public class BurstABubble
    {
        private const int MAX_THINGS_IN_FLIGHT = 3;
        private const int MAX_ROCKS = 3;
        private const int MAX_FIREWORKS = 3;

        private Player p;
        private Projectile[] m_thingsInFlight;
        private int m_thingsInFlight_NextUnoccupiedIndex = 0;
        private PlayingSpace boundaries;
        private Bubble[] m_bubbles;
        private int m_bubbles_LastOccupiedIndex;

        private MenuCUI m_mnuMain;

        public BurstABubble()
        {
            m_thingsInFlight = new Projectile[MAX_THINGS_IN_FLIGHT];

            m_mnuMain = new MenuCUI(this); // Create a MenuCUI object and pass in a reference to this BurstABubble object
            // This will allow the MenuCUI to call methods here in order to create rocks, fireworks, etc, etc.
            // CUI == Console User Interface.  It means the "console", where we use Console.WriteLine to print messages 
            // to the user and get input when the user types
            // The alternative is a "GUI", or "Graphical User Interface", which has windows, menus, and lets the user
            // use a mouse.  GUIs are nice, but much, much more complicated to program so we're skipping that :)
            boundaries = new PlayingSpace(0, 100, 0, 100);

            m_bubbles = new Bubble[4];
        }

        public void PlayGame()
        {
            Reset();

            while (PlayRound())
                ; // loop until the game is over
        }

        public void Reset()
        {
            p = new Player(MAX_ROCKS, MAX_FIREWORKS);

            for (int i = 0; i < m_bubbles.Length; i++)
            {
                m_bubbles[i] = new Bubble(50 - (i * 10), 50 - (i * 10), (5 - i + 1) * 3);
            }

            m_bubbles_LastOccupiedIndex = m_bubbles.Length - 1;
        }

        public bool PlayRound()
        {
            ClearConsole();

            // Tell the player how many projectiles they have left...
            p.Print();
            Console.WriteLine();

            // Update anything in play from a previous round:
            // Could probably merge these two loops into one, single loop
            for (int i = 0; i < m_thingsInFlight_NextUnoccupiedIndex; i++)
            {
                // Move the Thing - returns true if thing was destroyed
                if (m_thingsInFlight[i].DoUpdate() ||

                    // Has it left the playing field?
                    boundaries.IsOutOfBounds(m_thingsInFlight[i]))
                {
                    // tell the user it's gone:
                    Console.WriteLine("The following projectile has left the playing field:");
                    m_thingsInFlight[i].Print();

                    // remove it from play
                    for (int j = i; j < m_thingsInFlight_NextUnoccupiedIndex - 1; j++)
                    {
                        m_thingsInFlight[j] = m_thingsInFlight[j + 1];
                    }
                    // last slot now open
                    m_thingsInFlight_NextUnoccupiedIndex--;
                }
                else
                {
                    // only check for collisions if the projectile still exists
                    for (int iBubble = 0; iBubble <= m_bubbles_LastOccupiedIndex; iBubble++)
                    {
                        if (m_thingsInFlight[i].OverLapsOtherThing(m_bubbles[iBubble]))
                        {
                            Console.WriteLine("Projectile #{0} has burst bubble #{1}",
                                    m_thingsInFlight[i].ID,
                                    m_bubbles[iBubble].ID);

                            // remove it from play
                            for (int j = iBubble; j < m_bubbles_LastOccupiedIndex; j++)
                            {
                                m_bubbles[j] = m_bubbles[j + 1];
                            }
                            m_bubbles_LastOccupiedIndex--;

                            // This looks weird, but is correct
                            // we need to 'back up' a spot, since
                            // we just 'deleted' the current bubble out from underneath
                            // the iBubble index.
                            iBubble--;
                        }
                    }
                }
            }
            Console.WriteLine();

            // Print out the bubbles:
            if (m_bubbles_LastOccupiedIndex >= 0)
                Console.WriteLine("The following bubbles are still in-play:");
            else
                Console.WriteLine("There are no bubbles currently in-play:");

            for (int i = 0; i <= m_bubbles_LastOccupiedIndex; i++)
            {
                m_bubbles[i].Print();
            }
            Console.WriteLine();

            // Print out any user-thrown things:
            if (m_thingsInFlight_NextUnoccupiedIndex > 0)
                Console.WriteLine("The following projectiles are still in-play:");
            else
                Console.WriteLine("There are no projectiles currently in-flight");

            for (int i = 0; i < m_thingsInFlight_NextUnoccupiedIndex; i++)
            {
                m_thingsInFlight[i].Print();
            }
            Console.WriteLine();

            // Before allowing the player to choose from the menu, 
            // check to see if they've won or lost already.

            // Return false (game over)
            // if one of two conditions are met:
            // 1) All bubbles have been burst
            if (m_bubbles_LastOccupiedIndex == -1)
            {
                Console.WriteLine("Congrats - you burst all the bubbles!");
                Console.WriteLine("You win!!");
                return false;
            }
            if (m_thingsInFlight_NextUnoccupiedIndex == 0 &&
                    !p.HasProjectilesLeft())
            {
                Console.WriteLine("Too bad - you're out of ammo, and haven't burst all the bubbles!");
                Console.WriteLine("You lose!");
                return false;
            }

            // if the user elects to quit, do so.  Otherwise
            // keep going
            if (false == m_mnuMain.GetAndExecuteMenuChoice(p, this))
                return false;

            // else we're going to play another round
            return true;
        }



        public bool CreateRock(Player p)
        {
            if (p.NumRocks > 0)
            {
                Console.WriteLine("You wish to create a rock:");
                AddThingToFlightList(new Rock());
                p.NumRocks--;
            }
            else
            {
                Console.WriteLine("You don't have any rocks left to throw!\n\t <Press the Enter key to continue>");
                Console.ReadLine();
            }
            return true; // game should continue
        }

        public bool CreateFireWorks(Player p)
        {
            if (p.NumFireworks > 0)
            {
                Console.WriteLine("You wish to light a fireworks:");
                AddThingToFlightList(new Fireworks());
                p.NumFireworks--;
            }
            else
            {
                Console.WriteLine("You don't have any fireworks left!\n\t <Press the Enter key to continue>");
                Console.ReadLine();
            }

            return true; // game should continue
        }

        public bool Quit(Player p)
        {
            Console.WriteLine("Thank you for playing!");
            return false; // user chose to quit, so the game should stop at the end of this round
        }

        public bool Wait(Player p)
        {
            Console.WriteLine("Waiting this round");
            return true; // user chose to quit
        }

        public bool AddThingToFlightList(Projectile pr)
        {
            if (m_thingsInFlight_NextUnoccupiedIndex >= m_thingsInFlight.Length)
            {
                Console.WriteLine("Sorry, but there's no space left!");
                return false;
            }

            m_thingsInFlight[m_thingsInFlight_NextUnoccupiedIndex] = pr;
            m_thingsInFlight[m_thingsInFlight_NextUnoccupiedIndex].GetUserInitInfo();

            m_thingsInFlight_NextUnoccupiedIndex++;
            return true;
        }

        public void ClearConsole()
        {
            for (int i = 0; i < 80; i++)
                Console.WriteLine();
        }
    }
}
