namespace Engine
{
    public class MouseEvent : Event
    {
        public MouseEvent(EventType type, Key key) : base(type, key)
        {

        }
    }

    public class MouseMoved : MouseEvent
    {
        float m_MouseX;
        float m_MouseY;

        public MouseMoved(EventType type, Key key) : base(type, key)
        {
        }

        public EventType GetEventType()
        {
            return EventType.MouseMoved;
        }

        public EventCategory GetEventCategory()
        {
            return EventCategory.Mouse | EventCategory.Input;
        }
    }

    public class MouseScrolled : MouseEvent
    {
        float xOffset;
        float yOffset;

        public MouseScrolled(EventType type, Key key) : base(type, key)
        {
        }

        public EventType GetEventType()
        {
            return EventType.MouseScrolled;
        }

        public EventCategory GetEventCategory()
        {
            return EventCategory.Mouse | EventCategory.Input;
        }
    }

    public class MouseButtonEvent : MouseEvent
    {
        float button;

        public MouseButtonEvent(EventType type, Key key) : base(type, key)
        {
        }

        public float GetMouseButton()
        {
            return button;
        }

        public EventType GetEventType()
        {
            return EventType.MouseScrolled;
        }

        public EventCategory GetEventCategory()
        {
            return EventCategory.Mouse | EventCategory.Input;
        }
    }

    public class MouseButtonPressedEvent : MouseButtonEvent
    {
        float button;

        public MouseButtonPressedEvent(EventType type, Key key) : base(type, key)
        {
        }

        public float GetMouseButton()
        {
            return button;
        }

        public EventType GetEventType()
        {
            return EventType.MouseScrolled;
        }

        public EventCategory GetEventCategory()
        {
            return EventCategory.Mouse | EventCategory.Input;
        }
    }

    public class MouseButtonReleasedEvent : MouseButtonEvent
    {
        float button;

        public MouseButtonReleasedEvent(EventType type, Key key) : base(type, key)
        {
        }

        public float GetMouseButton()
        {
            return button;
        }

        public EventType GetEventType()
        {
            return EventType.MouseButtonReleased;
        }

        public EventCategory GetEventCategory()
        {
            return EventCategory.Mouse | EventCategory.Input;
        }
    }
}