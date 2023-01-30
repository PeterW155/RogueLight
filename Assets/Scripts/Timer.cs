using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float timeStart = 0f;
    float currentTime = 0f;
    [SerializeField] Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = timeStart.ToString();
        currentTime = timeStart;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1 * Time.deltaTime;
        textBox.text = "Time: " + currentTime.ToString("0");
    }
}
