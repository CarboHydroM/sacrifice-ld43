using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIA : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float ix = 0.1f;
        float iy = 0f;
        float sx = 0f;
        float sy = 0f;

        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDist = float.MaxValue;
        foreach(GameObject m in monsters)
        {
            Vector3 a = m.GetComponent<Transform>().position;
            Vector3 b = gameObject.GetComponent<Transform>().position;
            float dist = Vector3.Distance(a, b);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = m;
            }
        }

        PlayerController player = gameObject.GetComponent<PlayerController>();
        if (nearest)
        {
            Vector3 a = gameObject.GetComponent<Transform>().position;
            Vector3 b = nearest.GetComponent<Transform>().position;
            Vector3 dir = b - a;
            dir.Normalize();

            player.SetInputs(0f, 0f, dir.x, dir.y);
        }
        else
            player.SetInputs(0f, 0f, 0f, 0f);
    }
}
