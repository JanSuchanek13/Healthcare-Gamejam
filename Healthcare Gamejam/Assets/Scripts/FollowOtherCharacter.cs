using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FollowOtherCharacter : MonoBehaviour
{
    GameObject whomToFollow;
    bool followSomeone = false;
    NavMeshAgent navMeshAgent;
    GameObject gameMaster;

    private void Awake()
    {
        gameMaster = GameObject.Find("GAME_MASTER");
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void Follow(GameObject target)
    {
        whomToFollow = target;
        followSomeone = true;
    }

    void Update()
    {
        if (followSomeone)
        {
            navMeshAgent.SetDestination(whomToFollow.transform.position);
        }
    }

    public void StopFollowing()
    {
        whomToFollow = null;
        followSomeone = false;
        gameMaster.GetComponent<GameMaster>().isBeingTalkedTo = false;
        gameMaster.GetComponent<GameMaster>().conversationPartner = null;
    }
}
