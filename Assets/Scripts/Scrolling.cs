using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour {
    /// Scrolling speed. This must include the balloon speed.
    public float scrollingSpeed = 30.1f;
    /// Add a randomness [0, speedFactor] to the scrolling speed. It also
    /// affects the Z value (simulated by scalling).
    public float speedFactor = 2.0f;

    /// Scale up the entity by this factor depending on its speed
    public float scaleFactor = 30.0f;

    // Use this for initialization
    void Start () {
	    float factor = Random.Range(0f, speedFactor);
	    scrollingSpeed += factor;
	    Vector3 pos = transform.position;
	    pos.z += factor * 10.0f;
	    transform.position = pos;

	    float scale = 1.0f + factor * scaleFactor;
	    transform.localScale = new Vector3(scale, scale, scale);
    }
	
    // Update is called once per frame
    void Update () {
        GameObject[] query = GameObject.FindGameObjectsWithTag("Party");
        PartyStat partyStat = query[0].GetComponent<PartyStat>();
        float effectiveSpeed = scrollingSpeed +
                                (partyStat.nacelleSpeed * speedFactor);
	    transform.position -= Vector3.up * effectiveSpeed * Time.deltaTime;

	    // TODO Destroy the entity when it leave the screen
	    // if (transform.position.y > 0) {
	    //     Destroy(this);
	    // }
    }
}
