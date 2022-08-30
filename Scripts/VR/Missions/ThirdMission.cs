using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ThirdMission : MonoBehaviour, IPointerClickHandler
{
    public float energyGauge = 0;   // 에너지 채운 양

    public Image enegyFill;         // 에너지 채우기 이미지
    //public Image needle;            // 바늘

    public float lerpSpeed = 1f;    // 채워지는 속도

    public bool isClear;            // 미션 클리어 여부

    private void Awake()
    {
    }

    private void Update()
    {
        // a. Energy 가 다 채워지지 않은 경우
        if (enegyFill.fillAmount < 1)
        {
            FillEnergyGauge();
        }
        // b. Enery 가 다 채워진 경우
        else
        {
            MissionClear();
        }

        // Energy 게이지가 다 채워지지 않은 경우에는 계속 줄어들게 만든다.
        if (enegyFill.fillAmount > 0 && enegyFill.fillAmount < 1)
        {
            DecreaseEnergyGuage();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //energyGauge += 10;
    }

    // 버튼을 클릭하면 에너지 게이지가 찬다.
    public void IncreaseEnergyGuage()
    {
        energyGauge += 10;
    }

    void FillEnergyGauge()
    {
        // Energy 게이지 채우기
        //enegyFill.fillAmount = Mathf.Lerp(enegyFill.fillAmount, energyGauge / 100, lerpSpeed * Time.deltaTime);
        enegyFill.fillAmount = energyGauge * 0.01f;
        // Energy 게이지 채우는 것에 맞추어 바늘 회전
        //needle.transform.eulerAngles = Vector3.Lerp(needle.transform.eulerAngles,
            //new Vector3(0, 0, energyGauge / 100 * 360), 50f * Time.deltaTime);
    }

    void MissionClear()
    {
        Debug.Log("게임 클리어!");
        // 다음 미션으로 넘어가도록.. 
        // * 3번째 미션 클리어한 이후, 4번째 건너뛰고 마지막 5번째미션(=index4)로 이동할 수 있도록 세팅
        GameObject.Find("GameManager").GetComponent<GameManager>().clearMission = 4;
        // 미션 클리어하면 Third미션은 종료한다.
        enabled = false;

        // 미션 매니저에서 미션 클리어 이펙트 실행
        FindObjectOfType<MissionManager>().MissionClear(FindObjectOfType<MissionManager>().missionPos[2].gameObject);
    }

    void DecreaseEnergyGuage()
    {
        energyGauge -= 5f * Time.deltaTime;
    }
}
