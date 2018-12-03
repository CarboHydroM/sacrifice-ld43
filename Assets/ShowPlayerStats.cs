using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerStats : MonoBehaviour {

    public GameObject redStats;
    public GameObject greenStats;
    public GameObject blueStats;
    public GameObject yellowStats;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable() {
        GameObject[] query = GameObject.FindGameObjectsWithTag("Party");
        PartyStat partyStat = query[0].GetComponent<PartyStat>();
        float wasteRatio = 0f;
        float consumed = partyStat.ammoConsumption[0];
        if (consumed > 0f)
            wasteRatio = partyStat.wastedAmmo[0] * 100f / consumed;
        else
            wasteRatio = 0f;
        string statsText = "Kills: " + partyStat.ennemyKills[0].ToString() +
                            " / Wasted ammo: " + wasteRatio + "%";
        redStats.GetComponent<Text>().text = statsText;
        consumed = partyStat.ammoConsumption[1];
        if (consumed > 0f)
            wasteRatio = partyStat.wastedAmmo[1] * 100f / consumed;
        else
            wasteRatio = 0f;
        statsText = "Kills: " + partyStat.ennemyKills[1].ToString() +
                            " / Wasted ammo: " + wasteRatio + "%";
        blueStats.GetComponent<Text>().text = statsText;
        consumed = partyStat.ammoConsumption[2];
        if (consumed > 0f)
            wasteRatio = partyStat.wastedAmmo[2] * 100f / consumed;
        else
            wasteRatio = 0f;
        statsText = "Kills: " + partyStat.ennemyKills[2].ToString() +
                            " / Wasted ammo: " + wasteRatio + "%";
        yellowStats.GetComponent<Text>().text = statsText;
        consumed = partyStat.ammoConsumption[3];
        if (consumed > 0f)
            wasteRatio = partyStat.wastedAmmo[3] * 100f / consumed;
        else
            wasteRatio = 0f;
        statsText = "Kills: " + partyStat.ennemyKills[3].ToString() +
                            " / Wasted ammo: " + wasteRatio + "%";
        greenStats.GetComponent<Text>().text = statsText;
    }
}
