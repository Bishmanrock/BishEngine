using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This is the class for a bitmap font

namespace Engine
{
    //Texture font;

    class BitFont
    {
        //Dictionary<char, Letter> letters { get; } = new Dictionary<char, Letters>();

        string allowedLetters { get; } = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        //public BitFont(Bitmap bitimage, int glyphsPerLine, int glyphsPerCol)
        //{
            // Calc vertices
            // Create new letter object per allowed letter with the corresponding vertices
        //}
    }

    class Letter:GameObject
    {
        // Add renderable interface, constructor, etc.
    }

    // Function to render text. More paramters may be required depending on preference.
    //public void RenderText(string txt, Vector3 position, float scale, BitFont font)
    //{
        // for each letter in 'txt' get the corresponding object from the font object's letter dictionary

        // set that ;letter objects position, scale etc. to the parameters and then render it like a normal object

        // make sure to take into account that 'Positoon' only pints to the leftmost letter
    //}
}