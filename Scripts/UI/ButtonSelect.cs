using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private UnityEvent OnClick = new UnityEvent();

    private MeshRenderer meshRenderer = null;
    public TextMeshProUGUI text;
    Color originColor = Color.white;
    Color pushColer = Color.cyan;

    private void Awake()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = pushColer;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = originColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
