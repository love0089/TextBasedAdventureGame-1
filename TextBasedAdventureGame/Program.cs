using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventory;
using System.IO;

namespace TextBasedAdventureGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game theGame = new Game();
            theGame.reset();

            //theGame.StartGame();

            while (theGame.getPlaying() == true)
            {
                theGame.checkIfValid(Console.ReadLine().ToUpper());
            }
        }
    }

    public class Game
    {
        bool m_Playing = true;
        string path = @"C:\C#Temp\";
        Location m_Location = new Location();
        List<LocationTesting> locList = new List<LocationTesting>();

        string currentLocation = string.Empty;

        public void reset()
        {
            m_Playing = true;
            m_Location.reset();

            foreach (string file in Directory.GetFiles(path, "*.txt"))
            {
                float x = 0;
                string readText = File.ReadAllText(file);
                StringBuilder stringBuilder = new StringBuilder();

                string description = string.Empty;
                List<string> locGoesTo = new List<string>();
                List<string> items = new List<string>();

                string locName = string.Empty;

                bool nameGet = false;
                bool descGet = false;
                bool goToGet = false;
                bool itemGet = false;

                #region "Building the location list"
                foreach (char c in readText)
                {
                    if (c == '|')
                    {
                        x += 1;

                        if (nameGet == false)
                        {
                            nameGet = true;
                            locName = stringBuilder.ToString();
                            stringBuilder.Clear();
                            continue;
                        }

                        else if (descGet == false)
                        {
                            descGet = true;
                            description = stringBuilder.ToString();
                            stringBuilder.Clear();
                            continue;
                        }

                        else if (goToGet == false)
                        {
                            goToGet = true;
                            locGoesTo.Add(stringBuilder.ToString());
                            stringBuilder.Clear();
                            continue;
                        }

                        else if (itemGet == false)
                        {
                            itemGet = true;
                            items.Add(stringBuilder.ToString());
                            stringBuilder.Clear();
                            continue;
                        }

                        else
                        {
                            continue;
                        }
                    }

                    else
                    {
                        stringBuilder.Append(c);
                    }
                }
                #endregion

                LocationTesting locFile = new LocationTesting(locName, locGoesTo, description, items);
                locList.Add(locFile);
            }
        }

        private void setPlaying(bool playing)
        {
            m_Playing = playing;
        }

        public bool getPlaying()
        {
            return m_Playing;
        }

        private void newLocation(string locName)
        {
            bool goToWriting = true;
            bool descWriting = true;
            bool itemWriting = true;
            string description = string.Empty;
            List<string> locGoesTo = new List<string>();
            List<string> items = new List<string>();

            while(goToWriting == true)
            {
                Console.WriteLine("Where does this location go?\n");
                locGoesTo.Add(Console.ReadLine());
                Console.WriteLine("Is that all?");
                if(Console.ReadLine().ToUpper() == "YES") goToWriting = false;
            }
                
            while(descWriting == true)
            {
                Console.WriteLine("Please describe the location:\n");
                description += Console.ReadLine();
                Console.WriteLine("Is that all?");
                if(Console.ReadLine().ToUpper() == "YES") descWriting = false;
            }

            while (itemWriting == true)
            {
                Console.WriteLine("Wat items are here?\n");
                items.Add(Console.ReadLine());
                Console.WriteLine("Is that all?");
                if (Console.ReadLine().ToUpper() == "YES") itemWriting = false;
            }

            LocationTesting locationClass = new LocationTesting(locName, locGoesTo, description, items);
        }

        public void checkIfValid(string toCheck)
        {
            //string gotIt = string.Emp
            //ty;

            if (toCheck == "EXIT")
            {
                setPlaying(false);
                return;
            }

            if (toCheck == "RESET")
            {
                reset();
                return;
            }

            if (toCheck == "QTE")
            {

            }

            if (toCheck == "LOCTEST")
            {
                List<string> test = new List<string>();
                List<string> itemTest = new List<string>();
                string temp = string.Empty;

                for (int i = 0; i < 5; i++)
                {
                    test.Add("Location" + i.ToString());
                }

                for (int i = 0; i < 8; i++)
                {
                    itemTest.Add("Item" + i.ToString());
                }

                for (int i = 0; i < 99; i++)
                {
                    temp += i.ToString();
                }

                LocationTesting locTest = new LocationTesting("FIELDTEST", test, temp, itemTest);
                locList.Add(locTest);
            }

            if (toCheck == "FIELDTEST")
            {
                foreach (LocationTesting location in locList)
                {
                    string name = location.getLocation();

                    if (name == toCheck)
                    {
                        location.LocationReading();
                        return;
                    }
                }
            }

            foreach (LocationTesting location in locList)
            {
                if (location.getLocation() == toCheck)
                {
                    location.LocationReading();
                }

                foreach (string potentialLocation in location.m_GoTo)
                {
                    if (potentialLocation == toCheck)
                    {
                        location.LocationReading();
                        return;
                    }
                }
            }
        }

        public void StartGame()
        {
            Console.WriteLine("Fail Corp Presents....");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();

            Console.Beep(200, 400);
            Console.Beep(100, 400);
            Console.Beep(400, 900);

            Console.WriteLine(" _______  _______ _________ _ ");
            Console.WriteLine("|  ____ \\(  ___  )\\__   __/( \\");
            Console.WriteLine("| (    \\/| (   ) |   ) (   | (");
            Console.WriteLine("| (__    | (___) |   | |   | | ");
            Console.WriteLine("|  __)   |  ___  |   | |   | |");
            Console.WriteLine("| (      | (   ) |   | |   | |  ");
            Console.WriteLine("| )      | )   ( |___) (___| (____/\\");
            Console.WriteLine("|/       |/     \\|\\_______/(_______/");

            System.Threading.Thread.Sleep(500);

            Console.WriteLine(" _______           _______  _______ _________");
            Console.WriteLine("(  ___  )|\\     /|(  ____ \\(  ____ \\__   __/");
            Console.WriteLine("| (   ) || )   ( || (    \\/| (    \\/   ) (   ");
            Console.WriteLine("| |   | || |   | || (__    | (_____    | |  ");
            Console.WriteLine("| |   | || |   | ||  __)   (_____  )   | |  ");
            Console.WriteLine("| | /\\| || |   | || (            ) |   | |  ");
            Console.WriteLine("| (_\\ \\ || (___) || (____/\\/\\____) |   | |  ");
            Console.WriteLine("(____\\/_)(_______)(_______/\\_______)   )_(  ");
            Console.Beep(200, 400);
            Console.Beep(100, 400);
            Console.Beep(400, 900);

            System.Threading.Thread.Sleep(3000);
            Console.Clear();
            Console.WriteLine("Made by Joel Cright, Nikolai Morin Cull, Andy Lovett, and Greg Coghill");
            System.Threading.Thread.Sleep(3000);
            Console.Clear();

            Console.Clear();
            Console.WriteLine("          Welcome to FAIL QUEST.\nThis text based adventure will test your failure awesomenessness.\n\nYou have just exited the TOWN of Shmagma, ");
            Console.WriteLine("it is a hot summer day with a nice cool wind blowing east and everything is ");
            Console.WriteLine("perfect. All of a sudden a shriek comes from the north east of your position.");
            Console.WriteLine("Quickly you spin around and witness a massive DEMON carring a princess to the ");
            Console.WriteLine("castle behind the TOWN you just left.");
            Console.WriteLine("You feel you must do what you can to save the princess from her fate,\nbut you must aquire itams to aid you on your quest.");
            Console.WriteLine("You are facing a path that leads to a beautiful FIELD of tall grass.");
            Console.WriteLine("Behind you is the TOWN of Shmagma from which you have just exited.\n");
            Console.WriteLine("What do you choose to do?");
            Console.WriteLine("Go to the TOWN.");
            Console.WriteLine("Go to the FIELD.");
            Console.WriteLine("Look around.");
            Console.WriteLine("Check Inventory.");
            Console.WriteLine("");
        }
    }

    public class Location
    {
        string m_Location = string.Empty;
        Inventory.Inventory inventory = new Inventory.Inventory();
        bool lookedAround = false;

        public void reset()
        {
            m_Location = string.Empty;
            inventory.reset();
        }

        public void setLocation(string location)
        {
            m_Location = location;
        }

        public string getLocation()
        {
            return m_Location;
        }

        public void whereTo(string toHere)
        {
            string action = toHere.ToUpper();
            switch (action)
            {
                case "LOOK AROUND":
                    LookAround();
                    break;

                case "TEST INV":
                    inventory.testInventory();
                    break;

                case "TEST INV R":
                    inventory.testInventoryRemoval();
                    break;

                case "INVENTORY":
                    inventory.printInventory();
                    break;

                case "INV R":
                    inventory.removeFromInventory(Console.ReadLine());
                    break;

                case "MILL":
                case "GO TO THE MILL":
                case "GO TO MILL":
                    Mill();
                    break;

                case "BASEMENT":
                case "GO TO BASEMENT":
                case "GO TO THE BASEMENT":
                    Basement();
                    break;

                case "BLACKSMITH":
                case "GO TO THE BLACKSMITH":
                case "GO TO BLACKSMITH":
                    Blacksmith();
                    break;

                case "FIELD":
                case "GO TO FIELD":
                case "GO TO THE FIELD":
                    FieldOfShit();
                    break;

                case "RIVER":
                case "GO TO THE RIVER":
                case "GO TO RIVER":
                    River();
                    break;

                case "FOREST":
                case "GO TO FOREST":
                case "GO TO THE FOREST":
                    Forest();
                    break;

                case "EXIT":
                    break;

            }
        }

        private void LookAround()
        {
            if (lookedAround == false)
            {
                Console.WriteLine("You look around, and find a rope on the ground nearby!");
                foreach (string inventoryItem in inventory.getInventory())
                {
                    if (inventoryItem == null)
                    {
                        //inventory.Add("Rope");
                        lookedAround = true;
                        return;
                    }

                    else if (inventoryItem == "EndOfInv")
                    {
                        inventory.inventoryFull("Rope");
                        lookedAround = true;
                        return;
                    }
                }
            }

            else
            {
                Console.WriteLine("You've already looked around. There is nothing interesting anymore");
            }
        }

        private void FieldOfShit()
        {
            Console.Clear();
            Console.WriteLine("As you approach the field you notice a fairly recognizable smell");
            Console.WriteLine("You realize it is the smell of feces, but you are unable to ");
            Console.WriteLine("Identify the origins of what it came from.");
            Console.WriteLine("To the east lies the RIVER,\nto the south, the MILL,\nand to the west is the FOREST.\n");
            Console.WriteLine("What do you choose to do?");
            Console.WriteLine("Go to the RIVER.");
            Console.WriteLine("Go to the MILL.");
            Console.WriteLine("Go to the FOREST.");
            Console.WriteLine("Look around.");
            Console.WriteLine("Check Inventory.");
        }

        private void River()
        {
            Console.Clear();

            Console.WriteLine("You approach the river with caution as the rapids could easily pull you");
            Console.WriteLine(" in and make short work of you. A ROPE could be quite handy right now...\n");
            Console.WriteLine("What do you choose to do?");
            Console.WriteLine("Go to the FIELD.");
            Console.WriteLine("Go to the QUARRY.");
            Console.WriteLine("Look around.");
            Console.WriteLine("Check Inventory.");

            //Add an if statement that checks for rope, if rope is in inventory you may cross the river
        }

        private void Quarry()
        {
            bool inQuarry = true;
            bool pickaxe = false;
            bool menDead = false;
            Console.Clear();
            Console.WriteLine("You arrive at the quarry. It is very rocky. You see to quarry workers playing with some rocks, and a man standing over a pulley.\n There is a pickaxe on the ground");

            while (inQuarry == true)
            {
                Console.WriteLine("\nWhat will you do?\n");

                Console.WriteLine("Talk to Workers");
                Console.WriteLine("Talk to Man");
                Console.WriteLine("Search Rocks");
                Console.WriteLine("Take PICKAXE");
                Console.WriteLine("Leave");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "Talk to Workers":
                    case "Talk to workers":
                    case "talk to workers":
                        {
                            if (menDead == true)
                            {
                                Console.WriteLine("The men are dead. This makes it difficult to talk to them.");
                            }
                            else
                            {
                                Console.WriteLine("You approach the working men.\nThey ignore you and maintain their focus on the rocks.");
                            }
                            break;
                        }

                    case "Talk to Man":
                    case "talk to man":
                    case "Talk to man":
                        {
                            if (pickaxe == true)
                            {
                                Console.WriteLine("You approach the strange man...");
                                Console.WriteLine("He sees the pickaxe you are carrying and panics.\n He cuts the pulley and runs,dropping a hammer as he flees.\nThe pulley drops a boudler on the working men, crushing them.\nYou take the hammer");
                                pickaxe = false;
                                menDead = true;
                            }
                            else
                            {
                                Console.WriteLine("You approach the strange man...");
                                Console.WriteLine("He looks over your puny form and chuckles to himself. \nYou are no threat to him. He ignores you.");
                            }
                            break;
                        }
                    case "Search Rocks":
                        {
                            Console.WriteLine("You search through a pile of rocks. \nFoolishly, you disrupt a much larger pile, which is actually a small part\n of an even bigger pile. They tumble onto you, crushing you.");
                            System.Threading.Thread.Sleep(3000);
                            inQuarry = false;
                            gameOver();
                            break;
                        }

                    case "Take pickaxe":
                    case "Take Pickaxe":
                    case "take pickaxe":
                    case "Take PICKAXE":
                    case "take PICKAXE":
                    case "PICKAXE":
                        {
                            Console.WriteLine("You take the pickaxe. It is rather hefty, considering you are so weak and puny.");
                            System.Threading.Thread.Sleep(4000);
                            pickaxe = true;
                            break;
                        }

                    case "Leave":
                    case "leave":
                        {
                            Console.WriteLine("You exit the quarry. On your way back across the river, you lose your rope.");
                            System.Threading.Thread.Sleep(4000);
                            inQuarry = false;
                            Console.Clear();
                            break;
                        }
                    default:
                        Console.WriteLine("Thy does not compute. Try again");
                        break;
                }
            }
        }

        private void Mill()
        {
            Console.Clear();
            Console.WriteLine("A path from the field leads into a thin lining of trees. Behind the trees ");
            Console.WriteLine("you see an old MILL factory, it is very frail from wheather and time. you ");
            Console.WriteLine("walk to the old door that is barely hanging on to it's hinges, and slowly ");
            Console.WriteLine("push it open. The door opens half way then falls flat on the ground making ");
            Console.WriteLine("a loud BANG! Dust fills the air, but settles quickly. There is some old ");
            Console.WriteLine("equipment scattered about, and some remains of animals. There is an intact ");
            Console.WriteLine("door that leads to the BASEMENT to your left and a CHEST to your right.\n");
            Console.WriteLine("What do you choose to do?");
            Console.WriteLine("Go to the FIELD.");
            Console.WriteLine("Go to the BASEMENT.");
            Console.WriteLine("Open the CHEST.");
            Console.WriteLine("Look around.");
            Console.WriteLine("Check Inventory.");
        }

        private void Forest()
        {
            Console.Clear();
            Console.WriteLine("The FOREST is dark and crawling with danger. There is a small narrow ");
            Console.WriteLine("path leading into the arbour of death. As you enter, you hear something ");
            Console.WriteLine("scamper across the root riden ground ten feet in front of you, but you ");
            Console.WriteLine("cannot see what is was as it is dark as shit. You continue your trek ");
            Console.WriteLine("into the woods when you come across an opening. A small field of short ");
            Console.WriteLine("grass and blue sky over head from the lack of trees.\n");
            Console.WriteLine("What do you choose to do?");
            Console.WriteLine("Go to the FIELD.");
            Console.WriteLine("Look around.");
            Console.WriteLine("Check Inventory.");
        }

        private void Basement()
        {
            Console.Clear();
            Console.WriteLine("You open the door and head down the old rickety stairs to the BASEMENT.");
            Console.WriteLine("The further down you go the less you can see until nothing is visible.");
            Console.WriteLine("When you reach the last step you hear loud laboured breathing. The ");
            Console.WriteLine("breathing becomes louder and louder until it sounds like it is directly ");
            Console.WriteLine("in front of you. All of a sudden you hear a deafening hiss, then big red ");
            Console.WriteLine("eyes appear in font of you. Out of complete shock you wet your pants, shriek ");
            Console.WriteLine("and run back up the stairs and slam the door behind you. You can hear the ");
            Console.WriteLine("stairs cumble and fall apart behind the door. This calms you a little, but now ");
            Console.WriteLine("you have wet pants and reek of urine.\n");
            Console.WriteLine("What do you choose to do?");
            Console.WriteLine("Go to the FIELD.");
            Console.WriteLine("Open the CHEST.");
            Console.WriteLine("Look around.");
            Console.WriteLine("Check Inventory.");
        }

        private void Town()
        {
            Console.Clear();
            Console.WriteLine("You enter the town and the smell of baked goods and leather assult ");
            Console.WriteLine("your nostrils. The pleasant and familiar smell relaxes you and calms ");
            Console.WriteLine("you. The market is packed with people at every shop and commotion fills ");
            Console.WriteLine("air. To your left is the BLACKSMITH's, to your right is the TAVERN and ");
            Console.WriteLine("in front of you lies the gates to the CASTLE.");
            Console.WriteLine("What do you choose to do?");
            Console.WriteLine("Go to the BLACKSMITH.");
            Console.WriteLine("Go to the TAVERN.");
            Console.WriteLine("Go to the CASTLE.");
            Console.WriteLine("Look around.");
            Console.WriteLine("Check Inventory.");
        }

        private void Blacksmith()
        {
            bool inBlacksmith = false;
            bool gotGem = false;
            bool angryBlacksmith = true;
            Console.Clear();
            Console.WriteLine("You walk through the town and approach the BLACKSMITH's corner, the smell ");
            Console.WriteLine("of hot metal and smoke destroy the previous smell of baked goods from your ");
            Console.WriteLine("nostrils. You walk up to the BLACKSMITH and he glares at you with his one ");
            Console.WriteLine("eye, the other eye is covered with an eye patch from a recent accident.");
            Console.WriteLine("The BLACKSMITH grumbly asks you what you want.\n");
            while (inBlacksmith == true)
            {
                Console.WriteLine("What will you do?\n");

                Console.WriteLine("Tell the BLACKSMITH you don't like his face");
                Console.WriteLine("Talk to Blacksmith");
                Console.WriteLine("Look around");
                Console.WriteLine("Check INVENTORY");
                Console.WriteLine("Go to the TOWN");
                string input = Console.ReadLine();

                switch (input)
                {
                        //You are an idiot. Have fun dying
                    case "Tell the BLACKSMITH you don't like his face":
                    case "Tell the Blacksmith you don't like his face":
                    case "tell the blacksmith you dont like his face":
                    case "tell the blacksmith you don't like his face":
                        {
                            Console.Clear();
                            Console.WriteLine("The already miserable blacksmith, insulted by your words, is clearly not happy.He hurls his white-hot hammer across the room,\n and the hammer-head collides with your forehead, cracking your head wide open. ");
                            System.Threading.Thread.Sleep(4000);
                            inBlacksmith = false;
                            gameOver();
                            break;
                        }

                        //Got the gem? He lets you keep it
                    case "Talk to Blacksmith":
                    case "Talk to blacksmith":
                    case "talk to Blacksmith":
                    case "talk to blacksmith":
                        {
                            Console.WriteLine("");
                            break;
                        }

                        //You find a gem. Its pretty
                    case "look around":
                    case "Look around":

                        {

                            Console.WriteLine("You take a look around the blacksmith's workplace.\n After rudely sifting through various piles of stuff, you find a large, shiny gemstone.\nYou take it.");
                            
                            break;
                        }

                        //You check your stuff
                    case "Check INVENTORY":
                    case "Check inventory":
                    case "Check Inventory":
                    case "check INVENTORY":
                        {

                            break;
                        }

                        //Got the gem? If you didnt talk to the blacksmith after finding it, he kills you. Otherwise you're fine
                    case "Go to the TOWN":
                    case "go to the town":
                    case "go to the Town":
                    case "go to the TOWN":
                    case "Go to the Town":
                    case "Go to the town":
                        {
                            if (angryBlacksmith == true)
                            {
                                Console.WriteLine("You attempt to leave with the gem in hand when the blacksmith accuses you of theivery, which you are guilty of.\n He hurls a spear across the room, impaling your weak body.");
                                System.Threading.Thread.Sleep(4000);
                                gameOver();
                                inBlacksmith = false;
                                break;
                            }
                            else
                            {
                                Town();
                                inBlacksmith = false;
                                break;
                            }
                        }


                }
            }
        }

        private void Tavern()
        {
            int broseph = 0;
            bool inTavern = true;
            //Required bools
            //TODO: Item from drunk man
            Console.WriteLine("You walk into a tavern. You see many patrons and wenches. You notice a large man at the bar\n");

            while (inTavern == true)
            {
                Console.WriteLine("What will you do?\n");
                Console.WriteLine("Fight Man");
                Console.WriteLine("Talk to Man");
                Console.WriteLine("Drink with Man");
                Console.WriteLine("Leave");
                Console.WriteLine(": ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "Fight Man":
                    case "Fight man":
                    case "fight man":
                        {
                            Console.WriteLine("You point to the man and challenge his manliness.He immediately gets up,\nbrandishes an large axe, and removes your head from your shoulders.");
                            System.Threading.Thread.Sleep(3000);
                            inTavern = false;
                            gameOver();
                            break;
                        }
                    case "Talk to Man":
                    case "talk to man":
                    case "Talk to man":
                        {
                            Console.WriteLine("You approach the man and attempt to initiate a meaningful conversation...\n");
                            if (broseph > 1)
                            {
                                Console.Beep(300, 300);
                                Console.Beep(200, 300);
                                Console.Beep(300, 800);
                                Console.WriteLine("After a few minutes of chatting about wenches and alcohol.");
                                Console.WriteLine("He then passes out and falls to the ground.\n A key falls from his pocket. You take it.");
                                //Shit happens. Aw yiss
                                break;
                            }
                            else
                            {
                                Console.WriteLine("He scoffs at your scrawny form and turns his attention to something in the room that isn't as puny as you.\n");
                                //Nothing happens. Must be more bro-like
                                break;
                            }
                        }
                    case "Drink with Man":
                    case "drink with man":
                    case "Drink with man":
                        {
                            Console.WriteLine("You walk up to the bar and order a drink for the man with the fine,\n sculpted muscles. He gives you a heavy pat on the back and thanks you.");
                            broseph += 1;
                            //You become bros
                            break;
                        }

                    case "Leave":
                    case "leave":
                        {
                            Console.Clear();
                            inTavern = false;
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Thy does not compute. Try again, fool!");
                            break;
                        }
                }
            }
        }

        private void Castle()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        private void gameOver()
        {
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine("======= You have died =======");
            Console.WriteLine("         _,.-------.,_");
            Console.WriteLine("     ,;~'             '~;,");
            Console.WriteLine("   ,;                     ;,");
            Console.WriteLine("  ;                         ;");
            Console.WriteLine(" ,'                         ',");
            Console.WriteLine(",;                           ;,");
            Console.WriteLine("; ;      .           .      ; ;");
            Console.WriteLine("| ;   ______       ______   ; |");
            Console.WriteLine("|  `/~*     ~* . *~     *~\\'  |");
            Console.WriteLine("|  ~  ,-~~~^~, | ,~^~~~-,  ~  |");
            Console.WriteLine(" |   |        }:{        |   |");
            Console.WriteLine(" |   l       / | \\       !   |");
            Console.WriteLine(" .~  (__,.--* .^. *--.,__)  ~.");
            Console.WriteLine(" |     ---;' / | \\ `;---     |");
            Console.WriteLine("  \\__.       \\/^\\/       .__/");
            Console.WriteLine("   V| \\                 / |V");
            Console.WriteLine("    | |T~\\___!___!___/~T| |");
            Console.WriteLine("    | |`IIII_I_I_I_IIII'| |");
            Console.WriteLine("    |  \\,III I I I III,/  |");
            Console.WriteLine("     \\   `~~~~~~~~~~'    /");
            Console.WriteLine("       \\   .       .   /   ");
            Console.WriteLine("         \\.    ^    ./");
            Console.WriteLine("           ^~~~^~~~^");
            Console.Beep(300, 300);
            Console.Beep(200, 300);
            Console.Beep(100, 800);
            System.Threading.Thread.Sleep(3000);
            Console.Clear();
            reset();
        }
    }

    public class LocationTesting
    {
        string path = @"C:\C#Temp\";
        public List<string> m_GoTo = new List<string>();
        List<string> m_Items = new List<string>();
        string m_Location = string.Empty;
        string m_Description = string.Empty;

        public string getLocation()
        {
            return m_Location;
        }

        public LocationTesting(string location, List<string> canGoTo, string description, List<string> items)
        {
            m_Location = location;
            m_GoTo = canGoTo;
            m_Description = description;
            m_Items = items;
            Save();
        }

        private void Save()
        {
            string filePath = path + m_Location + ".txt";

            if (!File.Exists(filePath))
            {
                // Create a file to write to. 
                string toWrite = string.Join(", ", m_GoTo);
                string itemWrite = string.Join(", ", m_Items);
                string createText = m_Location + "|" + m_Description + "|" + toWrite + "|" + itemWrite + "|";
                File.WriteAllText(filePath, createText);
            }

            else
            {
                Console.WriteLine("This location already exists");
            }
        }

        public void LocationReading()
        {
            string goTo = string.Join("\n\t", m_GoTo);
            string items = string.Join("\n\t", m_Items);

            Console.WriteLine("The location is: " + m_Location);
            Console.WriteLine("\nYou can go to: " + goTo);
            Console.WriteLine("\nThe location looks: " + m_Description);
            Console.WriteLine("\nThe items here are: " + items);
        }
    }
}

