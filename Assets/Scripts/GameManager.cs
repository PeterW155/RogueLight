using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action GameLost; 

    [SerializeField] private GameObject _loseScreen;
    
    public void LoseGame()
    {
        Time.timeScale = 0.0f;
        if (_loseScreen)
        {
            _loseScreen.SetActive(true);
        }
        GameLost?.Invoke();
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    private void Awake()
    {
        Instance = this;
    }
}
