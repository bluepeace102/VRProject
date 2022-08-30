using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPuzzle : MonoBehaviour
{
    public MissionClickCheck[] buttons;
    public Transform[] input;
    [SerializeField]
    Vector3[] answerPos = new Vector3[9];

    bool isAnswer;
    GameObject select;

    public bool isClear;

    
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<9; i++)
        {
            answerPos[i] = input[i].position;
        }
        //8 1 3 0 5 2 6 4 7
        input[0].position = answerPos[8];
        input[1].position = answerPos[1];
        input[2].position = answerPos[3];
        input[3].position = answerPos[0];
        input[4].position = answerPos[5];
        input[5].position = answerPos[2];
        input[6].position = answerPos[6];
        input[7].position = answerPos[4];
        input[8].position = answerPos[7];


        for (int i = 0; i < 9; i++)
        {
            buttons[i].gameObject.transform.position = input[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SortPuzzle();
        ChangePosition();

        isAnswer = true;
        for (int i=0; i<answerPos.Length; i++)
        {
            if(answerPos[i] != input[i].transform.position)
            {
                isAnswer = false;
                break;
            }
            else
            {
                continue;
            }
        }
        Debug.Log(isAnswer);
        if (isAnswer)
            GameObject.Find("GameManager").GetComponent<GameManager>().clearMission = 4;
    }

    void SortPuzzle() //퍼즐을 lerp 방식으로 이동, 정렬
    {
        for (int i = 0; i < 9; i++)
        {
            buttons[i].gameObject.transform.position = Vector3.Lerp(buttons[i].gameObject.transform.position, input[i].transform.position, 3 * Time.deltaTime);
        }
    }

    void ChangePosition() 
        //클릭한 오브젝트가 근접해 있을 경우 빈 자리와 위치 바꾸기
    {
        for(int i=0; i<buttons.Length; i++)
        {
            if (buttons[i].isClick)
            {
                select = buttons[i].gameObject;
                buttons[i].isClick = false;
            }
        }
        float distance = Vector3.Distance(select.transform.position, buttons[0].gameObject.transform.position);

        if (distance < 0.3)
        {
            Vector3 originPos = select.transform.position;
            select.transform.position = input[0].transform.position;
            input[0].transform.position = originPos;
        }
        
    }
    
}
