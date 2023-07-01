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
        // Get total points in the scene
        totalPoints = GameObject.FindGameObjectsWithTag("Points").Length;
    }

    // Consume the point and check if all the points have been cosumed if yes then Change the scene.
    public void ConsumePoint(GameObject obj){
        Destroy(obj);
        totalPoints -= 1;

        if (totalPoints == 0){
            SceneManager.LoadScene("MainMenu");
        }
    }


}
