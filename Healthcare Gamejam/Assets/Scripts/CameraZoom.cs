using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] float minZoom = 120f;
    [SerializeField] float maxZoom = 20f;
    [SerializeField] float zoomSpeed = 20f;

    void FixedUpdate()
    {
        // -------------------Code for Zooming Out------------
        if (Input.GetAxis("Mouse ScrollWheel") < 0 | Input.GetKey("down"))
        {
            if (Camera.main.fieldOfView <= minZoom)
            {
                Camera.main.fieldOfView += zoomSpeed * Time.deltaTime;
            }
            if (Camera.main.orthographicSize <= 20)
            {
                Camera.main.orthographicSize += 0.5f;
            }
        }
        // ---------------Code for Zooming In------------------------
        if (Input.GetAxis("Mouse ScrollWheel") > 0 | Input.GetKey("up"))
        {
            if (Camera.main.fieldOfView > maxZoom)
            {
                Camera.main.fieldOfView -= zoomSpeed * Time.deltaTime;
            }
            if (Camera.main.orthographicSize >= 1)
            {
                Camera.main.orthographicSize -= 0.5f;
            }
        }
    }
}
