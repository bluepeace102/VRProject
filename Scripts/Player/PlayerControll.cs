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
        // 잡은 물체가 있을 때, 내가 마우스 버튼 클릭 or OVR 콘트롤러 버튼 눌렀을 경우
        //if (Input.GetMouseButtonDown(0))
        if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && catchObj != null)
        {
            // 잡은 물체 놓아주기...
            catchObj.GetComponent<WeaponEvents>().OnDrop();
        }

        // 잡은 물체가 있는 경우 
        if (catchObj != null)
        {
            // 잡은 물체를 손(hand) position 으로 부드럽게 이동
            catchObj.transform.position = Vector3.Lerp(catchObj.transform.position, hand.position, catchSpeed * Time.deltaTime);
        }

        // Player Hp Bar를 현재 체력에 맞추어 게이지 동기화
        if (PlayerHPBar.value != currentHP / MaxHP)
        {
            PlayerHPBar.value = Mathf.Lerp(PlayerHPBar.value, currentHP / MaxHP, 5f * Time.deltaTime);
        }


    }
    
    public void Damage(int damage)
    {
        currentHP = currentHP - damage;


        //HP가 감소되어 0이되면 게임이 종료된다
        if (currentHP <= 0)
        {
            Debug.Log("게임오버");
            GameObject.Find("GameManager").GetComponent<MoveScene>().OnClickLoadScene("Game Over Scene");
        }
        //게임종료 :Debug.Log("게임 오버")
    }
}
