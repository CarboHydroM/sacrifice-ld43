using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float timeToSpawn = 1f;
    public GameObject monster;
    public BoxCollider2D unloadCollider;
    public float crateProbaRate = 0.5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float spawnPerSec = 1f / timeToSpawn;
        float probaToSpawn = spawnPerSec * Time.deltaTime;

        float proba = Random.Range(0f, 1f);
        if(proba < probaToSpawn)
        {
            BoxCollider2D spawnCollider = gameObject.GetComponent<BoxCollider2D>();
            float x = Random.Range(-spawnCollider.size.x / 2f, spawnCollider.size.x / 2f);
            float y = Random.Range(-spawnCollider.size.y / 2f, spawnCollider.size.y / 2f);
            Vector3 pos = gameObject.transform.position + new Vector3(x, y, 0);
            GameObject newMonster = Instantiate(monster, pos, Quaternion.identity);
            MonsterController controler = newMonster.GetComponent<MonsterController>();
            controler.crateProbaRate = crateProbaRate;
        }
    }
}
