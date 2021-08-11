using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    public string sceneName;
    public static bool[] visited;
    public int ID;
    public GameObject winMenuUI;
    public GameObject keepPlayingUI;

    void Start()
    {
        visited = new bool[5];    
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (!visited[ID])
        {
            if (coll.gameObject.tag == "Player")
            {
                if (ID != 2)
                {
                    visited[ID] = true;
                    SceneManager.LoadScene(sceneName);

                    PlayerManager.inGame = true;
                }
                else
                {
                    if (PlayerManager.keyCount >= 5)
                    {
                        Time.timeScale = 0.0f;
                        winMenuUI.SetActive(true);
                    }
                    else
                    {
                        keepPlayingUI.SetActive(true);
                        Invoke("TurnOffUI", 3.0f);
                    }
                }
            }
            
        }
    }

    void TurnOffUI()
    {
        keepPlayingUI.SetActive(false);
    }
}
