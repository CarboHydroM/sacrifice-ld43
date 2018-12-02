﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReceiver : MonoBehaviour {

    public int playerIndex = 666;
	private float m_firingClock;
	public float m_firingThroughput = 0.2f;
	public GameObject m_bulletPrefab;
    public GameObject m_nacelle;
    public float m_speed = 10f;
    public string m_moveXAxis;
    public string m_moveYAxis;
    public string m_fireXAxis;
    public string m_fireYAxis;

	// Use this for initialization
	void Start () {
		m_firingClock = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		float ix = Input.GetAxis(m_moveXAxis);
		float iy = Input.GetAxis(m_moveYAxis);
		float sx = Input.GetAxis(m_fireXAxis);
		float sy = Input.GetAxis(m_fireYAxis);

		Vector2 impulse = new Vector2(ix, iy);
		Vector2 firingDirection = new Vector2(sx, sy);

		float dt = Time.deltaTime;
		Vector2 move = impulse * dt * m_speed;
		m_firingClock += dt;

		Transform t = gameObject.GetComponent(typeof(Transform)) as Transform;
        //t.position += new Vector3(move.x, move.y);
        t.position += new Vector3(move.x, 0f);


        Vector3 nacellePos = m_nacelle.transform.position;
        CompositeCollider2D nacelleCollider = m_nacelle.GetComponent<CompositeCollider2D>();
        Bounds nacelleBounds = nacelleCollider.bounds;
        if(t.position.x < nacelleBounds.min.x + 1f)
            t.position = new Vector3(nacelleBounds.min.x + 1f, t.position.y);
        if (t.position.x > nacelleBounds.max.x - 1f)
            t.position = new Vector3(nacelleBounds.max.x - 1f, t.position.y);


        if (firingDirection.magnitude > 0f) {
			firingDirection.Normalize();
			if (m_firingClock >= m_firingThroughput) {
				GameObject bObject = Instantiate(m_bulletPrefab, t.position, Quaternion.identity);
				Projectile bullet = bObject.GetComponent(typeof(Projectile)) as Projectile;
                bullet.launcher = gameObject;

                bullet.Shoot(firingDirection);
				m_firingClock = 0f;
			}
		} else
			m_firingClock = 0f;
	}
}
