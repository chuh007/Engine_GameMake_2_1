namespace _01Scripts.Core.EventSystem
{
    public class PlayerEvents
    {
        public static readonly AddEXPEvent AddExpEvent = new AddEXPEvent();
    }
    
    public class AddEXPEvent : GameEvent
    {
        public int exp;
    }
}