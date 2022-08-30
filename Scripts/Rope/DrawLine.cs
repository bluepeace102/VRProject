using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    LineRenderer lr;
    public float wireWidth;
    public GameObject[] wireCube;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = wireWidth;
        lr.endWidth = wireWidth;
    }

    void Update()
    {
        for(int i=0; i<wireCube.Length; i++)
        {
            lr.SetPosition(i, wireCube[i].GetComponent<Transform>().position);
        }
        

    }



}
