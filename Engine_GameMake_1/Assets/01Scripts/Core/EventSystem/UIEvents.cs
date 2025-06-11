namespace _01Scripts.Core.EventSystem
{
    public static class UIEvents
    {
        public static readonly FadeEvent FadeEvent = new FadeEvent();
        public static readonly FadeCompleteEvent FadeCompleteEvent = new FadeCompleteEvent();
        public static readonly HealthItemEvent HealthItemEvent = new HealthItemEvent();
        public static readonly AttackItemEvent AttackItemEvent = new AttackItemEvent();
    }
    
    public class FadeEvent : GameEvent
    {
        public bool isFadeIn;
        public float fadeTime;
        public bool isSaveOrLoad; //저장이나 로딩을 하는거냐?
    }

    public class FadeCompleteEvent : GameEvent { }

    public class HealthItemEvent : GameEvent
    {
        public int itemCount;
    }
    
    public class AttackItemEvent : GameEvent
    {
        public int itemCount;
    }
}