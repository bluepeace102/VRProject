using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �����ڸ�(=target)�� ���ư��� ����� ���
// �ٿ����� ��ü�� ���� �ڸ��� ���ư����Ѵ�.
// ex) ���⸦ Player�� �տ��� ������ ��, ���Ⱑ ���� �ִ� �ڸ��� ���ư��� �ϴ� ����
public class MovePosition : MonoBehaviour
{
    //�������� target ��ġ�� ������ ��ũ��Ʈ
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
