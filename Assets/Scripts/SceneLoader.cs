using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("Scenes/Enemies", LoadSceneMode.Additive);
        SceneManager.LoadScene("Scenes/background", LoadSceneMode.Additive);
        SceneManager.LoadScene("Scenes/Players", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
