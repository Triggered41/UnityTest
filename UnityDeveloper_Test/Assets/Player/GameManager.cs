using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;
public class GameManager : MonoBehaviour
{
    public TMP_Text timerText;
    public Timer t;
    // Start is called before the first frame update
    void Start()
    {
        t.StartTimer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timerText.text = t.time.x.ToString() + "m : " + Mathf.Round(t.time.y).ToString() + "s";
    }

    public void OnTimerEnd(){
        print("GameOver");
        SceneManager.LoadScene("GameOver");

    }

}
