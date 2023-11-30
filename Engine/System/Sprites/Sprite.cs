namespace Engine
{
    class Sprite : GameObject
    {
        public Texture texture;

        public Sprite(Texture texture)
        {
            this.texture = texture;
        }
    }
}