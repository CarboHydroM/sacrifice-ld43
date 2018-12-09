using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyStat : MonoBehaviour {
    private GameObject musicSys;
    public GameObject inGameMenuCanvas;
    public GameObject inGameCanvas;
    public GameObject endGameCanvas;

    public int[] score = { 0, 0, 0, 0 };
    public int[] ammoConsumption = { 0, 0, 0, 0 };
    public int[] ennemyKills = { 0, 0, 0, 0 };
    public int[] wastedAmmo = { 0, 0, 0, 0 };
    public int ammoStock = 4000;
    public float nacelleWeight = 5f;
    public float bulletWeight = 0.0004f;
    public float playerWeight = 0.25f;
    public float altitude = 0f;
    public float nacellePower = 10f;
    public float nacelleSpeed = 0f;
    public int balloonLifeStart = 100;
    public GameState gameStat;
    public GameObject gameOverCanvas;

    int m_balloonLife;
    public int balloonLife
    {
        get
        {
            return m_balloonLife;
        }
    }

    public HashSet<int> droppedPlayers = new HashSet<int>();

    public int m_currentLevelIdx = 0;
    AsyncOperation loader;
    string loadedSceneName;
    public void StartLevel(int levelIdx)
    {
        musicSys = GameObject.FindGameObjectWithTag("Music");
        Debug.Assert(musicSys);

        Time.timeScale = 1f;
        m_currentLevelIdx = levelIdx;
        if (m_currentLevelIdx == 5)
        {
            inGameCanvas.SetActive(false);
            loadedSceneName = "End";
        }
        else
        {
            loadedSceneName = "Level" + levelIdx.ToString();
            MusicSystem ms = musicSys.GetComponent<MusicSystem>();
            ms.PlayClip(m_currentLevelIdx);
        }

        loader = SceneManager.LoadSceneAsync("Scenes/" + loadedSceneName, LoadSceneMode.Additive);
        //loader.allowSceneActivation = false;
        firstUpdateInLevel = true;
        m_balloonLife = balloonLifeStart;
    }

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    bool firstUpdateInLevel = true;
    void Update () {
        if (loader.isDone == false)
            return;
        if (firstUpdateInLevel)
        {
            firstUpdateInLevel = false;
            Scene scene = SceneManager.GetSceneByName(loadedSceneName);
            SceneManager.SetActiveScene(scene);
        }

        float speedFactor = 0.3f;
        float totalWeight = nacelleWeight + (bulletWeight * ammoStock) -
                            (playerWeight * droppedPlayers.Count);
        nacelleSpeed = (nacellePower - totalWeight) * speedFactor;
        altitude += nacelleSpeed * Time.deltaTime;

        if (Input.GetButton("Menu"))
        {
            Time.timeScale = 0f;
            inGameMenuCanvas.SetActive(true);
        }
    }

    public void OnLevelResume()
    {
        Debug.Log("OnLevelResume");
        inGameMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnExitParty()
    {
        Debug.Log("OnExitParty");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        inGameMenuCanvas.SetActive(false);
        Destroy(gameObject);
        //SceneManager.UnloadSceneAsync("Scenes/Level1");
        UnloadLevel();
        Time.timeScale = 1f;
        MusicSystem ms = musicSys.GetComponent<MusicSystem>();
        ms.Stop();
    }

    public void EndLevelReached()  // Sacrifice has to be choose
    {
        Debug.Log("EndLevelReached");
        if(m_currentLevelIdx < 4) {
            endGameCanvas.SetActive(true);
            MusicSystem ms = musicSys.GetComponent<MusicSystem>();
            ms.PlayClip(0);
        }
        else
            EndLevel(4);
    }

    void UnloadLevel()
    {
        if (m_currentLevelIdx == 5)
            SceneManager.UnloadSceneAsync("End");
        else
            SceneManager.UnloadSceneAsync("Level" + m_currentLevelIdx.ToString());
    }

    public void EndLevel(int playerToDrop)  // Sacrifice has been choosen
    {
        Debug.Log("EndLevel");
        if (playerToDrop != 4)
            droppedPlayers.Add(playerToDrop);

        UnloadLevel();

        StartLevel(m_currentLevelIdx + 1);
    }

    public void HitBalloon(int value)
    {
        m_balloonLife -= value;
        if (balloonLife <= 0)
        {
            Time.timeScale = 0f;
            gameOverCanvas.SetActive(true);

            // switch to sacrifice theme
            MusicSystem ms = musicSys.GetComponent<MusicSystem>();
            ms.PlayClip(0);
        }
    }
}
