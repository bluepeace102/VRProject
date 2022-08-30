using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WeaponEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent OnClick = new UnityEvent();
    public Transform hand;
    public Transform handModel;
    public float catchSpeed;
    public float throwForce;
    public GameObject VRPointer;

    PlayerControll player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControll>();
        VRPointer = GameObject.Find("PhysicsPointer");
        //hand = GameObject.Find("Ghost-RightHand").transform;
        hand = GameObject.Find("RightHandAnchor").transform;
        handModel = GameObject.Find("Ghost-RightHand").transform;
    }

    // 물체를 잡으려고 클릭했을 때 실행(=당근 입장에서는 잡힌 상태)
    public void OnCatch()
    {
        Debug.Log(this.gameObject.name + " Was PointerDown.");

        // 잡힌상태 -------
        // 1. 나 자신의 물리법칙 off
        GetComponent<Rigidbody>().isKinematic = true;
        // 2. 내 부모를 hand (=Player의 손으로 설정)
        transform.parent = hand;
        // 3. Player한테는 catchObj(=잡은 물체)를 '나'잡았다고 알려줌
        player.catchObj = transform.gameObject;
        // 4.잡은 경우, 무기가 원래 자리로 돌아가지 않도록 해당 기능 컴포넌트 비활성.
        if (GetComponent<MovePosition>().enabled)
        {
            GetComponent<MovePosition>().enabled = false;
        }

        VRPointer.SetActive(false);         // 잡았을 때는, Line 꺼주기
        handModel.gameObject.SetActive(false);   // 잡았을 때는, Hand 꺼주기.. 
    }

    // 물체를 놓으려고 클릭했을 때 실행(=당근 입장에서는 놓여지는 상태)
    public void OnDrop()
    {
        Debug.Log(this.gameObject.name + " Was PointerUp.");
        // 1. 놓였으니까 다시 물리법칙 off
        GetComponent<Rigidbody>().isKinematic = false;
        // 1-1. 중력 법칙도 on
        GetComponent<Rigidbody>().useGravity = true;
        // 2. Player한테도 잡은 물체 이제 없는 상태라고 알려줌
        player.catchObj = null;
        // 3. 내 부모도 없어짐, 망망대해에 홀로 남음
        transform.parent = null;

        // 4. OVR 컨트롤러가 이동하는 이동(속도&방향)에 맞추어서 ThroForce만큼의 힘으로 던짐
        GetComponent<Rigidbody>().velocity =
            OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch) * throwForce;
        // 4-1. OVR 컨트롤러의 방향에 맞추어 던진 물체의 회전값 설정
        GetComponent<Rigidbody>().angularVelocity =
            OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);

        VRPointer.SetActive(true);         // 잡았을 때는, Line 켜주기
        handModel.gameObject.SetActive(true);   // 잡았을 때는, Hand 켜주기.. 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Enemy")
        {
            Debug.Log("공격 적중");
            Rabbit rabbit = collision.transform.GetComponentInParent<Rabbit>();
            rabbit.DamageProcess();
        }
        //// 에너미한테 닿은 게 아니라, 바닥에 닿으면 다시 원래자리로 돌아간다.
        //else if(collision.transform.tag == "Ground")
        //{
        //    // 리턴.. 
        //    GetComponent<MovePosition>().enabled = true;
        //}
    }
}
