using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float timeToSpawn = 1f;
    public GameObject monster;
    public BoxCollider2D unloadCollider;
    public float crateProbaRate = 0.5f;

    public Sprite[] spritesLevel1 = new Sprite[3];
    public Sprite[] spritesLevel2 = new Sprite[3];
    public Sprite[] spritesLevel3 = new Sprite[3];
    public Sprite[] spritesLevel4 = new Sprite[3];

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

            GameObject partyObject = GameObject.FindGameObjectWithTag("Party");
            PartyStat party = partyObject.GetComponent<PartyStat>();
            int monsterIdx = Random.Range(0, 3);
            Sprite[][] sprites = { spritesLevel1, spritesLevel2, spritesLevel3, spritesLevel4 };
            SpriteRenderer spr = newMonster.GetComponent<SpriteRenderer>();
            spr.sprite = sprites[1][monsterIdx];

            switch (party.m_currentLevelIdx - 1)
            {
                case 0:
                    spr.sprite = sprites[0][monsterIdx];
                    newMonster.GetComponent<MonsterController>().baseRotation = 180f;
                    newMonster.transform.localScale = new Vector3(2f, 2f, 2f);
                    break;
                case 1:
                    spr.sprite = sprites[1][monsterIdx];
                    newMonster.GetComponent<MonsterController>().baseRotation = 68f;
                    newMonster.transform.localScale = new Vector3(2f, 2f, 2f);
                    break;
                case 2:
                    spr.sprite = sprites[2][monsterIdx];
                    newMonster.transform.localScale = new Vector3(2f, 2f, 2f);
                    break;
                case 3:
                    spr.sprite = sprites[3][monsterIdx];
                    newMonster.GetComponent<MonsterController>().baseRotation = -90f;
                    newMonster.transform.localScale = new Vector3(2f, 2f, 2f);
                    break;
            }
        }
    }
}
