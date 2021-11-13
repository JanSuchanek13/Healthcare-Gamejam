using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    //public Vector3 GoToAllyPoint; // why tho
    NavMeshAgent agent;
    //private GameObject cameraObject;
    //private Camera mainCamera;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //cameraObject = GameObject.Find("Main Camera");
        //mainCamera = cameraObject.GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                agent.SetDestination(hit.point);
            }
            /* Debug.Log("step 1");

             Transform targetTransform = null; // target assigned in inspector 
             Debug.Log("step 2");

             RaycastHit[] hits; // returned hits
             Debug.Log("step 3");

             LayerMask mask = new LayerMask(); // set in inspector
             Debug.Log("step 4");

             hits = Physics.RaycastAll(transform.position, transform.forward, Mathf.Infinity);
             Debug.Log("step 5");

             for (int i = 0; i < hits.Length; i++)
             {
                 Debug.Log("step 6");

                 if (hits[i].collider.gameObject.CompareTag("Floor"))
                 {
                     Debug.Log("Found Floor!");

                     // Found something with a given tag. 
                 }
             }

             /*
                 Debug.Log("step 1");

             //RaycastHit hit;
             RaycastHit[] hits;
             Debug.Log("step 2");

             LayerMask mask = new LayerMask();
             hits = Physics.RaycastAll(cameraObject.transform.position, transform.forward, mask);
             hits = Physics.RaycastAll(mainCamera.transform.position, transform.forward, Mathf.Infinity);
             Debug.Log("step 3");


             for (int i = 0; i < hits.Length; i++)
             {
                 Debug.Log("step 4");

                 if (hits[i].collider.gameObject.CompareTag("Floor"))
                 {
                     Debug.Log("step 5");

                     //agent.SetDestination(hit.point);
                 }
             }


                 /*if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                 {
                     //GameObject hitObjct = hit.collider.gameObject;

                     hit.collider.gameObject.GetComponent<Collider>().enabled = false; 

                     agent.SetDestination(hit.point);
                 }
                 if (hit.collider.gameObject.CompareTag("Wall"))
                 {
                     hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                     RaycastHit newHit;
                 }
                 // old script
                 if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
             {
                 agent.SetDestination(hit.point);
             }*/
        }
    }
}
