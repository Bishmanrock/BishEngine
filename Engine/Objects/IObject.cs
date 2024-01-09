using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public interface IObject
    {
        public Transform transform { get; set; }

        public bool isActive { get; set; }  // Whether the GameObject is active or not
    }
}