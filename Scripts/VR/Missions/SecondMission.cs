using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMission : MonoBehaviour
{
    public MissionClickCheck[] missionBtn;
    int index = 0;
    public bool isClear = false;


    int[] answer = { 0, 1, 2, 1, 2, 0 };
    int[] input = new int[6];


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("2번 미션 시작");
    }

    // Update is called once per frame
    void Update()
    {
        //클릭된 오브젝트 확인
        for (int i = 0; i < missionBtn.Length; i++)
        {
            if (missionBtn[i].isClick)
            {
                input[index] = i;
                missionBtn[i].isClick = false;
                index++;
            }
        }

        if (index > 5)
        {
            if (ReturnAnswer())
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().clearMission = 2;
            }
            else
            {
                index = 0;
            }
            
        }


    }


    bool ReturnAnswer()
    {
        for(int i=0; i<answer.Length; i++)
        {
            if(answer[i] != input[i])
            {
                return false;
            }
        }
        GameObject.Find("GameManager").GetComponent<GameManager>().clearMission = 2;
        return true;
    }
}
