namespace _01Scripts.TurnSystem
{
    public interface ITurnActor
    {
        public int Speed { get; set; }
        public int ActionValue { get; set; }
        public void TurnAction();
    }
}