using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour {

    public Sprite[] lvlBackgrounds;
    public float startY;    // <---- hypergruik
    private float startAltitude;
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject query = GameObject.FindGameObjectWithTag("Party");
        PartyStat partyStat = query.GetComponent<PartyStat>();
        Transform t = gameObject.GetComponent<Transform>();
        float alt = partyStat.altitude - startAltitude;
        Vector3 pos = new Vector3(0f, startY - (alt * 0.25f), 30f);
        t.position = pos;
	}

    public void SetLevelBackground(int lvlIndex)
    {
        spriteRenderer.sprite = lvlBackgrounds[lvlIndex - 1];
        GameObject query = GameObject.FindGameObjectWithTag("Party");
        PartyStat partyStat = query.GetComponent<PartyStat>();
        startAltitude = partyStat.altitude;
    }
}
