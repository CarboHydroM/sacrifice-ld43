using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    public Vector2 moveDir;
    public float speed = 1f;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update () {
        Quaternion quat = new Quaternion();
        Vector3 moveDir3 = new Vector3(moveDir.x, moveDir.y, 0f);
        quat.SetLookRotation(new Vector3(0f, 0f, 1f), moveDir3);
        gameObject.transform.rotation = quat;
        Debug.Log("dir " + moveDir.x.ToString() + " " + moveDir.y.ToString());

        gameObject.transform.position = gameObject.transform.position + moveDir3 * speed;
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
}
