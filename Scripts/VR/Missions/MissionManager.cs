using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public MissionClickCheck[] missionObject;

    public GameObject player;
    public Transform[] missionPos;

    [SerializeField]
    int select = -1; // ���� ���õ� �̼� ��ȣ (* -1�� �ƹ��͵� ���õǾ����� ���� �ʱ� ����)

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

    // Ÿ�� ��ġ�� �÷��̾� ��ġ �̵�
    void MovePlayerToTargetPosition()
    {
        // �÷��̾ Ÿ�� ��ġ�� �̵�
        player.transform.position = Vector3.Lerp(player.transform.position, target.position, 3 * Time.deltaTime);
    }
    void SetTargetToSelectMission()
    {

        // ������ �̼ǿ� ���� Ÿ�� ��ġ ����
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

    // Ÿ���� � �̼������� ���� �ش� �̼� ����
    void PlayMissionAsTarget()
    {
        //if(target == missionPos[0])
        //{
        //    Debug.Log("1�� �̼�");
        //    FirstMission();
        //}
        if (target == missionPos[1] && manager.clearMission == 1)
        {
            Debug.Log("2�� �̼�");
            SecondMission();
        }
        else if (target == missionPos[2] && manager.clearMission == 2)
        {
            //Debug.Log("3�� �̼�");
            ThirdMission();
        }
        else if (target == missionPos[3] && manager.clearMission == 3)
        {
            Debug.Log("4�� �̼�");
            ForthMission();
        }
        else if (target == missionPos[4] && manager.clearMission == 4)
        {
            //Debug.Log("5�� �̼�");
            FifthMission();
        }
    }

    // ������ �̼Ǹ� Ŭ�� Ȱ��ȭ
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

    // Ŭ���� �̼��� 5���Ǵ� ��� �̼� Ŭ���� ������ �̵�
    void CheckMissionClear()
    {

        if (manager.clearMission == 5)
        {
            Debug.Log("���� Ŭ����!");
            //���̵��� ��
            GameObject.Find("EventSystem").GetComponent<MoveScene>().OnClickLoadScene("Clear Scene");
            manager.clearMission = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 1. ������ �̼Ǹ� Ŭ�� Ȱ��ȭ
        ActiveMission();

        // 2. �ش� �̼��� ��ġ�� �÷��̾� �̵�
        //MovePlayerToTargetPosition();

        // 3. ������ �̼��� Ÿ������ ����
        SetTargetToSelectMission();

        // 4. Ÿ���� � �̼������� ���� �ش� �̼� ����
        PlayMissionAsTarget();

        // Ŭ���� �̼��� 5���Ǵ� ��� �̼� Ŭ���� ������ �̵�
        //CheckMissionClear();
    }

    //ù��° �̼� - �˸��� �ڸ��� �ڵ� �ȱ�
    //void FirstMission()
    //{
    //    FirstMission missionFirst = missionObj[0].GetComponent<FirstMission>();
    //    target = missionPos[0];
    //    missionFirst.enabled = true;

    //    if (Input.GetKey(KeyCode.Backspace))
    //        //if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
    //    {
    //        Debug.Log("�ʱ� ȭ������ ���ư��ϴ�");
    //        ReturnCamera();
    //        missionFirst.enabled = false;
    //    }

    //    if (missionFirst.isClear)
    //    {
    //        ReturnCamera();

    //    }
    //    //ù��° �̼� �ڸ� Ŭ�����
    //    //Ư�� ��ư ������ ī�޶� ���� ���ư�
    //    //���콺 Ŭ�� �� �ڵ� ���ø�
    //    //�˸��� ����Ʈ�� �������� �� �ڵ尡 ����

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
        // ThirdMission Ȱ��ȭ
        if (FindObjectOfType<ThirdMission>().enabled == false)
        {
            FindObjectOfType<ThirdMission>().enabled = true;
        }
        // ���� Ÿ���� 3��° �̼����� ����
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
        //9ĭ����
    }

    void FifthMission()
    {
        // FifthMission Ȱ��ȭ
        if (FindObjectOfType<FifthMission>().enabled == false)
        {
            FindObjectOfType<FifthMission>().enabled = true;
        }
        // ���� Ÿ���� 3��° �̼����� ����
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
        //�̼� Ŭ���� ����Ʈ
        GameObject clearEffect = Instantiate(missionClearEffect);
        clearEffect.transform.position = mission.transform.position;
        clearEffect.transform.rotation = mission.transform.rotation;
        clearEffect.transform.localScale = mission.transform.localScale;
        clearEffect.GetComponent<ParticleSystem>().Stop();
        clearEffect.GetComponent<ParticleSystem>().Play();
        missionClearAudio.Play();
        //�̼� Ŭ���� �����
        ReturnCamera();
    }
}
