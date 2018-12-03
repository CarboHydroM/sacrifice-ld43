using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrateController : MonoBehaviour
{
    private float m_clock;
    private float m_angle = 0f;
    private Transform m_transform;
    public float m_fallSpeed = 5f;
    public int ammoBonus = 10;

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
    void Start()
    {
        m_clock = 0f;
        m_transform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        m_clock += dt;
        Vector2 move = new Vector2(0f, m_fallSpeed * dt);

        m_angle = (Mathf.Sin(m_clock * 1.5f) * 0.5f) + (Mathf.PI * 0.5f);
        float x = Mathf.Cos(m_angle);
        float y = Mathf.Sin(m_angle);
        Quaternion quat = new Quaternion();
        Vector3 moveDir = new Vector3(x, y, 0f);
        quat.SetLookRotation(new Vector3(0f, 0f, 1f), moveDir);
        m_transform.rotation = quat;

        m_transform.position -= new Vector3(move.x, move.y);

        if (GoneBeyond(90f, 50f))
            Destroy(gameObject);
    }
}
