using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public abstract class KeyEvent : Event
    {
        int m_KeyCode;

        protected KeyEvent(EventType type, Key key) : base(type, key)
        {
        }

        //protected KeyEvent(EventType type, Key key)
        //{
        //    eventCategory = (EventCategory.Keyboard | EventCategory.Input);
        //}

        public abstract void GetKeyCode();
    }

    public class KeyPressedEvent : KeyEvent
    {
        public KeyPressedEvent(EventType type, Key key) : base(type, key)
        {
        }

        //KeyPressedEvent(int keycode, int repeatCount)
        //{
        //    KeyEvent(keycode);
        //}

        public override void GetKeyCode()
        {
        }

        public EventType GetEventType()
        {
            return EventType.KeyPressed;
        }
    }

    public class KeyReleasedEvent : KeyEvent
    {
        public KeyReleasedEvent(EventType type, Key key) : base(type, key)
        {
        }

        public override void GetKeyCode()
        {

        }

        public EventType GetEventType()
        {
            return EventType.KeyReleased;
        }
    }
}