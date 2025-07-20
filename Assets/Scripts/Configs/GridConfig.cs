using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/DataGrid")]
    public class GridConfig : ScriptableObject
    {
        [SerializeField] private List<Grid> _grids = new ();

        public Grid GetCurrentGridConfig(int gridId) => 
            _grids.Find(x => x.numTir == gridId);
    }

    [Serializable]
    public class Grid
    {
        public int numTir;
        public Vector2Int size;
        public int numKeys;
    }
}