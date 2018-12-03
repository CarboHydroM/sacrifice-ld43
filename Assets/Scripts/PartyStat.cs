using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyStat : MonoBehaviour {
    public GameObject inGameMenuCanvas;

    public int[] score = { 0, 0, 0, 0 };
    public float nacelleWeight = 5f;
    public float altitude = 0f;
    public float nacellePower = 10f;
    public float nacelleSpeed = 0f;
    public GameState gameStat;

    List<float> dropedPlayers = new List<float>();

    // Use this for initialization
    void Start () {
        SceneManager.LoadScene("Scenes/Level1", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update () {
        nacelleSpeed = ((nacellePower - nacelleWeight) / 10f) * Time.deltaTime;
        altitude += nacelleSpeed;

        if (Input.GetButton("Menu"))
        {
            Time.timeScale = 0f;
            inGameMenuCanvas.SetActive(true);
        }
    }

    public void OnLevelResume()
    {
        inGameMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnExitParty()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        inGameMenuCanvas.SetActive(false);
        Destroy(gameObject);
        SceneManager.UnloadSceneAsync("Scenes/Level1");
        Time.timeScale = 1f;
    }

    public void EndLevel(int playerToDrop)
    {
        if(playerToDrop != 4)
            dropedPlayers.Add(playerToDrop);

        gameStat.OnExitParty();
    }
}
