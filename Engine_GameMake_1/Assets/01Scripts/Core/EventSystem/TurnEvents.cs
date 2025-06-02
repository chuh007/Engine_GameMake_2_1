namespace _01Scripts.Core.EventSystem
{
    public class TurnEvents
    {
        public static readonly TurnEndEvent TurnEndEvent = new TurnEndEvent();
    }

    public class TurnEndEvent : GameEvent
    {
        
    }
}