﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai
{
    public class KeyPressEventArgs : EventArgs
    {
        public char KeyChar
        {
            get;
            set;
        }
    }
}