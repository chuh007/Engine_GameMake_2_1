namespace _01Scripts.TurnSystem
{
    public interface ITurnActor
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        public int ActionValue { get; set; }
        public void TurnAction();
    }
}