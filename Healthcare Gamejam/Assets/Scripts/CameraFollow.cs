using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTrans;
    Vector3 objOffset;

    void Start()
    {
        objOffset = transform.position - playerTrans.position;
    }

    void FixedUpdate()
    {
        float objOffsetMulti = playerTrans.localScale.x;
        Vector3 newPos = playerTrans.position + objOffset * objOffsetMulti;
        transform.position = newPos;
    }
}
