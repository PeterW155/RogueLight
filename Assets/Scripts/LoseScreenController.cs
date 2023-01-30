using UnityEngine;
using UnityEngine.UI;

public class LoseScreenController : MonoBehaviour
{
    [SerializeField] private Text _score;

    private void OnEnable()
    {
        _score.text = "Points: " + ScoreManager.instance.score.ToString();
    }
}
