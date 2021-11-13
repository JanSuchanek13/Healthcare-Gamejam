using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverSeeThroughWalls : MonoBehaviour
{
    private LayerMask wallMask;
    private Camera mainCamera;
    private Vector3 myPos;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        myPos = gameObject.transform.position;
    }

    private void OnMouseOver()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(myPos);
        cutoutPos.y /= (Screen.width / Screen.height);
        Vector3 offset = myPos - mainCamera.transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(mainCamera.transform.position, offset, offset.magnitude, wallMask);

        for (int i = 0; i < hitObjects.Length; ++i)
        {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int m = 0; m < materials.Length; ++m)
            {
                materials[m].SetVector("_CutoutPos", cutoutPos);
                materials[m].SetFloat("_CutoutSize", 0.1f);
                materials[m].SetFloat("_FalloffSize", 0.05f);
            }
        }
    }
    /*
    private void Update()
    {
        //add this:
        RaycastHit hit;
        //mousePosition = hit;


        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(myPos);
        cutoutPos.y /= (Screen.width / Screen.height);
        Vector3 offset = myPos - mainCamera.transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(mainCamera.transform.position, offset, offset.magnitude, wallMask);

        for (int i = 0; i < hitObjects.Length; ++i)
        {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int m = 0; m < materials.Length; ++m)
            {
                materials[m].SetVector("_CutoutPos", cutoutPos);
                materials[m].SetFloat("_CutoutSize", 0.1f);
                materials[m].SetFloat("_FalloffSize", 0.05f);
            }
        }
    }*/
}
