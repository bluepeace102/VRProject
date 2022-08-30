using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChair : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 부드럽게 Target 위치로 이동
        transform.position = Vector3.Lerp(transform.position, target.position, 5f * Time.deltaTime);
        // 부드럽게 Target 이 바라보는 Forward 방향으로 자신의 Forward 방향을 맞춤
        //transform.forward = Vector3.Lerp(transform.forward, target.forward, 5f * Time.deltaTime);

        // 내 회전 값과, Target 회전 값 동기화
        transform.rotation = target.rotation;
    }
}
