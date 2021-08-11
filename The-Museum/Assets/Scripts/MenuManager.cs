using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectPanel;

    [SerializeField] private InputField UsernameInput;
    [SerializeField] private InputField CreateGameInput;
    [SerializeField] private InputField JoinGameInput;

    [SerializeField] private GameObject StartButton;
    
    public static bool online = false;


    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersionName);
    }

    private void OnFailedToConnectToMasterServer()
    {
        if (PhotonNetwork.JoinLobby(TypedLobby.Default))
        {
            Debug.Log("Connected");
        }
        else
        {
            Debug.Log("Failed To Connect");
        }
        
    }


   /* private void Start()
    {
        UsernameMenu.SetActive(true);
    }*/

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

    public void PlayGame()
    {
        Time.timeScale = 1.0f;
        PlayerManager.keyCount = 0;
        PlayerManager.liveCount = 3;
        PauseMenu.GameIsPaused = false;
        online = false;
        SceneManager.LoadScene("MuseumRoom");
        
    }

    public void ChangeUsernameInput()
    {
        if(UsernameInput.text.Length >= 3)
        {
            StartButton.SetActive(true);

        }
        else
        {
            StartButton.SetActive(false);
        }
    }

    public void SetUsername()
    {
        UsernameMenu.SetActive(false);
        ConnectPanel.SetActive(true);
        PhotonNetwork.playerName = UsernameInput.text;
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { MaxPlayers = 5 }, null);
    }

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MuseumRoom");
        online = true;
        Debug.Log("Joining Room");
    }
}