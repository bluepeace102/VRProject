using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Lever : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private UnityEvent OnClick = new UnityEvent();
    public Transform leverDown;

    Vector3 clickPoint;
    float moveSpeed = 5f;

    private MeshRenderer meshRenderer = null;

    private void Update()
    {
        if (transform.position.y < leverDown.position.y)
        {
            Debug.Log("레버를 내렸습니다");
            GameObject.Find("GameManager").GetComponent<GameManager>().clearMission = 5;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 diff = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) - clickPoint;
        Vector3 pos = transform.localPosition;
        pos.y += diff.y * Time.deltaTime * moveSpeed;
        transform.position = pos;

        clickPoint = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
    }

}

