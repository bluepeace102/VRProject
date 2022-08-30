using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour { 

    public Transform hand;
    public float catchSpeed;
    public float throwForce;
    public GameObject missionManager;
    MissionManager manager;
    //public float catchRadius = 5f;
    //public float catchDistance = 5f;

    public GameObject catchObj;
    GameObject detectObj;

    public float MaxHP = 1500;
    float currentHP;

    public Slider PlayerHPBar;

    void Start()
    {
        manager = missionManager.GetComponent<MissionManager>();
        currentHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ��ü�� ���� ��, ���� ���콺 ��ư Ŭ�� or OVR ��Ʈ�ѷ� ��ư ������ ���
        //if (Input.GetMouseButtonDown(0))
        if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && catchObj != null)
        {
            // ���� ��ü �����ֱ�...
            catchObj.GetComponent<WeaponEvents>().OnDrop();
        }

        // ���� ��ü�� �ִ� ��� 
        if (catchObj != null)
        {
            // ���� ��ü�� ��(hand) position ���� �ε巴�� �̵�
            catchObj.transform.position = Vector3.Lerp(catchObj.transform.position, hand.position, catchSpeed * Time.deltaTime);
        }

        // Player Hp Bar�� ���� ü�¿� ���߾� ������ ����ȭ
        if (PlayerHPBar.value != currentHP / MaxHP)
        {
            PlayerHPBar.value = Mathf.Lerp(PlayerHPBar.value, currentHP / MaxHP, 5f * Time.deltaTime);
        }


    }
    
    public void Damage(int damage)
    {
        currentHP = currentHP - damage;


        //HP�� ���ҵǾ� 0�̵Ǹ� ������ ����ȴ�
        if (currentHP <= 0)
        {
            Debug.Log("���ӿ���");
            GameObject.Find("GameManager").GetComponent<MoveScene>().OnClickLoadScene("Game Over Scene");
        }
        //�������� :Debug.Log("���� ����")
    }
}
