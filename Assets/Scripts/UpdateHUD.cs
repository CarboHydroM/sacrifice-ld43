using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHUD : MonoBehaviour {

    public GameObject ammoStock;
    public GameObject player1Score;
    public GameObject player2Score;
    public GameObject player3Score;
    public GameObject player4Score;
    public GameObject altitude;
    public GameObject speed;
    public GameObject balloonLife;

    PartyStat party;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        GameObject partyObject = GameObject.FindGameObjectWithTag("Party");
        if (partyObject)
        {
            party = partyObject.GetComponent<PartyStat>();
            ammoStock.GetComponent<Text>().text = party.ammoStock.ToString();
            player1Score.GetComponent<Text>().text = party.score[0].ToString();
            player2Score.GetComponent<Text>().text = party.score[1].ToString();
            player3Score.GetComponent<Text>().text = party.score[2].ToString();
            player4Score.GetComponent<Text>().text = party.score[3].ToString();
            altitude.GetComponent<Text>().text = party.altitude.ToString() + "m";
            speed.GetComponent<Text>().text = party.nacelleSpeed.ToString() + "m/s";
            balloonLife.GetComponent<Text>().text = party.balloonLife.ToString();
        }
    }
}
