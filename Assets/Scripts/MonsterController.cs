using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    public Vector2 moveDir;
    public float speed = 1f;
    public BoxCollider2D unloadColider;

    float spawnTime;

    // Use this for initialization
    void Start () {
        spawnTime = Time.fixedTime;
    }

    // Update is called once per frame
    void Update () {
        Quaternion quat = new Quaternion();
        Vector3 moveDir3 = new Vector3(moveDir.x, moveDir.y, 0f);
        quat.SetLookRotation(new Vector3(0f, 0f, 1f), moveDir3);
        gameObject.transform.rotation = quat;

        gameObject.transform.position = gameObject.transform.position + moveDir3 * (speed / Time.deltaTime);

        if (Time.fixedTime - spawnTime > 1f)
        {
            if (unloadColider.IsTouching(gameObject.GetComponent<Collider2D>()) == false)
                Destroy(gameObject);
        }
    }

    public void MoveDir(Vector2 dir)
    {
        moveDir = dir;
    }

    public void FireDir(Vector2 dir)
    {

    }

    public void Fire()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Projectile proj = other.gameObject.GetComponent<Projectile>();
        if(proj)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
