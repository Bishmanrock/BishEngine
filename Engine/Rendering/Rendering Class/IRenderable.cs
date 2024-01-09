using static OpenGL.GL;

// Interface used to hold all data for an object that needs to be rendered

namespace Engine
{
    public interface IRenderable : IObject
    {
        public Renderable renderData { get; }

        public void Render()
        {
            if (isActive == true)
            {
                renderData.Draw();
            }
        }
    }
}