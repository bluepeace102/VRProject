using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int rabbitScore; // �䳢 ��Ƽ� ���� ����
    public int clearMission = 1; //�̼� Ŭ���� �ܰ�

    public TextMeshProUGUI pointText;
    

    public GameObject[] enemySpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�̼� ���൵�� ���� ���ʹ� ����
        if (!enemySpawnPoint[0].activeSelf)
        {
            enemySpawnPoint[0].SetActive(true);
        }
        if (!enemySpawnPoint[1].activeSelf && clearMission > 1)
            enemySpawnPoint[1].SetActive(true);
        if (!enemySpawnPoint[2].activeSelf && clearMission < 3)
            enemySpawnPoint[2].SetActive(true);


        // ��� �̼� Ŭ�����ؼ� clearMission ���� 5�� �Ǹ� ���� Ŭ���� Scene���� �̵�
        //if(!enemySpawnPoint[2].activeSelf && clearMission == 5)
        //{
        //    GameClear();
        //}

        pointText.text = "POINT : " + rabbitScore*2;
    }

    public void GameClear()
    {
        //���������� �̵�
        GetComponent<MoveScene>().OnClickLoadScene("Clear Scene");
     
    }
}
