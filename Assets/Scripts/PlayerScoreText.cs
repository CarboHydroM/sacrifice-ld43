using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreText : MonoBehaviour {

    public int playerIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Text text = gameObject.GetComponent<Text>();
        GameObject partyObject = GameObject.FindGameObjectWithTag("Party");
        PartyStat party = partyObject.GetComponent<PartyStat>();
        int score = party.score[playerIndex];
        text.text = score.ToString();
    }
}
