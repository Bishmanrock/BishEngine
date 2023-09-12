using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class WindowResizeEvent : Event
    {
        int m_Width;
        int m_Height;

        public WindowResizeEvent(EventType type, Key key) : base(type, key)
        {
        }

        public int GetWidth()
        {
            return m_Width;
        }

        public int GetHeight()
        {
            return m_Height;
        }

        public EventType GetEventType()
        {
            return EventType.WindowResize;
        }

        public EventCategory GetEventCategory()
        {
            return EventCategory.Application;
        }
    }

    public class WindowCloseEvent : Event
    {
        public WindowCloseEvent(EventType type, Key key) : base(type, key)
        {
        }

        public EventType GetEventType()
        {
            return EventType.WindowClose;
        }

        public EventCategory GetEventCategory()
        {
            return EventCategory.Application;
        }
    }

    public class AppTickEvent : Event
    {
        //AppTickvent() {}
        // EVENT_CLASS_TYPE(AppTick)
        // EVENT_CLASS_CATEGORY(EventCategoryApplication)
        public AppTickEvent(EventType type, Key key) : base(type, key)
        {
        }
    }

    public class AppUpdateEvent : Event
    {
        //AppUpdateEvent() {}
        // EVENT_CLASS_TYPE(AppUpdate)
        // EVENT_CLASS_CATEGORY(EventCategoryApplication)
        public AppUpdateEvent(EventType type, Key key) : base(type, key)
        {
        }
    }

    public class AppRenderEvent : Event
    {
        //AppRenderEvent() {}
        // EVENT_CLASS_TYPE(AppRender)
        // EVENT_CLASS_CATEGORY(EventCategoryApplication)

        //public AppRenderEvent()
        //{
        //    eventCategory = EventCategory.Application;
        //}
        public AppRenderEvent(EventType type, Key key) : base(type, key)
        {
        }
    }
}