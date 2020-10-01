using System;
using Tetris.Game.Models.Blocks;
using Random = UnityEngine.Random;

namespace Tetris.Game.Models
{
    public static class GameRules
    {
        public const int Height = 20;
        public const int Width = 10;
        public const float FallInterval = 0.75f;
        public const float DropFallInterval = 0.05f;
        
        private static readonly int BlockTypeCount = Enum.GetValues(typeof(BlockType)).Length;

        public static BlockType GetRandomBlockType()
        {
            return (BlockType) Random.Range(0, BlockTypeCount);
        }
    }
}