using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReceiver : MonoBehaviour {

	private float m_firingClock;
	public float m_firingThroughput = 0.2f;
	public GameObject m_bulletPrefab;
    public float m_speed = 10f;

	// Use this for initialization
	void Start () {
		m_firingClock = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		float ix = Input.GetAxis("Horizontal 1");
		float iy = Input.GetAxis("Vertical 1");
		float sx = Input.GetAxis("Horizontal 2");
		float sy = Input.GetAxis("Vertical 2");

		Vector2 impulse = new Vector2(ix, iy);
		Vector2 firingDirection = new Vector2(sx, sy);

		float dt = Time.deltaTime;
		Vector2 move = impulse * dt * m_speed;
		m_firingClock += dt;

		Transform t = gameObject.GetComponent(typeof(Transform)) as Transform;
		t.position += new Vector3(move.x, move.y);

		if (firingDirection.magnitude > 0f) {
			firingDirection.Normalize();
			if (m_firingClock >= m_firingThroughput) {
				GameObject bObject = Instantiate(m_bulletPrefab, t.position, Quaternion.identity);
				Projectile bullet = bObject.GetComponent(typeof(Projectile)) as Projectile;
				bullet.Shoot(firingDirection);
				m_firingClock = 0f;
			}
		} else
			m_firingClock = 0f;
	}
}
