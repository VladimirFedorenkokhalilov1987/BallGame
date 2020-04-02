using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public Text levelText;
    public GameObject[] Targets;
    public GameObject Ball;
    public GameObject Player;
    public GameObject AI;
    public int count = 0;
    public int level=1;

    // Start is called before the first frame update
    void Awake()
    {
        if (levelText == null)
            levelText = GameObject.Find("LevelText").GetComponent<Text>();
        if (Ball == null)
            Ball = GameObject.FindObjectOfType<BallLogic>().gameObject;
        if (Player == null)
            Player = GameObject.FindObjectOfType<PlayerLogic>().gameObject;
        if (AI == null)
            AI = GameObject.FindObjectOfType<AiLogic>().gameObject;
    }

    void Update()
    {
        if (Ball.activeInHierarchy == false)
        {
            Ball.SetActive(true);
            Ball.transform.position = this.transform.position;
            Player.GetComponent<PlayerLogic>().CanShot = true;
        }
        
        for (int i = 0; i < Targets.Length; i++)
        {
            if (Targets[i].gameObject.activeSelf == false)
            {
                //next lvl
                if(count>=Targets.Length)
                NextLevel();
            }
        }
    }

    private void NextLevel()
    {
        count = 0;
        for (int i = 0; i < Targets.Length; i++)
        {
            Targets[i].SetActive(true);
        }

        level += 1;
        levelText.text = "Level: " + level;
        AI.transform.position = new Vector3(0, 1,4);
        AI.GetComponent<AiLogic>().IncreaseSpeed();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
