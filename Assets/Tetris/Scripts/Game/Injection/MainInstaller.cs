using Tetris.Game.Models;
using Tetris.Game.Models.Blocks;
using Tetris.Scripts.Game.Controllers;
using Tetris.Scripts.Game.Views;
using UnityEngine;
using Zenject;

namespace Tetris.Game.Injection
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private BlockSetData _blockSetData;
        
        public override void InstallBindings()
        {
            Container.BindInstance<IBlockSetProvider>(_blockSetData).AsSingle();
            Container.BindInstance<IBlockMaterialProvider>(_blockSetData).AsSingle();
            Container.Bind<ITetrisGame>().To<GameController>().AsSingle().NonLazy();
        }
    }
}