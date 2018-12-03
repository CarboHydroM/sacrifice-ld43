using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public string m_moveXAxis;
    public string m_moveYAxis;
    public string m_fireXAxis;
    public string m_fireYAxis;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		float ix = Input.GetAxis(m_moveXAxis);
		float iy = Input.GetAxis(m_moveYAxis);
		float sx = Input.GetAxis(m_fireXAxis);
		float sy = Input.GetAxis(m_fireYAxis);

        PlayerController player = gameObject.GetComponent<PlayerController>();
        player.SetInputs(ix, iy, sx, sy);
    }
}
