﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInput : MonoBehaviour {

    public int playerIndex = 666;
    //private float m_firingClock;
    //private int m_bulletCount;
    //private float m_bulletClock;
    public float m_firingCycle = 0.1f;
    public int m_bulletsPerShot = 1;
    public float m_bulletDelay = 0.05f;
    public GameObject m_bulletPrefab;
    public GameObject m_nacelle;
    public float m_speed = 10f;
    public string m_moveXAxis;
    public string m_moveYAxis;
    public string m_fireXAxis;
    public string m_fireYAxis;

    // Use this for initialization
    void Start()
    {
        //m_firingClock = 0f;
        //m_bulletClock = 0f;
        //m_bulletCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float ix = Input.GetAxis(m_moveXAxis);
        float iy = Input.GetAxis(m_moveYAxis);
        float sx = Input.GetAxis(m_fireXAxis);
        float sy = Input.GetAxis(m_fireYAxis);

        Vector2 impulse = new Vector2(ix, iy);
        Vector2 firingDirection = new Vector2(sx, sy);

        MonsterController monster =  gameObject.GetComponent<MonsterController>();
        monster.MoveDir(impulse);
        monster.FireDir(firingDirection);
    }
}
