using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    [SerializeField] private Vector2 grid =  new Vector2(1,1);

    private void OnDrawGizmos()
    {
        SnapGrid();
    }

    void SnapGrid()
    {
        transform.position = new Vector2(Mathf.Round(transform.position.x / grid.x) * grid.x, Mathf.Round(transform.position.y / grid.y) * grid.y);
    }

}

