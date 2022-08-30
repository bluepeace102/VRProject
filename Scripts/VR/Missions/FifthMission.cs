using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FifthMission : MonoBehaviour
{
    Vector3 clickPoint;
    float moveSpeed = 5f;

    public Transform lever;
    public Transform leverDown;

    public bool isClear;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // 레버를 모두 끌어당겼다면...
        //if (lever.position.x > leverDown.position.x)
        //{
        //    Debug.Log("레버를 내렸습니다");
        //}

        // 레버를 끌어당긴다.
        //VRDrag();
    }

    public void VRDrag()
    {

        // 컨트롤러 클릭했을 때,
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            Debug.Log("레버");
            // 레버 초기 위치 
            clickPoint = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        }
        // 컨트롤러 클릭 중일 때
        else if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            Debug.Log("레버 클릭중");
            // 레버 움직이기
            Vector3 diff = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) - clickPoint;
            Vector3 pos = lever.position;
            pos.y += diff.y * Time.deltaTime * moveSpeed;
            lever.position = pos;

            clickPoint = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        }
    }
    public void GameClear()
    {
        // Lever Slide Down 애니메이션 모두 끝나면 잠시 대기 후에 씬 이동
        StartCoroutine(ClearMission());
    }

    IEnumerator ClearMission()
    {
        yield return null;

        // 5 번째 미션 클리어
        GameObject.Find("GameManager").GetComponent<GameManager>().clearMission = 5;
        // 5 번재 미션 클리어에 따른 Clear 미션 파티클 실행
        FindObjectOfType<MissionManager>().MissionClear(FindObjectOfType<MissionManager>().missionPos[4].gameObject);

        // 5초 대기 후에..
        yield return new WaitForSeconds(5f);

        // 씬 이동
        FindObjectOfType<GameManager>().GameClear();
    }


}
