using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float mouseSensitivity = 5f;
    float horizontalAngle;
    float verticalAngle;
    //플레이어 움직임 제어(회전) 스크립트 : 키보드 사용
    //VR 때는 안씀

    // Update is called once per frame
    void Update()
    {

        //좌우회전

        if(Input.GetKey(KeyCode.RightArrow)) // 오른쪽 회전
        {
            Debug.Log("오른쪽 회살표 입력");
            horizontalAngle += mouseSensitivity * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("왼쪽 화살표 입력");
            horizontalAngle -= mouseSensitivity * Time.deltaTime;
        }

        //회전값 0~360도 사이 유지 (최적화)
        if (horizontalAngle > 360) horizontalAngle -= 360;
        if (horizontalAngle < 0) horizontalAngle += 360;

        //3. 플레이어 회전값 가져오기
        Vector3 currentPlayerAngle = transform.localEulerAngles;

        //4. 가져온 값에서 Y축을 회전누적값으로 변환
        currentPlayerAngle.y = horizontalAngle;

        //5. 플레이어 회전값에 다시적용
        transform.localEulerAngles = currentPlayerAngle;


        //----------------------------------상하회전


        //상하이동값누적
        if (Input.GetKey(KeyCode.DownArrow))
        {
            verticalAngle += mouseSensitivity * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            verticalAngle -= mouseSensitivity * Time.deltaTime;
        }

        //카메라 각도 -90~90도 까지만 있기를 바람
        verticalAngle = Mathf.Clamp(verticalAngle, -89.0f, 45.0f);

        Vector3 currentCameraAngle = Camera.main.transform.localEulerAngles;
        currentCameraAngle.x = verticalAngle;
        Camera.main.transform.localEulerAngles = currentCameraAngle;

    }
}
