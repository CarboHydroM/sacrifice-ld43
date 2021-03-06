﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelState : MonoBehaviour {
    private AsyncOperation bgScnLoading;
    private bool bgIsSet;   // <----- hypergruik
    public float distanceToReach = 20f;
    public string enemieScene;
    public string backgroundScene;
    public GameObject monsterPlayerPrefab;

    public GameObject[] monsterPlayers = new GameObject[4];

    GameObject[] m_players = new GameObject[4];

    public GameObject endGameCanvas;

    AsyncOperation loadPlayers;
    void Start () {
        SceneManager.LoadSceneAsync("Scenes/" + enemieScene, LoadSceneMode.Additive);
        bgScnLoading = SceneManager.LoadSceneAsync("Scenes/" + backgroundScene, LoadSceneMode.Additive);
        loadPlayers = SceneManager.LoadSceneAsync("Scenes/Players", LoadSceneMode.Additive);
        bgIsSet = false;
    }

    bool firstUpdate = true;
    Scene enemyScene;
    Scene bgScene;
    Scene playerScene;
    void Update () {
        if (bgScnLoading.isDone && !bgIsSet && loadPlayers.isDone && loadPlayers.isDone)
        {
            enemyScene = SceneManager.GetSceneByName(enemieScene);
            bgScene = SceneManager.GetSceneByName(backgroundScene);
            playerScene = SceneManager.GetSceneByName("Players");
            Debug.Assert(enemyScene.IsValid());
            Debug.Assert(bgScene.IsValid());
            Debug.Assert(playerScene.IsValid());

            GameObject gobj = GameObject.FindGameObjectWithTag("BG");
            Debug.Assert(gobj);
            BackgroundScrolling bgs = gobj.GetComponent<BackgroundScrolling>();
            Debug.Assert(bgs);
            gobj = GameObject.FindGameObjectWithTag("Party");
            PartyStat partyStat = gobj.GetComponent<PartyStat>();
            Debug.Assert(partyStat);
            bgs.SetLevelBackground(partyStat.m_currentLevelIdx);
            bgIsSet = true;
        }

        if (firstUpdate)
        {
            if (loadPlayers.isDone == false)
                return;
            firstUpdate = false;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in players)
                m_players[p.GetComponent<PlayerController>().playerIndex] = p;

            GameObject gameObject = GameObject.FindGameObjectWithTag("Game");
            GameState game = gameObject.GetComponent<GameState>();
            for (int i = 0; i < 4; ++i)
            {
                if (game.playerAreIA[i])
                    m_players[i].GetComponent<PlayerInput>().enabled = false;
                else
                    m_players[i].GetComponent<PlayerIA>().enabled = false;
            }
        }
        GameObject partyObject = GameObject.FindGameObjectWithTag("Party");
        PartyStat party = partyObject.GetComponent<PartyStat>();
        if(party.altitude >= distanceToReach)
        {
            party.EndLevelReached();
            distanceToReach = float.MaxValue;
        }

        foreach (GameObject p in m_players)
        {
            Debug.Assert(party);
            //Debug.Assert(p);
            if (p)
            {
                Debug.Assert(p.GetComponent<PlayerController>());
                if (party.droppedPlayers.Contains(p.GetComponent<PlayerController>().playerIndex))
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
                    newMonster.GetComponent<MonsterInput>().playerIndex = playerIndex;
                    //MonsterController ai = newMonster.GetComponent<MonsterController>();

                    // Debug.Log("Spawn ghost " + playerIndex.ToString());

                    GameObject playerObj = m_players[playerIndex];
                    PlayerInput player = playerObj.GetComponent<PlayerInput>();
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
        SceneManager.UnloadSceneAsync(playerScene);
        SceneManager.UnloadSceneAsync(enemyScene);
        SceneManager.UnloadSceneAsync(bgScene);
    }
}
