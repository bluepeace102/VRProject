using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMission : MonoBehaviour
{
    public GameObject plag; // 꼽는곳
    public GameObject wireHead; //전선 머리
    Collider wireCol;
    public bool isClear; // 클리어 여부;

    Vector3 clickPoint;
    float moveSpeed = 5f;
    bool isCollide = false;
    // Start is called before the first frame update
    void Start()
    {
        // wireCol = wire.GetComponent<Collider>();
        Debug.Log("첫번째 미션 실행");

    }

    // Update is called once per frame
    void Update()
    {
        if (isCollide)
        {
            wireHead.transform.position = Vector3.Lerp(wireHead.transform.position, plag.transform.position, 3 * Time.deltaTime);
            wireHead.transform.forward = Vector3.Lerp(wireHead.transform.forward, plag.transform.forward, 3 * Time.deltaTime);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Plag")
        {
            Debug.Log("플래그에 충돌했습니다");
            isCollide = true;
        }
    }

    private void OnMouseDrag()
    {
        Vector3 diff = Input.mousePosition - clickPoint;
        Vector3 pos = wireHead.transform.position;

        pos.y += diff.y * Time.deltaTime * moveSpeed;

        wireHead.transform.position = pos;

        clickPoint = Input.mousePosition;

    }
}
