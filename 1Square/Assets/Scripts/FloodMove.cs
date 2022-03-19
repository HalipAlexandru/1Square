using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodMove : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private bool isOn = false;

    private Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        if(isOn)
            transform.Translate(speed * Time.deltaTime * Vector3.up);
    }

    public void OnOFF(bool state)
    {
        isOn = state;
    }

    public void Reset()
    {
        transform.position = pos;
    }
}
