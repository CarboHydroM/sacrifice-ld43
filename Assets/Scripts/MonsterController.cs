using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    public Vector2 moveDir;
    public float speed = 10f;
    public int scoreBounty = 100;
    public GameObject explosionPrefab;
    public GameObject ammoCratePrefab;
    public float crateProbaRate = 0.2f;
    private AudioSource audioSource;

    float spawnTime;

    // Use this for initialization
    void Start () {
        spawnTime = Time.fixedTime;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        if (Time.deltaTime < 0.00000001f)
            return;               
        Quaternion quat = new Quaternion();
        Vector3 moveDir3 = new Vector3(moveDir.x, moveDir.y, 0f);
        quat.SetLookRotation(new Vector3(0f, 0f, 1f), moveDir3);
        gameObject.transform.rotation = quat;

        gameObject.transform.position = gameObject.transform.position + moveDir3 * (speed * Time.deltaTime);

        if (Time.fixedTime - spawnTime > 1f)
        {
            //BoxCollider2D unloadCollider = 
            //    GameObject.FindGameObjectWithTag("DontUnloadArea").GetComponent<BoxCollider2D>();

            Vector3 p = gameObject.GetComponent<Transform>().position;
            //if (unloadCollider.IsTouching(gameObject.GetComponent<Collider2D>()) == false)
            if(p.x < -40f || p.x > 40f || p.y < -10f || p.y > 40f)
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
        GameObject partyObj = GameObject.FindGameObjectWithTag("Party");
        PartyStat party = partyObj.GetComponent<PartyStat>();

        Projectile proj = other.gameObject.GetComponent<Projectile>();
        if (proj)
        {
            audioSource.Play();
            GameObject launcher = proj.launcher;
            PlayerController inputRceiver = launcher.GetComponent<PlayerController>();
            if(inputRceiver)  // Fired by a player
            {
                int playerIndex = inputRceiver.playerIndex;
                party.score[playerIndex] += scoreBounty;
                party.ennemyKills[playerIndex]++;
            }
            Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
            if (Random.Range(0f, 1f) <= crateProbaRate)
            {
                Instantiate(ammoCratePrefab, gameObject.transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Nacelle")
        {
            Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
            party.HitBalloon(1);
            Destroy(gameObject);
        }
    }
}
