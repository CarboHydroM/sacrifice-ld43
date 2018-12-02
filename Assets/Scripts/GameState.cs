using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

    public GameObject mainMenuCanvas;
    public GameObject ingameCanvas;
    public GameObject inGameMenuCanvas;
    public GameObject partyPrefab;

    PartyStat partyInstance;

    enum State
    {
        MainMenu,
        InGame,
        InGameMenu,
    }

    State m_state = State.MainMenu;

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
        Application.Quit();
    }

    public void OnStartParty()
    {
        mainMenuCanvas.SetActive(false);
        ingameCanvas.SetActive(true);
        partyInstance = Instantiate(partyPrefab).GetComponent<PartyStat>();
        partyInstance.gameStat = this;
        partyInstance.inGameMenuCanvas = inGameMenuCanvas;
    }

    public void OnExitParty()
    {
        mainMenuCanvas.SetActive(true);
        ingameCanvas.SetActive(false);
        partyInstance.OnExitParty();
    }

    public void OnLevelResume()
    {
        partyInstance.OnLevelResume();
    }

}
