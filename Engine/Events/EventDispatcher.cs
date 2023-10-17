// The EventDispatcher is used to cycle through event types when an event happens, find the correct Event based on EventType, and then execute it. It's basically a way of being able to focus in on specific Event types, rather than cycling through every Event to find the correct one.

namespace Engine
{
    public class EventDispatcher
    {
        //Event event;

        //public EventDispatcher(Event event)
        //{
        //    this.event = event;
        //}

        public bool Dispatch()
        {
            return false;
        }
    }
}