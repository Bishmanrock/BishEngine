using static OpenGL.GL;

// Contains data for use with the IRenderable interface. The RenderingManager uses this every Draw call.

namespace Engine
{
    public class Renderable
    {
        private float[] vertices;
        private uint[] indices;
        public Shader shader;
        private uint vertexBufferObject;
        public uint vertexArrayObject;
        private uint elementBufferObject;
        private GameObject gameObject;

        private Texture texture0;
        private Texture texture1;

        public unsafe Renderable(float[] vert, uint[] ind, string tex0path, string tex1path, GameObject gameObject)
        {
            this.gameObject = gameObject;
            vertices = vert;
            indices = ind;

            // We enable depth testing here. If you try to draw something more complex than one plane without this,
            // you'll notice that polygons further in the background will occasionally be drawn over the top of the ones in the foreground.
            // Obviously, we don't want this, so we enable depth testing. We also clear the depth buffer in GL.Clear over in OnRenderFrame.
            //glEnable(GL_DEPTH_TEST);

            vertexArrayObject = glGenVertexArray();
            glBindVertexArray(vertexArrayObject);

            vertexBufferObject = glGenBuffer();
            glBindBuffer(GL_ARRAY_BUFFER, vertexBufferObject);

            fixed (float* v = &vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, vertices.Length * sizeof(float), v, GL_STATIC_DRAW);
            }

            elementBufferObject = glGenBuffer();
            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, elementBufferObject);

            fixed (uint* i = &indices[0])
            {
                glBufferData(GL_ELEMENT_ARRAY_BUFFER, indices.Length * sizeof(uint), i, GL_STATIC_DRAW);
            }

            shader = new Shader(ShaderType.TEMP_TRANSFORMATION);

            var vertexLocation = shader.GetAttribLocation("aPosition");
            glEnableVertexAttribArray((uint)vertexLocation);
            glVertexAttribPointer((uint)vertexLocation, 3, GL_FLOAT, false, 5 * sizeof(float), NULL);

            var texCoordLocation = shader.GetAttribLocation("aTexCoord");
            glEnableVertexAttribArray((uint)texCoordLocation);
            glVertexAttribPointer((uint)texCoordLocation, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)(3 * sizeof(float)));

            if (tex0path != null)
            {
                texture0 = Texture.LoadFromFile(tex0path);
            }
            
            if (tex1path != null)
            {
                texture1 = Texture.LoadFromFile(tex1path);
            }

            if (tex0path != null)
            {
                shader.SetInt("texture0", 0);
            }

            if (tex1path != null)
            {
                shader.SetInt("texture1", 1);
            }
        }
        
        // Sets a texture layer
        public void SetTexture()
        {

        }

        public void Draw()
        {

            //shader.Use();
            glBindVertexArray(vertexArrayObject);
            //glBindTexture();

            //shader.SetMatrix4("model", model);
            shader.SetMatrix4("model", gameObject.TransformToModel(gameObject.transform));
            shader.SetMatrix4("view", CameraManager.activeCamera.GetView());
            shader.SetMatrix4("projection", CameraManager.activeCamera.GetProjection());


            texture0.Use(GL_TEXTURE0);
            texture1.Use(GL_TEXTURE1);

            glDrawArrays(GL_TRIANGLES, 0, vertices.Length); // Both GL_TRIANGLES and 36 should be dynamically provided, but at present the Renderable can't see the Cube to do so. Not so sure this should be held on the GameObject class either, as GameObject won't always necesarilly be something renderable, so not sure where this data should be held?
        }
    }
}