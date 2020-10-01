using Tetris.Game.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Tetris.Game.Views.GameOver
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        
        private ITetrisGame _game;
        
        [Inject]
        public void Initialize(ITetrisGame game)
        {
            _game = game;
            
            _game.GameOver += OnGameOver;
            
            _restartButton.onClick.AddListener(() =>
            {
                _game.Begin();
                gameObject.SetActive(false);
            });
            
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _game.GameOver -= OnGameOver;
        }

        private void OnGameOver()
        {
            gameObject.SetActive(true);
        }
    }
}