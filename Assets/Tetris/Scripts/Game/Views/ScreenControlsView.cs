using Tetris.Game.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Tetris.Game.Views
{
    public class ScreenControlsView : MonoBehaviour
    {
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _leftRotateButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _rightRotateButton;
        [SerializeField] private Button _quickDropButton;

        [Inject]
        public void Initialize(ITetrisGame game)
        {
            _leftButton.onClick.AddListener(game.LeftShift);
            _leftRotateButton.onClick.AddListener(game.LeftRotate);
            _rightButton.onClick.AddListener(game.RightShift);
            _rightRotateButton.onClick.AddListener(game.RightRotate);
            _quickDropButton.onClick.AddListener(game.QuickFall);

#if UNITY_ANDROID || UNITY_IOS
            gameObject.SetActive(true);
#else
            gameObject.SetActive(false);
#endif
        }
    }
}