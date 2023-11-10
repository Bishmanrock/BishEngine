using static OpenGL.GL;

// Interface used to hold all data for an object that needs to be rendered

namespace Engine
{
    interface IRenderable
    {
        public Renderable renderData { get; }
    }
}