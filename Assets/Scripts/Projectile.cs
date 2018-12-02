using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private Vector2 m_direction;
	public float m_speed;
    public GameObject launcher;

	public void Shoot(Vector2 direction) {
		m_direction = direction;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 move = m_direction * m_speed * Time.deltaTime;

		Transform t = gameObject.GetComponent(typeof(Transform)) as Transform;
		t.position += new Vector3(move.x, move.y);

		// V0 checking here that we are out of range and ready to despawn
		Camera c = Camera.main;
		if (t.position.y >= c.pixelHeight)
			Destroy(gameObject);
	}
}
