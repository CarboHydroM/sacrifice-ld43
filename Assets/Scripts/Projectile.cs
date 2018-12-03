using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Transform m_transform;
	private Vector2 m_direction;
	public float m_speed;
    public GameObject launcher;

	public void Shoot(Vector2 direction) {
		m_direction = direction;
        m_transform = gameObject.GetComponent(typeof(Transform)) as Transform;
    }

    public bool GoneBeyond(float xmax, float ymax)
    {
        float x = m_transform.position.x;
        float y = m_transform.position.y;

        float d2 = (x * x) + (y * y);
        float maxd2 = (xmax * xmax) + (ymax * ymax);
        if (d2 > maxd2)
            return true;

        return false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 move = m_direction * m_speed * Time.deltaTime;
		m_transform.position += new Vector3(move.x, move.y);

		if (GoneBeyond(90f, 50f))
        {
            GameObject[] query = GameObject.FindGameObjectsWithTag("Party");
            PartyStat partyStat = query[0].GetComponent<PartyStat>();
            InputReceiver player = launcher.GetComponent<InputReceiver>();
            partyStat.wastedAmmo[player.playerIndex]++;
            Destroy(gameObject);
        }
	}
}
