namespace Challenge
{
    public class MenuItem
    {
        public MenuItem() { }
        public MenuItem(int number, string name, string description, List<string> ingredientList, decimal price)
        {
            Number = number;
            Name = name;
            Description = description;
            IngredientList = ingredientList;
            Price = price;
        }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients 
            {
            get { return ListIngredients(IngredientList); }
            }
        private List<string> IngredientList { get; set; }
        public decimal Price { get; set; }

        public void AddIngredient(string ingredient)
        {
            IngredientList.Add(ingredient);
        }
        private string ListIngredients(List<string> ingredientList)
        {
            string concatList = "Ingredients: " + String.Join(", ", ingredientList);
            return concatList;
        }
    }
}
