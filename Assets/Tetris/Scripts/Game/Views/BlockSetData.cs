using System;
using System.Collections.Generic;
using Tetris.Game.Models.Blocks;
using UnityEngine;

namespace Tetris.Scripts.Game.Views
{
    [CreateAssetMenu]
    public class BlockSetData : ScriptableObject, IBlockSetProvider, IBlockMaterialProvider
    {
        [SerializeField] private Entry[] _entries;

        public IEnumerable<Vector2Int> GetBlockSet(BlockType type) => _entries[(int) type].Locations;
        public Material GetMaterial(BlockType type) => _entries[(int) type].Material;

        [Serializable]
        private class Entry
        {
            public Material Material;
            public Vector2Int[] Locations;
        }
    }
}