using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MissionClickCheck : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent OnClick = new UnityEvent();
    [SerializeField] private Animation animation;

    public AudioSource buttonAudio;
    public bool isClick = false;
   

    private void Awake()
    {
        if (this.GetComponent<Animation>() != null)
        {
            animation = GetComponent<Animation>();
            
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isClick = true;
        buttonAudio.Play();
        //animation.Play();
    }
}