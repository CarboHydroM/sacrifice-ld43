using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIA : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    GameObject nearestCrateForPlayer(GameObject player)
    {
        GameObject nearestCrate = null;
        GameObject[] crates = GameObject.FindGameObjectsWithTag("AmmoCrate");
        float minDist = float.MaxValue;
        foreach (GameObject m in crates)
        {
            Vector3 a = m.GetComponent<Transform>().position;
            Vector3 b = player.GetComponent<Transform>().position;
            float dist = Vector3.Distance(a, b);
            if (dist < minDist)
            {
                minDist = dist;
                nearestCrate = m;
            }
        }
        return nearestCrate;
    }

    GameObject nearestPlayerForCrate(GameObject crate)
    {
        GameObject nearestPlayer = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDist = float.MaxValue;
        foreach (GameObject p in players)
        {
            if (p.activeSelf)
            {
                Vector3 a = p.GetComponent<Transform>().position;
                Vector3 b = crate.GetComponent<Transform>().position;
                float dist = Vector3.Distance(a, b);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearestPlayer = p;
                }
            }
        }
        return nearestPlayer;
    }

    // Update is called once per frame
    void Update () {
        float ix = 0.1f;
        float iy = 0f;
        float sx = 0f;
        float sy = 0f;

        GameObject nearest = null;
        {
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
            float minDist = float.MaxValue;
            foreach (GameObject m in monsters)
            {
                Vector3 a = m.GetComponent<Transform>().position;
                Vector3 b = gameObject.GetComponent<Transform>().position;
                float dist = Vector3.Distance(a, b);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = m;
                }
            }
        }

        GameObject nearestCrate = null;
        {
            GameObject[] crates = GameObject.FindGameObjectsWithTag("AmmoCrate");
            float minDist = float.MaxValue;
            foreach (GameObject crate in crates)
            {
                GameObject nearestPlayer = nearestPlayerForCrate(crate);
                if (nearestPlayer == gameObject)
                {
                    Vector3 a = crate.GetComponent<Transform>().position;
                    Vector3 b = gameObject.GetComponent<Transform>().position;
                    float dist = Vector3.Distance(a, b);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        nearestCrate = crate;
                    }
                }
            }
        }

        {
            PlayerController player = gameObject.GetComponent<PlayerController>();
            Vector3 fireDir = new Vector3();
            if (nearest)
            {
                Vector3 a = gameObject.GetComponent<Transform>().position;
                Vector3 b = nearest.GetComponent<Transform>().position;
                fireDir = b - a;
                fireDir.x += Random.Range(-0.1f, 0.1f);
                fireDir.y += Random.Range(-0.1f, 0.1f);
                fireDir.Normalize();
            }

            Vector3 moveDir = new Vector3();
            if (nearestCrate)
            {
                Vector3 a = gameObject.GetComponent<Transform>().position;
                Vector3 b = nearestCrate.GetComponent<Transform>().position;
                moveDir = b - a;
                moveDir.Normalize();
            }

            player.SetInputs(moveDir.x, moveDir.y, fireDir.x, fireDir.y);
        }
    }
}
