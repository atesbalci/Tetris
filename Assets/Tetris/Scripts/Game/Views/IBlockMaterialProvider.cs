using Tetris.Game.Models.Blocks;
using UnityEngine;

namespace Tetris.Game.Views
{
    public interface IBlockMaterialProvider
    {
        Material GetMaterial(BlockType type);
    }
}