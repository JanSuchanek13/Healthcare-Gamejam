using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveRandomly : MonoBehaviour
{
    // tutorial base: https://www.youtube.com/watch?v=RXB7wKSoupI 

    //stats that worked: at 100 scale
    /* speed: .9
     * angular drag: 90
     * accell 8
     * stoppin .2
     * radius .0005
     * height 1e-05
     */

    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    float minTimerForNewPath;
    float maxTimerForNewPath;
    bool inCoRoutine;
    bool validPath;
    Vector3 target;
    GameObject gameMaster;

    // Animation Assets on character
    Animation anim;

    void Awake()
    {
        gameMaster = GameObject.Find("GAME_MASTER");

        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();

        // this part is to get character moving right on spawn
        minTimerForNewPath = .1f;
        maxTimerForNewPath = .1f;
        Invoke("StartMoving", .5f);

        // controls animations
        anim = gameObject.GetComponent<Animation>();
        StartCoroutine(CheckMoving());
    }

    // here the MeshNav receives its real time-variables
    void StartMoving()
    {
        minTimerForNewPath = gameMaster.GetComponent<GameMaster>().minTimerForNewPath;
        maxTimerForNewPath = gameMaster.GetComponent<GameMaster>().maxTimerForNewPath;
        //minTimerForNewPath = 3f;
        //maxTimerForNewPath = 8f;
    }

    Vector3 getNewRandomPosition()
    {
        float x = Random.Range(-100f, 100);
        float z = Random.Range(-100f, 100);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }
    IEnumerator DoSomething()
    {
        inCoRoutine = true;
        yield return new WaitForSeconds(Random.Range(minTimerForNewPath, maxTimerForNewPath));
        GetNewPath();
        validPath = navMeshAgent.CalculatePath(target, path);

        while (!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
        }
        inCoRoutine = false;
    }
    public void GetNewPath()
    {
        target = getNewRandomPosition();
        navMeshAgent.SetDestination(target);
    }
    void Update()
    {
        if (isOnTheMove)
        {
            anim.Stop("RabbitArmature_RabbitIdle");
            anim.Play("RabbitArmature_RabbitWalking");
        }else
        {
            anim.Stop("RabbitArmature_RabbitWalking");
            anim.Play("RabbitArmature_RabbitIdle");
        }

        if (gameMaster.GetComponent<GameMaster>().isBeingTalkedTo == false && !inCoRoutine)
        {
            StartCoroutine(DoSomething());
        }

        /*if (isBeingTalkedTo)
        {
            StopAndTalk(vector3 playerPos);
        }*/
    }
    
    bool isOnTheMove = false;
    IEnumerator CheckMoving()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(.1f);
        Vector3 finalPos = transform.position;
        
        if(startPos != finalPos)
        {
            isOnTheMove = true;
        }else
        {
            isOnTheMove = false;
        }
        StartCoroutine(CheckMoving());
    }

    //bool isBeingTalkedTo = false;

    // Jan calls this when player moves close to patient
    // hands over his position to look at
    public void StopAndTalk(Vector3 playerPos)
    {
        // stop moving
        gameMaster.GetComponent<GameMaster>().isBeingTalkedTo = true;
        //isBeingTalkedTo = true;
        
        // look at player
        gameObject.transform.forward = playerPos;

        // stop patient
        target = gameObject.transform.position;
        navMeshAgent.SetDestination(target);
        //inCoRoutine = false; // redundant?
        //navMeshAgent.Stop(); // redundant?
        //navMeshAgent.isStopped = true; // redundant??
        // reset path 
        //navMeshAgent.ResetPath(); // redundant??
    }
}
