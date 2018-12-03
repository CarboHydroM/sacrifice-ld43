using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyStat : MonoBehaviour {
    public GameObject inGameMenuCanvas;
    public GameObject endGameCanvas;

    public int[] score = { 0, 0, 0, 0 };
    public float nacelleWeight = 5f;
    public float altitude = 0f;
    public float nacellePower = 10f;
    public float nacelleSpeed = 0f;
    public GameState gameStat;

    List<float> dropedPlayers = new List<float>();

    // Use this for initialization
    void Start () {
        SceneManager.LoadSceneAsync("Scenes/Level1", LoadSceneMode.Additive);
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
        SceneManager.UnloadSceneAsync("Scenes/Level1");
        Time.timeScale = 1f;
    }

    public void EndLevelReached()  // Sacrifice has to be choose
    {
        Debug.Log("EndLevelReached");
        endGameCanvas.SetActive(true);
    }

    public void EndLevel(int playerToDrop)  // Sacrifice has been choosen
    {
        Debug.Log("EndLevel");
        if (playerToDrop != 4)
            dropedPlayers.Add(playerToDrop);

        gameStat.OnExitParty();
    }
}
