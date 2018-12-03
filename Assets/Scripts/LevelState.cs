using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelState : MonoBehaviour {
    public float distanceToReach = 20f;
    public string enemieScene;
    public string backgroundScene;
    public GameObject monsterPlayerPrefab;

    public GameObject[] monsterPlayers = new GameObject[4];

    GameObject[] m_players = new GameObject[4];

    public GameObject endGameCanvas;

    void Start () {
        SceneManager.LoadSceneAsync("Scenes/" + enemieScene, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Scenes/" + backgroundScene, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Scenes/Players", LoadSceneMode.Additive);
    }

    void Update () {
        GameObject partyObject = GameObject.FindGameObjectWithTag("Party");
        PartyStat party = partyObject.GetComponent<PartyStat>();
        if(party.altitude >= distanceToReach)
        {
            party.EndLevelReached();
            distanceToReach = float.MaxValue;
        }

        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in players)
                m_players[p.GetComponent<InputReceiver>().playerIndex] = p;
        }
        foreach (GameObject p in m_players)
        {
            Debug.Assert(party);
            //Debug.Assert(p);
            if (p)
            {
                Debug.Assert(p.GetComponent<InputReceiver>());
                if (party.droppedPlayers.Contains(p.GetComponent<InputReceiver>().playerIndex))
                    p.SetActive(false);
            }
        }

        foreach (int playerIndex in party.droppedPlayers)
        {
            if(monsterPlayers[playerIndex] == null)
            {
                float spawnProp = Random.Range(0f, 1);
                if (spawnProp < Time.deltaTime)
                {
                    BoxCollider2D spawnCollider = gameObject.GetComponent<BoxCollider2D>();
                    float x = Random.Range(-spawnCollider.size.x / 2f, spawnCollider.size.x / 2f);
                    float y = Random.Range(-spawnCollider.size.y / 2f, spawnCollider.size.y / 2f);
                    Vector3 pos = gameObject.transform.position + new Vector3(x, y, 0);
                    GameObject newMonster = Instantiate(monsterPlayerPrefab, pos, Quaternion.identity);
                    //MonsterController ai = newMonster.GetComponent<MonsterController>();

                    // Debug.Log("Spawn ghost " + playerIndex.ToString());

                    GameObject playerObj = m_players[playerIndex];
                    InputReceiver player = playerObj.GetComponent<InputReceiver>();
                    MonsterInput mi = newMonster.GetComponent<MonsterInput>();
                    mi.m_moveXAxis = player.m_moveXAxis;
                    mi.m_moveYAxis = player.m_moveYAxis;
                    mi.m_fireXAxis = player.m_fireXAxis;
                    mi.m_fireYAxis = player.m_fireYAxis;
                }
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.UnloadSceneAsync("Scenes/" + enemieScene);
        SceneManager.UnloadSceneAsync("Scenes/" + backgroundScene);
        SceneManager.UnloadSceneAsync("Scenes/Players");
    }
}
