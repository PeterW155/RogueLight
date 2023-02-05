using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject _loseScreen;
    
    public void LoseGame()
    {
        Time.timeScale = 0.0f;
        if (_loseScreen)
        {
            _loseScreen.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
    }
    
    private void Awake()
    {
        Instance = this;
    }
}
