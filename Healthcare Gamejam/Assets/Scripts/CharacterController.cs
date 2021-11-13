using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    NavMeshAgent agent;
    // Animation Assets on character
    Animation anim;
    bool isOnTheMove = false;
    GameObject gameMaster;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // controls animations
        anim = gameObject.GetComponent<Animation>();
        StartCoroutine(CheckMoving());
        gameMaster = GameObject.Find("GAME_MASTER");
    }
    void Update()
    {
        if (isOnTheMove)
        {
            anim.Stop("honeyArmature_IdleHoney");
            anim.Play("honeyArmature_walkinghoney");
            //playerWalkingSound
        }else
        {
            anim.Stop("honeyArmature_walkinghoney");
            anim.Play("honeyArmature_IdleHoney");
        }

        //if (gameMaster.GetComponent<GameMaster>().isBeingTalkedTo)
        //{  
        //}

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
    public void StopPlayerToTalk(Vector3 NPCpos)
    {
        // stop moving
        gameMaster.GetComponent<GameMaster>().isBeingTalkedTo = true;
        //isBeingTalkedTo = true;

        // look at player
        gameObject.transform.forward = NPCpos;

        // stop patient
        Vector3 target = gameObject.transform.position;
        agent.SetDestination(target);
        //inCoRoutine = false; // redundant?
        //navMeshAgent.Stop(); // redundant?
        //navMeshAgent.isStopped = true; // redundant??
        // reset path 
        //navMeshAgent.ResetPath(); // redundant??
    }
    IEnumerator CheckMoving()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(.1f);
        Vector3 finalPos = transform.position;

        if (startPos != finalPos)
        {
            isOnTheMove = true;
        }else
        {
            isOnTheMove = false;
        }
        StartCoroutine(CheckMoving());
    }
}


//**********************************



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