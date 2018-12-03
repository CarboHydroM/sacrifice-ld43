using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public float distanceToReach = 20f;
    public GameObject endGameCanvas;

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("Scenes/Enemies", LoadSceneMode.Additive);
        SceneManager.LoadScene("Scenes/background", LoadSceneMode.Additive);
        SceneManager.LoadScene("Scenes/Players", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level1"));
        endGameCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        GameObject partyObject = GameObject.FindGameObjectWithTag("Party");
        PartyStat party = partyObject.GetComponent<PartyStat>();
        if(party.altitude >= distanceToReach)
        {
            // End level
            endGameCanvas.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        SceneManager.UnloadSceneAsync("Scenes/Enemies");
        SceneManager.UnloadSceneAsync("Scenes/background");
        SceneManager.UnloadSceneAsync("Scenes/Players");
    }
}
