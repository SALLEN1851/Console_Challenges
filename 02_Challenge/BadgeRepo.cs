namespace ChallengeTwo
{
    public class BadgeRepo
    {
        private readonly Dictionary<int, Badge> _badgeDictionary = new Dictionary<int, Badge>();

        // Create
        public void CreateNewBadge(int badgeID, List<string> doors, string name)
        {
            Badge newBadge = new Badge(badgeID, doors, name);
            _badgeDictionary.Add(badgeID, newBadge);
        }

        // Read
        public Badge GetBadge(int badgeID)
        {
            if (_badgeDictionary.ContainsKey(badgeID))
            {
                return _badgeDictionary[badgeID];
            }
            return null;
        }
        public string GetDoorsByID(int badgeID)
        {
            Badge targetBadge = GetBadge(badgeID);
            if (targetBadge != null)
            {
                return string.Join(", ", targetBadge.Doors);
            }
            return "not found";
        }

        // Update
        public void AddDoor(int badgeID, List<string> newRange)
        {
            Badge targetBadge = GetBadge(badgeID);
            targetBadge.Doors.AddRange(newRange);
            if (targetBadge.Doors.Contains("removed"))
            {
                RemoveDoor(targetBadge, "removed");
            }
            Console.WriteLine("doors added");
            Console.WriteLine($"Badge {badgeID} has access to: " + GetDoorsByID(badgeID) +"\n");
        }
        public void RemoveDoor(Badge targetBadge, string door)
        {

            if (targetBadge.Doors.Contains(door))
            {
                targetBadge.Doors.Remove(door);
                Console.WriteLine("removed");
            }
            else Console.WriteLine("failed");

        }

        public void PrintAllBadges()
        {
            Console.WriteLine();
            Console.WriteLine($"{"Badge#"}{"Door Access"}");
            Console.WriteLine("------------------------------");
            foreach(KeyValuePair<int, Badge> badge in _badgeDictionary)
            {
                Console.WriteLine($"{badge.Key}{GetDoorsByID(badge.Key)}");
            }
        }
    }
}