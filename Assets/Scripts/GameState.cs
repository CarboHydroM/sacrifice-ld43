using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

    public GameObject mainMenuCanvas;
    public GameObject ingameCanvas;
    public GameObject inGameMenuCanvas;
    public GameObject endGameCanvas;
    public GameObject partyPrefab;

    PartyStat partyInstance;

    /*enum State
    {
        MainMenu,
        InGame,
    }

    State m_state = State.MainMenu;*/

    // Use this for initialization
    void Start () {
        mainMenuCanvas.SetActive(true);
        ingameCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
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
        partyInstance.endGameCanvas = endGameCanvas;
        //m_state = State.MainMenu;
    }

    public void OnExitParty()
    {
        Debug.Log("OnExitParty");
        mainMenuCanvas.SetActive(true);
        ingameCanvas.SetActive(false);
        partyInstance.OnExitParty();
        //m_state = State.InGame;
    }

    public void OnLevelResume()
    {
        Debug.Log("OnLevelResume");
        partyInstance.OnLevelResume();
    }

}
