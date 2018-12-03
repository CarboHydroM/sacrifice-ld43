using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelState : MonoBehaviour {
    public float distanceToReach = 20f;
    public string enemieScene;
    public string backgroundScene;

    public GameObject endGameCanvas;

    AsyncOperation loadPlayers;

    // Use this for initialization
    void Start () {
        SceneManager.LoadSceneAsync("Scenes/" + enemieScene, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Scenes/" + backgroundScene, LoadSceneMode.Additive);
        loadPlayers = SceneManager.LoadSceneAsync("Scenes/Players", LoadSceneMode.Additive);
        // endGameCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        //if (loadPlayers.isDone == false)
        //    return;
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Players"));

        GameObject partyObject = GameObject.FindGameObjectWithTag("Party");
        PartyStat party = partyObject.GetComponent<PartyStat>();
        if(party.altitude >= distanceToReach)
        {
            party.EndLevelReached();
            distanceToReach = float.MaxValue;
            // End level
            //endGameCanvas.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        SceneManager.UnloadSceneAsync("Scenes/" + enemieScene);
        SceneManager.UnloadSceneAsync("Scenes/" + backgroundScene);
        SceneManager.UnloadSceneAsync("Scenes/Players");
    }
}
