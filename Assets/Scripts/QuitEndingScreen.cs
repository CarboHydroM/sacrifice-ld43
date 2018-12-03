using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitEndingScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        GameObject partyObject = GameObject.FindGameObjectWithTag("Game");
        GameState game = partyObject.GetComponent<GameState>();
        game.OnExitParty();
    }

}
