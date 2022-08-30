using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float mouseSensitivity = 5f;
    float horizontalAngle;
    float verticalAngle;
    //�÷��̾� ������ ����(ȸ��) ��ũ��Ʈ : Ű���� ���
    //VR ���� �Ⱦ�

    // Update is called once per frame
    void Update()
    {

        //�¿�ȸ��

        if(Input.GetKey(KeyCode.RightArrow)) // ������ ȸ��
        {
            Debug.Log("������ ȸ��ǥ �Է�");
            horizontalAngle += mouseSensitivity * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("���� ȭ��ǥ �Է�");
            horizontalAngle -= mouseSensitivity * Time.deltaTime;
        }

        //ȸ���� 0~360�� ���� ���� (����ȭ)
        if (horizontalAngle > 360) horizontalAngle -= 360;
        if (horizontalAngle < 0) horizontalAngle += 360;

        //3. �÷��̾� ȸ���� ��������
        Vector3 currentPlayerAngle = transform.localEulerAngles;

        //4. ������ ������ Y���� ȸ������������ ��ȯ
        currentPlayerAngle.y = horizontalAngle;

        //5. �÷��̾� ȸ������ �ٽ�����
        transform.localEulerAngles = currentPlayerAngle;


        //----------------------------------����ȸ��


        //�����̵�������
        if (Input.GetKey(KeyCode.DownArrow))
        {
            verticalAngle += mouseSensitivity * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            verticalAngle -= mouseSensitivity * Time.deltaTime;
        }

        //ī�޶� ���� -90~90�� ������ �ֱ⸦ �ٶ�
        verticalAngle = Mathf.Clamp(verticalAngle, -89.0f, 45.0f);

        Vector3 currentCameraAngle = Camera.main.transform.localEulerAngles;
        currentCameraAngle.x = verticalAngle;
        Camera.main.transform.localEulerAngles = currentCameraAngle;

    }
}
