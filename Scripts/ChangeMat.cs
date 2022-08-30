using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChangeMat : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Material normalColor;
    [SerializeField] private Material enterColor;

    private MeshRenderer meshRenderer = null;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        normalColor = meshRenderer.material;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        meshRenderer.material = enterColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        meshRenderer.material = normalColor;
    }


}

