using static OpenGL.GL;

// Class to instantly instantiate a cube GameObject

namespace Engine
{
    public class Quad : GameObject, IRenderable
    {
        public Renderable renderData { get; }

        Mesh mesh;

        // Why are so many vertcies needed for a cube??
        private readonly float[] _vertices =
    {
     0.5f,  0.5f, 0.0f,  // top right
     0.5f, -0.5f, 0.0f,  // bottom right
    -0.5f, -0.5f, 0.0f,  // bottom left
    -0.5f,  0.5f, 0.0f   // top left 
            };

        private readonly uint[] _indices =
        {
                0, 1, 3,   // first triangle
                1, 2, 3    // second triangle
        };

        public unsafe Quad()
        {

            renderData = new Renderable(_vertices, _indices, "F:\\GameDev\\.Engine\\Engine\\Engine\\Graphics\\Fonts\\Font - System 16x16.png", null);
        }

        public Shader GetShader()
        {
            return renderData.shader;
        }

        public unsafe void Draw()
        {
            // Then, we pass all of these matrices to the vertex shader.
            // You could also multiply them here and then pass, which is faster, but having the separate matrices available is used for some advanced effects.

            // IMPORTANT: OpenTK's matrix types are transposed from what OpenGL would expect - rows and columns are reversed.
            // They are then transposed properly when passed to the shader. 
            // This means that we retain the same multiplication order in both OpenTK c# code and GLSL shader code.
            // If you pass the individual matrices to the shader and multiply there, you have to do in the order "model * view * projection".
            // You can think like this: first apply the modelToWorld (aka model) matrix, then apply the worldToView (aka view) matrix, 
            // and finally apply the viewToProjectedSpace (aka projection) matrix.

            // For some reason you need to set the scale as 1. I imagine this is because the gameobject is initialising at 0, 0, 0.


            //shader.SetMatrix4("model", model);
            renderData.shader.SetMatrix4("model", TransformToModel(transform));
            renderData.shader.SetMatrix4("view", CameraManager.activeCamera.GetView());
            renderData.shader.SetMatrix4("projection", CameraManager.activeCamera.GetProjection());

            glBindVertexArray(renderData.vertexArrayObject);

            glDrawArrays(GL_TRIANGLES, 0, 6);
        }

        /// <summary>
        /// Creates the matrix which transforms vertex positions into world positions.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns>The transformation matrix.</returns>
        private Matrix4x4 TransformToModel(Transform transform)
        {
            return Matrix4x4.CreateRotationX(transform.rotation.x) *
                Matrix4x4.CreateRotationY(transform.rotation.y) *
                 Matrix4x4.CreateRotationZ(transform.rotation.z) *
                Matrix4x4.CreateScale(transform.scale) *
                 Matrix4x4.CreateTranslation(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}