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
        // ������ ��� ������ٸ�...
        //if (lever.position.x > leverDown.position.x)
        //{
        //    Debug.Log("������ ���Ƚ��ϴ�");
        //}

        // ������ �������.
        //VRDrag();
    }

    public void VRDrag()
    {

        // ��Ʈ�ѷ� Ŭ������ ��,
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            Debug.Log("����");
            // ���� �ʱ� ��ġ 
            clickPoint = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        }
        // ��Ʈ�ѷ� Ŭ�� ���� ��
        else if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            Debug.Log("���� Ŭ����");
            // ���� �����̱�
            Vector3 diff = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) - clickPoint;
            Vector3 pos = lever.position;
            pos.y += diff.y * Time.deltaTime * moveSpeed;
            lever.position = pos;

            clickPoint = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        }
    }
    public void GameClear()
    {
        // Lever Slide Down �ִϸ��̼� ��� ������ ��� ��� �Ŀ� �� �̵�
        StartCoroutine(ClearMission());
    }

    IEnumerator ClearMission()
    {
        yield return null;

        // 5 ��° �̼� Ŭ����
        GameObject.Find("GameManager").GetComponent<GameManager>().clearMission = 5;
        // 5 ���� �̼� Ŭ��� ���� Clear �̼� ��ƼŬ ����
        FindObjectOfType<MissionManager>().MissionClear(FindObjectOfType<MissionManager>().missionPos[4].gameObject);

        // 5�� ��� �Ŀ�..
        yield return new WaitForSeconds(5f);

        // �� �̵�
        FindObjectOfType<GameManager>().GameClear();
    }


}
