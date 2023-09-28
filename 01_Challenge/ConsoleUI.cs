namespace Challenge
{
    public enum MenuOption
    {
        Show = 1,
        Create,
        Remove,
        Exit
    }

    public class ConsoleUI
    {
        private readonly CafeRepo _repo = new CafeRepo();
        private bool _isRunning = true;

        private const string IngredientsEndMarker = "/";
        private const string PricePrompt = "Price: ";
        private const string InvalidPriceMessage = "That is not a valid price.";

        public void Start()
        {
            SeedMenuItems();
            RunConsoleUI();
        }

        private void RunConsoleUI()
        {
            while (_isRunning)
            {
                DisplayMenu();
                SelectOption();
            }
        }

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Select an Option:\n" +
                "1. Show List of Items\n" +
                "2. Create a New Item\n" +
                "3. Remove an Item\n" +
                "4. Exit");
        }

        private void SelectOption()
        {
            string userInput = Console.ReadLine();
            bool isValidChoice = Enum.TryParse(typeof(MenuOption), userInput, out object chosenOption);

            if (!isValidChoice)
            {
                return; 
            }

         MenuOption chosenMenuOption = (MenuOption)chosenOption;

            if (chosenMenuOption == MenuOption.Show)
            {
                Console.Clear();
                _repo.PrintMenu();
            }
            else if (chosenMenuOption == MenuOption.Create)
            {
                CreateNewItem();
            }
            else if (chosenMenuOption == MenuOption.Remove)
            {
                RemoveItem();
            }
            else if (chosenMenuOption == MenuOption.Exit)
            {
                _isRunning = false;
                return;
            }

            Console.WriteLine("Press a key to return");
            Console.ReadKey();

            }
        

        public void CreateNewItem()
        {
            Console.Clear();
            int itemNumber = _repo.GetMenu().Last().Number + 1;
            Console.Write("Enter the name of item: ");
            string name = Console.ReadLine();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            List<string> ingredients = GetIngredients();
            Console.Write("Price: ");
            decimal price;
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal parsedNum))
                {
                    price = parsedNum;
                    break;
                }
                else Console.WriteLine("That is not a valid price.");
            }

            MenuItem newItem = new MenuItem(itemNumber, name, description, ingredients, price);
            _repo.AddToMenu(newItem);
        }

        public List<string> GetIngredients()
        {
            List<string> ingredients = new List<string>();
            Console.WriteLine("Ingredients list: Type '/' when complete.");
        
            while(true)
            {
                Console.Write("Add an ingredient: ");
                string input = Console.ReadLine();
                if (input != "/")
                {
                    ingredients.Add(input);
                }
                else break;
            }
            return ingredients;
        }
        public void RemoveItem()
        {
            Console.Clear();
            Console.Write("Please type the item number: ");
            while (true)
            {
                string input = Console.ReadLine();
                bool wasParsed = int.TryParse(input, out int parsedNum);
                if (wasParsed)
                {
                    bool isRemoved = _repo.RemoveFromMenu(parsedNum);
                    if (isRemoved)
                    {
                        Console.WriteLine("Item removed.");
                        return;
                    }
                    Console.Write("Item not found. "); 
                    TryAgain();
                    RemoveItem();
                    return;
                }
                else Console.Write("Invalid. ");
            }
        }
        public void TryAgain()
        {
            Console.WriteLine("Try again? Y/N");
        string userInput = Console.ReadLine().ToLower();

            if (userInput == "yes" || userInput == "y")
            {
                return;
            }
            else if (userInput == "no" || userInput == "n")
            {
                RunConsoleUI();
                return;
            }
            else
            {
                TryAgain();
            }
        }

        public void SeedMenuItems()
        {
            MenuItem latte = new MenuItem(1, "Caramel Latte","very tasty", new List<string>(), 8);
            latte.AddIngredient("espresso");
            latte.AddIngredient("caramel");
            latte.AddIngredient("milk");

            MenuItem coffee = new MenuItem(2, "Coffee","excellent, bold coffee", new List<string>(), 2);
            coffee.AddIngredient("splenda");
            coffee.AddIngredient("sugar");
            coffee.AddIngredient("creamer");
           

            _repo.AddToMenu(latte);
            _repo.AddToMenu(coffee);
        }

        }
    }

