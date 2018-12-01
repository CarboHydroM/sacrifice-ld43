using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour {

    float dirShift;

	// Use this for initialization
	void Start () {
        dirShift = Random.Range(0f, 100f);
    }
	
	// Update is called once per frame
	void Update () {

        //Debug.Log("Hello", gameObject);

        MonsterController controller = gameObject.GetComponent<MonsterController>();
        //Vector2 dir = new Vector2(Mathf.Cos(Time.fixedTime), Mathf.Sin(Time.fixedTime));
        Vector2 dir = new Vector2(Mathf.Cos(Time.fixedTime + dirShift), -1f);
        dir.Normalize();
        controller.MoveDir(dir);
    }
}
