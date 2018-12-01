using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReceiver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float dh = Input.GetAxis("Horizontal");
		float dv = Input.GetAxis("Vertical");

		Vector2 impulse = new Vector2(dh, dv);
		Vector2 move = impulse * Time.deltaTime;

		Transform t = gameObject.GetComponent(typeof(Transform)) as Transform;
		t.position += new Vector3(move.x, move.y);
	}
}
