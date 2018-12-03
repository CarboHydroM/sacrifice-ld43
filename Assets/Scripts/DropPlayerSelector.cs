using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DropPlayerSelector : MonoBehaviour {
    public GameObject[] playersText = new GameObject[5];
    public GameObject countDownText;
    public float countDownDuration = 5f;

    float countDownStart;

    // Use this for initialization
    void Start () {
    }

    void OnDestroy()
    {
    }

    private void OnEnable()
    {
        countDownStart = Time.realtimeSinceStartup;
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update () {
        float[] xMove = { 0f, 0f, 01f, 0f };
        float[] yMove = { 0f, 0f, 01f, 0f };
        xMove[0] = Input.GetAxis("Move X 1");
        yMove[0] = Input.GetAxis("Move Y 1");
        xMove[1] = Input.GetAxis("Move X 2");
        yMove[1] = Input.GetAxis("Move Y 2");
        xMove[2] = Input.GetAxis("Move X 3");
        yMove[2] = Input.GetAxis("Move Y 3");
        xMove[3] = Input.GetAxis("Move X 4");
        yMove[3] = Input.GetAxis("Move Y 4");


        float xMean = xMove.Average();
        float yMean = yMove.Average();
        Vector2 dir = new Vector2(xMean, yMean);

        //Debug.Log("Choosen dp1 : " + xMove[0].ToString() + " " + yMove[0].ToString());
        foreach (var text in playersText)
            text.GetComponent<Text>().fontSize = 30;
        playersText[4].GetComponent<Text>().fontSize = 20;

        GameObject partyObject = GameObject.FindGameObjectWithTag("Party");
        PartyStat party = partyObject.GetComponent<PartyStat>();

        foreach (var text in playersText.Select((value, i) => new { i, value }))
        {
            if (party.droppedPlayers.Contains(text.i))
                text.value.SetActive(false);
        }

        int minScore = int.MaxValue;
        int playerIndex = 0;
        foreach (var score in party.score.Select((value, i) => new { i, value }))
        {
            if(score.value < minScore && !party.droppedPlayers.Contains(score.i))
            {
                minScore = score.value;
                playerIndex = score.i;
            }
        }

        if (dir.SqrMagnitude() > 0.04f)
        {
            dir.Normalize();

            Vector2[] dirs =
            {
                new Vector2(0f, 1f),
                new Vector2(-1f, 0f),
                new Vector2(0f, -1f),
                new Vector2(1f, 0f),
                new Vector2(0.6f, -0.6f),
            };

            //dirs[4].Normalize();

            float max = float.MinValue;
            foreach (var buttonDir in dirs.Select((value, i) => new { i, value }))
            {
                float dot = Vector2.Dot(buttonDir.value, dir);
                if (dot > max)
                {
                    max = dot;
                    playerIndex = buttonDir.i;
                }
            }
            //var dots =
            //    from buttonDir in dirs
            //    select Vector2.Dot(buttonDir, dir);

            //Debug.Log("Choosen index : " + index.ToString());
        }

        if (playerIndex == 4)
            playersText[playerIndex].GetComponent<Text>().fontSize = 35;
        else
            playersText[playerIndex].GetComponent<Text>().fontSize = 50;

        //Debug.Log("Time.fixedTime : " + Time.fixedTime.ToString());
        float timeElapsed = Time.realtimeSinceStartup - countDownStart;
        // Debug.Log("timeElapsed : " + timeElapsed.ToString());
        float timeLeft = countDownDuration - timeElapsed;
        // Debug.Log("timeLeft : " + timeElapsed.ToString());
        countDownText.GetComponent<Text>().text = timeLeft.ToString();

        if (timeLeft < 0f)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
            party.EndLevel(playerIndex);
        }
    }
}
