using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float timeToSpawn = 1f;
    public GameObject monster;
    public BoxCollider2D unloadCollider;

    float lastSpawn = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Time.fixedTime - lastSpawn > timeToSpawn)
        {
            lastSpawn = Time.fixedTime;
            BoxCollider2D spawnCollider = gameObject.GetComponent<BoxCollider2D>();
            float x = Random.Range(-spawnCollider.size.x / 2f, spawnCollider.size.x / 2f);
            float y = Random.Range(-spawnCollider.size.y / 2f, spawnCollider.size.y / 2f);
            Vector3 pos = gameObject.transform.position + new Vector3(x, y, 0);
            GameObject newMonster = Instantiate(monster, pos, Quaternion.identity);
        }
    }
}
