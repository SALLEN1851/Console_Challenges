namespace ChallengeTwo
{
    class ConsoleUI
    {
        private BadgeRepo _repo = new BadgeRepo();
        private bool _isRunning = true;
        public void Start()
        {
            SeedBadges();
            RunMenu();
        }
        private void RunMenu()
        {
            while (_isRunning)
            {
                Console.Clear();
                Console.WriteLine(
                    "Hello Security Admin, What would you like to do?\n" +
                    "---------------------------------\n" +
                    "1. Add a badge\n" +
                    "2. Edit a badge\n" +
                    "3. List all badges\n" +
                    "4. Exit");

                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    AddBadge();
                }
                else if (userInput == "2")
                {
                    EditDoors();
                }
                else if (userInput == "3")
                {
                    Console.Clear();
                    _repo.PrintAllBadges();
                }
                else if (userInput == "4")
                {
                    _isRunning = false;
                    return;
                }
                else
                {
                    Console.WriteLine("invalid.");
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to return");
                Console.ReadKey();
            }
        }

            private void AddBadge()
            {
                Console.Clear();
                Console.Write("What is the badge number: ");
                int badgeID;
                if (!int.TryParse(Console.ReadLine(), out badgeID))
                {
                    Console.WriteLine("Invalid badge number.");
                    return;
                }

                List<string> doors = GetMoreDoors();

                _repo.CreateNewBadge(badgeID, doors, badgeID.ToString());
                Console.WriteLine("Badge created");
            }

        private List<string> GetMoreDoors()
        {
            List<string> doors = new List<string>();
            Console.WriteLine("more door access: enter '/' when complete.");

            while (true)
            {
                Console.Write("add a door: ");
                string input = Console.ReadLine().ToUpper();
                if (input != "/")
                {
                    doors.Add(input);
                }
                else break;
            }
            return doors;
        }

        private void EditDoors()
        {
            Console.Clear();
            Console.WriteLine("Which badge number would you like to edit?");
            int badgeID = int.Parse(Console.ReadLine());
            Console.WriteLine();
            string doorList = _repo.GetDoorsByID(badgeID);
            Console.WriteLine($"Badge {badgeID} has access to: " + doorList + "\n");
            if (doorList == "badge not found") { return; }
            Console.WriteLine(
                "Select an option:\n" +
                "   1. add doors\n" +
                "   2. remove a door\n" +
                "   3. cancel\n");
            string userInput = Console.ReadLine();
            SelectEdit(userInput, badgeID);
        }

            private void SelectEdit(string input, int badgeID)
        {
            bool notCancel = true;
            while (notCancel)
            {
                if (input == "1")
                {
                    List<string> newRange = GetMoreDoors();
                    _repo.AddDoor(badgeID, newRange);
                    return;
                }
                else if (input == "2")
                {
                    Remove(badgeID);
                    return;
                }
                else if (input == "3")
                 {
                    List<string> newRange = GetMoreDoors();
                    _repo.AddDoor(badgeID, newRange);
                    return;
                }
                else
                {
                    Console.WriteLine("invalid.");
                }
            }
            Console.WriteLine("edit canceled.");
        }

        private void Remove(int badgeID)
        {
            Badge targetBadge = _repo.GetBadge(badgeID);
            Console.WriteLine("Which door would you like to remove?");
            string door = Console.ReadLine().ToUpper();
            _repo.RemoveDoor(targetBadge, door);
            Console.WriteLine($"Badge {badgeID} has access to: " + _repo.GetDoorsByID(badgeID) + "\n");
        }

        private void SeedBadges()
        {
            List<string> doorListOne = new List<string> { "A7" };
            List<string> doorListTwo = new List<string> { "A1", "A4", "B1", "B2" };
            List<string> doorListThree = new List<string> { "A4", "A5" };
            _repo.CreateNewBadge(12345, doorListOne, "FIRST");
            _repo.CreateNewBadge(22345, doorListTwo, "SECOND");
            _repo.CreateNewBadge(32345, doorListThree, "THIRD");
        }
    }
}