using static OpenGL.GL;
using StbImageSharp;

// This manager holds a reference to all loaded textures in the game, so that they don't have to be reloaded every time

namespace Engine
{
    public static class TextureManager
    {
        static Dictionary<string, Texture> textureDictionary = new Dictionary<string, Texture>();

        // Returns the Texture
        public static Texture GetTexture(string name)
        {
            switch (textureDictionary.ContainsKey(name))
            {
                case true:
                    return textureDictionary[name];
                case false:
                    Console.WriteLine($"Texture {name} was not present in the Texture Dictionary");
                    return null;
            }
        }

        // Generates a reference to the Texture internally. Means you don't have to use a filepath every time. For testing now, this can maybe later just be merged with LoadFromFile rather than spreading it across two functions, if there's no requirement for that to be seperate.
        public static void AddTexture(string name, string location)
        {
            Texture tex = LoadFromFile(location);
            textureDictionary.Add(name, tex);
        }

        public static Texture LoadFromFile(string path)
        {
            // Generate handle
            uint handle = glGenTexture();

            // Bind the handle
            glActiveTexture(GL_TEXTURE0);
            glBindTexture(GL_TEXTURE_2D, handle);

            // For this example, we're going to use .NET's built-in System.Drawing library to load textures.

            // OpenGL has it's texture origin in the lower left corner instead of the top left corner,
            // so we tell StbImageSharp to flip the image when loading.
            StbImage.stbi_set_flip_vertically_on_load(1);

            // Here we open a stream to the file and pass it to StbImageSharp to load.
            using (Stream stream = File.OpenRead(path))
            {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

                // Now that our pixels are prepared, it's time to generate a texture. We do this with GL.TexImage2D.

                glTexImage2D(
                    GL_TEXTURE_2D, // The type of texture we're generating. There are various different types of textures, but the only one we need right now is Texture2D.
                    0, // Level of detail. We can use this to start from a smaller mipmap (if we want), but we don't need to do that, so leave it at 0.
                    GL_RGBA, // Target format of the pixels. This is the format OpenGL will store our image with.
                    image.Width, // Width of the image
                    image.Height, // Height of the image.
                    0, // Border of the image. This must always be 0; it's a legacy parameter that Khronos never got rid of.
                    GL_RGBA, // The format of the pixels, explained above. Since we loaded the pixels as RGBA earlier, we need to use PixelFormat.Rgba.
                    GL_UNSIGNED_BYTE, // Data type of the pixels.
                    ConvertToIntPtr(image.Data)); // The actual pixels
            }

            // Now that our texture is loaded, we can set a few settings to affect how the image appears on rendering.

            // First, we set the min and mag filter. These are used for when the texture is scaled down and up, respectively.
            // Here, we use Linear for both. This means that OpenGL will try to blend pixels, meaning that textures scaled too far will look blurred.
            // You could also use (amongst other options) Nearest, which just grabs the nearest pixel, which makes the texture look pixelated if scaled too far.
            // NOTE: The default settings for both of these are LinearMipmap. If you leave these as default but don't generate mipmaps,
            // your image will fail to render at all (usually resulting in pure black instead).
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);

            // Now, set the wrapping mode. S is for the X axis, and T is for the Y axis.
            // We set this to Repeat so that textures will repeat when wrapped. Not demonstrated here since the texture coordinates exactly match
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

            // Next, generate mipmaps.
            // Mipmaps are smaller copies of the texture, scaled down. Each mipmap level is half the size of the previous one
            // Generated mipmaps go all the way down to just one pixel.
            // OpenGL will automatically switch between mipmaps when an object gets sufficiently far away.
            // This prevents moiré effects, as well as saving on texture bandwidth.
            // Here you can see and read about the morié effect https://en.wikipedia.org/wiki/Moir%C3%A9_pattern
            // Here is an example of mips in action https://en.wikipedia.org/wiki/File:Mipmap_Aliasing_Comparison.png
            glGenerateMipmap(GL_TEXTURE_2D);

            return new Texture(handle);
        }

        // Converts a byte array to a IntPtr
        public static IntPtr ConvertToIntPtr(byte[] array)
        {

            using PinnedArray pinnedArray = new(array);

            //GCHandle pinnedArray = GCHandle.Alloc(array, GCHandleType.Pinned); // Pin the array to prevent it from changing while running
            IntPtr pointer = pinnedArray.Pointer;
            //pinnedArray.Free(); // Free up the pinned array from memory

            return pointer;
        }
    }
}
