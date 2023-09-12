using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// These are the in-game actions, such as "Shoot" or "Move", that are then binded to keyboard/mouse inputs

namespace Engine
{
    public enum Actions
    {
        Up,
        Down,
        Left,
        Right,
        Action,
        Cancel,
        Menu
    }
}