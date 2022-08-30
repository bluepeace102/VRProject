using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 원래자리(=target)로 돌아가게 만드는 기능
// 붙여놓은 물체를 원래 자리로 돌아가게한다.
// ex) 무기를 Player가 손에서 놓았을 때, 무기가 원래 있던 자리로 돌아가게 하는 역할
public class MovePosition : MonoBehaviour
{
    //오브제를 target 위치로 보내는 스크립트
    public float returnDistance = 0.1f;
    public float returnSpeed = 10f;
    public Transform target;


    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance > returnDistance)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, returnSpeed * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, target.forward, returnSpeed * Time.deltaTime);
        }
    }
}
