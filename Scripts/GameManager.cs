using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int rabbitScore; // 토끼 잡아서 버는 점수
    public int clearMission = 1; //미션 클리어 단계

    public TextMeshProUGUI pointText;
    

    public GameObject[] enemySpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //미션 진행도에 따라 에너미 스폰
        if (!enemySpawnPoint[0].activeSelf)
        {
            enemySpawnPoint[0].SetActive(true);
        }
        if (!enemySpawnPoint[1].activeSelf && clearMission > 1)
            enemySpawnPoint[1].SetActive(true);
        if (!enemySpawnPoint[2].activeSelf && clearMission < 3)
            enemySpawnPoint[2].SetActive(true);


        // 모든 미션 클리어해서 clearMission 값이 5가 되면 게임 클리어 Scene으로 이동
        //if(!enemySpawnPoint[2].activeSelf && clearMission == 5)
        //{
        //    GameClear();
        //}

        pointText.text = "POINT : " + rabbitScore*2;
    }

    public void GameClear()
    {
        //엔딩씬으로 이동
        GetComponent<MoveScene>().OnClickLoadScene("Clear Scene");
     
    }
}
