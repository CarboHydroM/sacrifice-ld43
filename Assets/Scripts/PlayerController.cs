using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int playerIndex = 666;
    private float m_firingClock;
    private int m_bulletCount;
    private float m_bulletClock;
    public float m_firingCycle = 0.1f;
    public int m_bulletsPerShot = 1;
    public float m_bulletDelay = 0.05f;
    public GameObject m_bulletPrefab;
    public GameObject m_nacelle;
    private PartyStat m_partyStat;
    public float m_speed = 10f;

    // Use this for initialization
    void Start()
    {
        m_firingClock = 0f;
        m_bulletClock = 0f;
        m_bulletCount = 0;

        GameObject[] query = GameObject.FindGameObjectsWithTag("Party");
        m_partyStat = query[0].GetComponent<PartyStat>();
    }

    // Update is called once per frame
    public void SetInputs(float ix, float iy, float sx, float sy)
    {
        Vector2 impulse = new Vector2(ix, iy);
        Vector2 firingDirection = new Vector2(sx, sy);

        float dt = Time.deltaTime;
        Vector2 move = impulse * dt * m_speed;
        Transform t = gameObject.GetComponent(typeof(Transform)) as Transform;
        //t.position += new Vector3(move.x, move.y);
        t.position += new Vector3(move.x, 0f);

        Vector3 nacellePos = m_nacelle.transform.position;
        CompositeCollider2D nacelleCollider = m_nacelle.GetComponent<CompositeCollider2D>();
        Bounds nacelleBounds = nacelleCollider.bounds;
        if (t.position.x < nacelleBounds.min.x + 1f)
            t.position = new Vector3(nacelleBounds.min.x + 1f, t.position.y);
        if (t.position.x > nacelleBounds.max.x - 1f)
            t.position = new Vector3(nacelleBounds.max.x - 1f, t.position.y);

        if ((firingDirection.magnitude > 0f) && (m_partyStat.ammoStock > 0))
        {
            firingDirection.Normalize();

            if (m_firingClock > m_firingCycle)
            {
                m_firingClock = 0f;
                m_bulletCount = 0;
            }
            else
            {
                if (m_bulletCount < m_bulletsPerShot)
                {
                    if (m_bulletClock > m_bulletDelay)
                    {
                        m_bulletClock = 0f;
                    }
                    else
                    {
                        if (m_bulletClock == 0f)
                        {
                            GameObject bObject = Instantiate(m_bulletPrefab, t.position, Quaternion.identity);
                            Projectile bullet = bObject.GetComponent(typeof(Projectile)) as Projectile;
                            bullet.Shoot(firingDirection);
                            bullet.launcher = gameObject;
                            m_bulletCount++;
                            m_partyStat.ammoConsumption[playerIndex]++;
                            m_partyStat.ammoStock--;
                        }
                        m_bulletClock += dt;
                    }
                }
                m_firingClock += dt;
            }
        }
        else
        {
            m_firingClock = 0f;
            m_bulletCount = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        AmmoCrateController crate = other.gameObject.GetComponent<AmmoCrateController>();
        if (crate)
        {
            m_partyStat.ammoStock += crate.ammoBonus;
            m_partyStat.score[playerIndex] += crate.scoreBonus;
            Destroy(crate.gameObject);
        }
    }
}
