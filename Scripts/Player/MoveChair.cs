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
        // �ε巴�� Target ��ġ�� �̵�
        transform.position = Vector3.Lerp(transform.position, target.position, 5f * Time.deltaTime);
        // �ε巴�� Target �� �ٶ󺸴� Forward �������� �ڽ��� Forward ������ ����
        //transform.forward = Vector3.Lerp(transform.forward, target.forward, 5f * Time.deltaTime);

        // �� ȸ�� ����, Target ȸ�� �� ����ȭ
        transform.rotation = target.rotation;
    }
}
