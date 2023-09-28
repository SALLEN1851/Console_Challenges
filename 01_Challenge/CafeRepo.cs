namespace Challenge
{
    public class CafeRepo
    {
        private readonly List<MenuItem> _menu = new List<MenuItem>();


        // Create
        public void AddToMenu(MenuItem item)
        {
            _menu.Add(item);
        }

        // Read
        public List<MenuItem> GetMenu()
        {
            return _menu;
        }
        public MenuItem GetByNumber(int number)
        {
            foreach (MenuItem item in _menu)
            {
                if (item.Number == number)
                {
                    return item;
                }
            }
            return null;
        }
        public void PrintMenu()
        {
            foreach(MenuItem item in _menu)
            {
                Console.WriteLine(
                    $"#{item.Number}\n" +
                    $"Meal: {item.Name}\n" +
                    $"Description: {item.Description}\n" +
                    $"{item.Ingredients}\n" +
                    $"Price: ${item.Price}\n" +
                    $"\n");
            }
        }

        // Delete
        public bool RemoveFromMenu(int number)
        {
            MenuItem item = GetByNumber(number);
            if (item != null)
            {
                _menu.Remove(item);
                return true;
            }
            else return false;
        }

    }
}