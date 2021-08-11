using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static int keyCount = 0;
    public static int liveCount = 3;
    public static float gameTimer = 30.0f;
    public static bool inGame;

    public GameObject PlayerPrefab;

    public GameObject lostLifeUI;
    public GameObject gameLostUI;
    public Text liveScore;
    public Text keyScore;
    public Text timerUI;

    public GameObject player;

    static float lostlifeTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        liveScore = GameObject.Find("LivesText").GetComponent<Text>();
        liveScore.text = "LIVES: " + liveCount.ToString();

        keyScore = GameObject.Find("KeysText").GetComponent<Text>();
        keyScore.text = "KEYS: " + keyCount.ToString();

        timerUI = GameObject.Find("GameTimer").GetComponent<Text>();

        // spawn player
        // TODO: Update with random position.
        if (MenuManager.online)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(this.transform.position.x * 1f, this.transform.position.y * 1f), Quaternion.identity, 0);
        }
        else
        {
            player.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        liveScore.text = "LIVES: " + liveCount.ToString();

        keyScore.text = "KEYS: " + keyCount.ToString();

        

        if (inGame)
        {

            timerUI.text = "TIMER: " + gameTimer.ToString("f2");

            if (gameTimer <= 0.0f)
            {

                lostLifeUI.SetActive(true);
                PauseMenu.GameIsPaused = true;

                lostlifeTimer += Time.deltaTime;
                if (lostlifeTimer > 4.0f)
                {

                    gameTimer = 5.0f;
                    inGame = false;
                    lostLifeUI.SetActive(false);
                    lostlifeTimer = 0.0f;
                    PauseMenu.GameIsPaused = false;
                    liveCount--;
                    SceneManager.LoadScene("MuseumRoom");

                }


            }
            if (gameTimer > 0)
            {
                gameTimer -= Time.deltaTime;
            }
            else
            {
                gameTimer = 0.0f;
            }
        }
        else
        {
            timerUI.text = " ";
            gameTimer = 30.0f;
        }

        if(liveCount <= 0)
        {
            if (!inGame)
            {
                PauseMenu.GameIsPaused = true;
                gameLostUI.SetActive(true);
            }
        }
    }

}
