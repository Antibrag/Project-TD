namespace Data
{
    public class Level
    {
        public string ScenePath { get; private set; }
        public bool IsComplete { get; set; }
        public float StartPlayerHealth { get; private set; }

        public Level(string scenePath, float startPlayerHealth, bool isCompleted = false)
        {
            ScenePath = scenePath;
            IsComplete = isCompleted;
            StartPlayerHealth = startPlayerHealth;
        }
    }
}