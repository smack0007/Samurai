﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai
{
    public class KeyEventArgs : EventArgs
    {
        public Key Key
        {
            get;
            set;
        }
    }
}
