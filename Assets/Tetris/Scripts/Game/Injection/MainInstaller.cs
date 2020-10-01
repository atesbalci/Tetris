using Tetris.Game.Models;
using Tetris.Game.Models.Blocks;
using Tetris.Game.Views;
using Tetris.Game.Views.Blocks;
using Tetris.Helpers.Coroutines;
using Tetris.Helpers.Coroutines.Impl;
using Tetris.Scripts.Game.Controllers;
using UnityEngine;
using Zenject;

namespace Tetris.Game.Injection
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private BlockSetData _blockSetData;
        [SerializeField] private CoroutineRunner _coroutineRunner;
        [SerializeField] private GameObject _blockPrefab;
        [SerializeField] private Transform _blockPoolParent;
        [SerializeField] private Transform _blockSpawnedParent;
        [SerializeField] private Transform _previewParent;
        
        public override void InstallBindings()
        {
            Container.BindInstance<IBlockSetProvider>(_blockSetData).AsSingle();
            Container.BindInstance<IBlockMaterialProvider>(_blockSetData).AsSingle();
            Container.BindInstance<ICoroutineRunner>(_coroutineRunner).AsSingle();
            Container.Bind<ITetrisGame>().To<GameController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockViewsController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PreviewController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<KeyboardInputController>().AsSingle().NonLazy();
            Container.BindInstance(_blockSpawnedParent).WhenInjectedInto<BlockViewsController>();
            Container.BindInstance(_previewParent).WhenInjectedInto<PreviewController>();
            Container.BindMemoryPool<BlockView, BlockPool>()
                .WithInitialSize(300)
                .FromComponentInNewPrefab(_blockPrefab)
                .UnderTransform(_blockPoolParent)
                .AsSingle();
        }
    }
}