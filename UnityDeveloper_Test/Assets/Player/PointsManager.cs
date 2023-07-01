using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointsManager : MonoBehaviour
{
    private int totalPoints;
    // Start is called before the first frame update
    void Start()
    {
        totalPoints = GameObject.FindGameObjectsWithTag("Points").Length;
    }

    public void ConsumePoint(GameObject obj){
        Destroy(obj);
        totalPoints -= 1;

        if (totalPoints == 0){
            SceneManager.LoadScene("MainMenu");
        }
    }


}
