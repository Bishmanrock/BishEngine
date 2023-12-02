// Seems to be some sort of Event handler, primarily for input. But this isn't how input is handled.

namespace Engine
{
    public abstract class Event
    {
        private EventType eventType;
        public EventCategory eventCategory;
        private Key key;

        bool m_Handled = false; // Whether the event has been handled yet

        public Event(EventType type, Key key)
        {
            this.eventType = type;
            this.key = key;
        }

        public EventType Type => Type;
        public Key Key => key;

        //public bool IsInCategory(EventCategory category)
        //{
        //return GetCategoryFlags() & category;
        // }
    }
}