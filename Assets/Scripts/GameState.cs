using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{

    public GameObject mainMenuCanvas;
    public GameObject ingameCanvas;
    public GameObject inGameMenuCanvas;
    public GameObject endGameCanvas;
    public GameObject partyPrefab;
    public GameObject gameOverCanvas;

    public bool[] playerAreIA = { false, false, false, false };

    PartyStat partyInstance;

    /*enum State
    {
        MainMenu,
        InGame,
    }

    State m_state = State.MainMenu;*/

    // Use this for initialization
    void Start()
    {
        mainMenuCanvas.SetActive(true);
        ingameCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnQuit()
    {
        Debug.Log("OnQuit");
        Application.Quit();
    }

    public void OnStartParty()
    {
        Debug.Log("OnStartParty");

        mainMenuCanvas.SetActive(false);
        ingameCanvas.SetActive(true);
        partyInstance = Instantiate(partyPrefab).GetComponent<PartyStat>();
        partyInstance.gameStat = this;
        partyInstance.inGameMenuCanvas = inGameMenuCanvas;
        partyInstance.inGameCanvas = ingameCanvas;
        partyInstance.endGameCanvas = endGameCanvas;
        partyInstance.gameOverCanvas = gameOverCanvas;
        partyInstance.StartLevel(1);
        //m_state = State.MainMenu;
    }

    public void OnExitParty()
    {
        gameOverCanvas.SetActive(false);
        Debug.Log("OnExitParty");
        mainMenuCanvas.SetActive(true);
        ingameCanvas.SetActive(false);
        partyInstance.OnExitParty();
        Destroy(partyInstance);
        //m_state = State.InGame;

        // stop music when going back to title
        GameObject musicSys = GameObject.FindGameObjectWithTag("Music");
        Debug.Assert(musicSys);
        MusicSystem ms = musicSys.GetComponent<MusicSystem>();
        ms.Stop();
    }

    public void OnLevelResume()
    {
        Debug.Log("OnLevelResume");
        partyInstance.OnLevelResume();
    }

    public void OnPlayer2_IsIA()
    {
        playerAreIA[1] = !playerAreIA[1];
    }
    public void OnPlayer3_IsIA() { playerAreIA[2] = !playerAreIA[2]; }
    public void OnPlayer4_IsIA() { playerAreIA[3] = !playerAreIA[3]; }
}
