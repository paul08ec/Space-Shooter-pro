using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private GameManager _gameManager;

    
    void Start()
    {
       
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
       
        if(_gameManager == null)
        {
            Debug.LogError("GameManager is null.");
        }
        
    }

   public void UpdateScore(int playerScore)
    {
        _scoreText.text =("Score: ") + playerScore.ToString();
    }
    public void UpdateLives(int curretLives)
    {
        _livesImg.sprite = _liveSprites[curretLives];
        if (curretLives ==0)
        {
            GameOverSecuence();
            
        }
        void GameOverSecuence()
        {
            _gameManager.GameOver();
            _gameOverText.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlikerRoutine());

        }
        IEnumerator GameOverFlikerRoutine()
        {
            while(true)
            {
                _gameOverText.text = "GAME OVER";
                yield return new WaitForSeconds(0.5f);
                _gameOverText.text = "";
                yield return new WaitForSeconds(0.5f);
            }
        }


    }
}
