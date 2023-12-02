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

        private List<Texture> textureList = new List<Texture>();

        //public unsafe Renderable(float[] vert, uint[] ind, string tex0path, string tex1path, GameObject gameObject)
        public unsafe Renderable(float[] vert, uint[] ind, Texture tex0path, Texture tex1path, GameObject gameObject)
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

            SetTexture(tex0path, 0);
            SetTexture(tex1path, 1);

            if (tex0path != null)
            {
                texture0 = tex0path;
            }

            if (tex1path != null)
            {
                texture1 = tex1path;
            }

            if (tex0path != null)
            {
                shader.SetInt("tex0path", 0);
                //shader.SetInt(textureList[0].ToString(), 0);
            }

            if (tex1path != null)
            {
                shader.SetInt("tex1path", 1);
                //shader.SetInt(textureList[1].ToString(), 1);
            }
        }
      
        // Sets a texture layer
        public void SetTexture(Texture texture, int layer)
        {
            textureList.Insert(layer, texture); // Inserts the Texture at the specific index, which corresponds to the layer
        }

        private void ApplyTextureList()
        {
            foreach(Texture texture in textureList)
            {
                //shader.SetInt("texture1", 1);
            }
        }

        public void Draw()
        {
            if (gameObject.isActive == false) return;

            glBindVertexArray(vertexArrayObject);
            //glBindTexture();

            //shader.SetMatrix4("model", model);
            shader.SetMatrix4("model", gameObject.TransformToModel(gameObject.transform));
            shader.SetMatrix4("view", CameraManager.activeCamera.GetView());
            shader.SetMatrix4("projection", CameraManager.activeCamera.GetProjection());

            texture0.Use(GL_TEXTURE0);
            texture1.Use(GL_TEXTURE1);

            glDrawArrays(GL_TRIANGLES, 0, vertices.Length); // Works for now, but not so sure vertices.Length is correct. GL_TRIANGLES also needs to be dynamic if other values will be required.
        }
    }
}