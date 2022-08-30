using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ThirdMission : MonoBehaviour, IPointerClickHandler
{
    public float energyGauge = 0;   // ������ ä�� ��

    public Image enegyFill;         // ������ ä��� �̹���
    //public Image needle;            // �ٴ�

    public float lerpSpeed = 1f;    // ä������ �ӵ�

    public bool isClear;            // �̼� Ŭ���� ����

    private void Awake()
    {
    }

    private void Update()
    {
        // a. Energy �� �� ä������ ���� ���
        if (enegyFill.fillAmount < 1)
        {
            FillEnergyGauge();
        }
        // b. Enery �� �� ä���� ���
        else
        {
            MissionClear();
        }

        // Energy �������� �� ä������ ���� ��쿡�� ��� �پ��� �����.
        if (enegyFill.fillAmount > 0 && enegyFill.fillAmount < 1)
        {
            DecreaseEnergyGuage();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //energyGauge += 10;
    }

    // ��ư�� Ŭ���ϸ� ������ �������� ����.
    public void IncreaseEnergyGuage()
    {
        energyGauge += 10;
    }

    void FillEnergyGauge()
    {
        // Energy ������ ä���
        //enegyFill.fillAmount = Mathf.Lerp(enegyFill.fillAmount, energyGauge / 100, lerpSpeed * Time.deltaTime);
        enegyFill.fillAmount = energyGauge * 0.01f;
        // Energy ������ ä��� �Ϳ� ���߾� �ٴ� ȸ��
        //needle.transform.eulerAngles = Vector3.Lerp(needle.transform.eulerAngles,
            //new Vector3(0, 0, energyGauge / 100 * 360), 50f * Time.deltaTime);
    }

    void MissionClear()
    {
        Debug.Log("���� Ŭ����!");
        // ���� �̼����� �Ѿ����.. 
        // * 3��° �̼� Ŭ������ ����, 4��° �ǳʶٰ� ������ 5��°�̼�(=index4)�� �̵��� �� �ֵ��� ����
        GameObject.Find("GameManager").GetComponent<GameManager>().clearMission = 4;
        // �̼� Ŭ�����ϸ� Third�̼��� �����Ѵ�.
        enabled = false;

        // �̼� �Ŵ������� �̼� Ŭ���� ����Ʈ ����
        FindObjectOfType<MissionManager>().MissionClear(FindObjectOfType<MissionManager>().missionPos[2].gameObject);
    }

    void DecreaseEnergyGuage()
    {
        energyGauge -= 5f * Time.deltaTime;
    }
}
