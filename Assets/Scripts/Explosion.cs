using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float duration = 1000f;

    float startTime;

	// Use this for initialization
	void Start ()
    {
        startTime = Time.fixedTime;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.fixedTime - startTime > duration)
            Destroy(gameObject);
	}
}
