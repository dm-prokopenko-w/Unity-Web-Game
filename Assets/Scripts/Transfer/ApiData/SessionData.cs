using System;
using System.Collections.Generic;

namespace Transfer.ApiData
{
    [Serializable]
    public class SessionData
    {
        public int seed;
        public int tier;
        public int progress;
        public List<OpenedCell> openedCells;
    }
}