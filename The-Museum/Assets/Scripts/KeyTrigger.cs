using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyTrigger : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerManager.inGame = false;
            Destroy(gameObject);
            PlayerManager.keyCount++;
            Debug.Log("Load Museum Room");
            PauseMenu.GameIsPaused = false;
            SceneManager.LoadScene("MuseumRoom");
        }
    }
}
