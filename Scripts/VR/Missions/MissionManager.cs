using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public MissionClickCheck[] missionObject;

    public GameObject player;
    public Transform[] missionPos;

    [SerializeField]
    int select = -1; // 현재 선택된 미션 번호 (* -1은 아무것도 선택되어있지 않은 초기 상태)

    Transform target;
    public Transform originPos;

    public GameObject missionClearEffect;
    public AudioSource missionClearAudio;

    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        target = originPos;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // 타깃 위치로 플레이어 위치 이동
    void MovePlayerToTargetPosition()
    {
        // 플레이어를 타깃 위치로 이동
        player.transform.position = Vector3.Lerp(player.transform.position, target.position, 3 * Time.deltaTime);
    }
    void SetTargetToSelectMission()
    {

        // 선택한 미션에 따라서 타겟 위치 설정
        switch (select)
        {
            //case "Mission 1":
            //    target = missionPos[0];
            //    break;
            case 0:
                target = missionPos[1];
                break;
            case 1:
                target = missionPos[2];
                break;
            case 2:
                target = missionPos[3];
                break;
            case 3:
                target = missionPos[4];
                break;
        }
    }

    // 타깃이 어떤 미션인지에 따라 해당 미션 실행
    void PlayMissionAsTarget()
    {
        //if(target == missionPos[0])
        //{
        //    Debug.Log("1번 미션");
        //    FirstMission();
        //}
        if (target == missionPos[1] && manager.clearMission == 1)
        {
            Debug.Log("2번 미션");
            SecondMission();
        }
        else if (target == missionPos[2] && manager.clearMission == 2)
        {
            //Debug.Log("3번 미션");
            ThirdMission();
        }
        else if (target == missionPos[3] && manager.clearMission == 3)
        {
            Debug.Log("4번 미션");
            ForthMission();
        }
        else if (target == missionPos[4] && manager.clearMission == 4)
        {
            //Debug.Log("5번 미션");
            FifthMission();
        }
    }

    // 선택한 미션만 클릭 활성화
    void ActiveMission()
    {
        for (int i = 0; i < missionObject.Length; i++)
        {
            if (missionObject[i].isClick)
            {
                select = i;
                missionObject[i].isClick = false;
            }
        }
    }

    // 클리어 미션이 5가되는 경우 미션 클리어 씬으로 이동
    void CheckMissionClear()
    {

        if (manager.clearMission == 5)
        {
            Debug.Log("게임 클리어!");
            //페이드인 후
            GameObject.Find("EventSystem").GetComponent<MoveScene>().OnClickLoadScene("Clear Scene");
            manager.clearMission = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 선택한 미션만 클릭 활성화
        ActiveMission();

        // 2. 해당 미션의 위치로 플레이어 이동
        //MovePlayerToTargetPosition();

        // 3. 선택한 미션을 타깃으로 설정
        SetTargetToSelectMission();

        // 4. 타깃이 어떤 미션인지에 따라 해당 미션 실행
        PlayMissionAsTarget();

        // 클리어 미션이 5가되는 경우 미션 클리어 씬으로 이동
        //CheckMissionClear();
    }

    //첫번째 미션 - 알맞은 자리에 코드 꽂기
    //void FirstMission()
    //{
    //    FirstMission missionFirst = missionObj[0].GetComponent<FirstMission>();
    //    target = missionPos[0];
    //    missionFirst.enabled = true;

    //    if (Input.GetKey(KeyCode.Backspace))
    //        //if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
    //    {
    //        Debug.Log("초기 화면으로 돌아갑니다");
    //        ReturnCamera();
    //        missionFirst.enabled = false;
    //    }

    //    if (missionFirst.isClear)
    //    {
    //        ReturnCamera();

    //    }
    //    //첫번째 미션 자리 클로즈업
    //    //특정 버튼 누르면 카메라 도로 돌아감
    //    //마우스 클릭 시 코드 들어올림
    //    //알맞은 포인트에 내려놓을 시 코드가 꽂힘

    //}

    void SecondMission()
    {

        SecondMission missionSecond = missionObject[1].GetComponent<SecondMission>();
        target = missionPos[1];
        missionSecond.enabled = true;

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            ReturnCamera();
            missionSecond.enabled = false;
        }
        if (missionSecond.isClear)
        {
            MissionClear(missionPos[1].gameObject);
        }
    }

    void ThirdMission()
    {
        // ThirdMission 활성화
        if (FindObjectOfType<ThirdMission>().enabled == false)
        {
            FindObjectOfType<ThirdMission>().enabled = true;
        }
        // 현재 타깃을 3번째 미션으로 변경
        if (target != missionPos[2])
        {
            target = missionPos[2];
        }
    }

    void ForthMission()
    {

        MovingPuzzle missionForth = missionObject[3].GetComponent<MovingPuzzle>();

        missionForth = missionObject[3].GetComponent<MovingPuzzle>();
        target = missionPos[3];

        missionForth.enabled = true;

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            ReturnCamera();
            missionForth.enabled = false;
        }
        if (missionForth.isClear)
        {
            MissionClear(missionPos[3].gameObject);
        }
        //9칸퍼즐
    }

    void FifthMission()
    {
        // FifthMission 활성화
        if (FindObjectOfType<FifthMission>().enabled == false)
        {
            FindObjectOfType<FifthMission>().enabled = true;
        }
        // 현재 타깃을 3번째 미션으로 변경
        if (target != missionPos[4])
        {
            target = missionPos[4];
        }
    }

    void ReturnCamera()
    {
        //target = originPos;
    }

    public void MissionClear(GameObject mission)
    {
        //미션 클리어 이펙트
        GameObject clearEffect = Instantiate(missionClearEffect);
        clearEffect.transform.position = mission.transform.position;
        clearEffect.transform.rotation = mission.transform.rotation;
        clearEffect.transform.localScale = mission.transform.localScale;
        clearEffect.GetComponent<ParticleSystem>().Stop();
        clearEffect.GetComponent<ParticleSystem>().Play();
        missionClearAudio.Play();
        //미션 클리어 오디오
        ReturnCamera();
    }
}
