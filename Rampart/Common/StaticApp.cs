using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rampart.Common
{
    public static class StaticApp
    {
        private static Random _rand;

        public static Random RandomNum { get { return _rand ?? (_rand = new Random()); } }
    }
}
