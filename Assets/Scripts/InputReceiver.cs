using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReceiver : MonoBehaviour {

	private Vector2 m_firingDirection;
	private float m_firingClock;
	public float m_firingThroughput = 0.2f;
	public GameObject m_bulletPrefab;

	// Use this for initialization
	void Start () {
		m_firingDirection = new Vector2(0f, 1f);
		m_firingClock = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		float ix = Input.GetAxis("Horizontal 1");
		float iy = Input.GetAxis("Vertical 1");
		float sx = Input.GetAxis("Horizontal 2");
		float sy = Input.GetAxis("Vertical 2");
		bool fire = Input.GetKeyDown(KeyCode.A);

		Vector2 impulse = new Vector2(ix, iy);
		m_firingDirection = new Vector2(sx, sy);
		m_firingDirection.Normalize();

		float dt = Time.deltaTime;
		Vector2 move = impulse * dt;
		m_firingClock += dt;

		Transform t = gameObject.GetComponent(typeof(Transform)) as Transform;
		t.position += new Vector3(move.x, move.y);

		if (fire) {
			if (m_firingClock >= m_firingThroughput) {
				GameObject bObject = Instantiate(m_bulletPrefab, t.position, Quaternion.identity);
				Projectile bullet = bObject.GetComponent(typeof(Projectile)) as Projectile;
				bullet.Shoot(m_firingDirection);
				m_firingClock = 0f;
			}
		} else
			m_firingClock = 0f;
	}
}
