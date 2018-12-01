using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsSpawner : MonoBehaviour {
    /// The cloud prefab to be spawned.
    public GameObject cloud;
    /// How long between each spawn.
    public float spawnTime = 3f;
    /// An array of the spawn points this enemy can spawn from.
	public Transform[] spawnPoints;

	/// Add a radom offset [-spawnXOffset, +spawnXOffset] while spawned
	public float spawnXOffset = 1.0f;

	// Use this for initialization
	void Start () {
	    InvokeRepeating("Spawn", spawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Spawn() {
	    // Find a random index between zero and one less than the number of spawn points.
	    int spawnPointIndex = Random.Range(0, spawnPoints.Length);

	    // Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
	    // Quaternion rotation = Quaternion.identity;
	    //Instantiate(cloud, position, rotation);

	    // Create an instance of the enemy prefab at the randomly selected
	    //spawn point's position and rotation.
	    Vector3 position = spawnPoints[spawnPointIndex].position;
	    // Do not spawn them directly on their spawner point but with a tiny offset
	    position.x += Random.Range(-spawnXOffset, spawnXOffset);
	    Instantiate(cloud, position, spawnPoints[spawnPointIndex].rotation);
	}
	    
}
