using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FollowOtherCharacter : MonoBehaviour
{
    GameObject whomToFollow;
    bool followSomeone = false;
    NavMeshAgent navMeshAgent;
    GameObject gameMaster;
    Vector3 followOffset;

    private void Awake()
    {
        gameMaster = GameObject.Find("GAME_MASTER");
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void Follow(GameObject target)
    {
        whomToFollow = target;
        //followOffset = transform.localScale * 1.5f; // not working, dunno why?
        followSomeone = true;
    }

    void Update()
    {
        if (followSomeone)
        {
            navMeshAgent.SetDestination(whomToFollow.transform.position + followOffset); // should be body size*1.1f
        }
    }

    public void StopFollowing()
    {
        whomToFollow = null;
        followSomeone = false;
        gameMaster.GetComponent<GameMaster>().isBeingTalkedTo = false;
        gameMaster.GetComponent<GameMaster>().conversationPartner = null;
        followOffset = new Vector3(0f, 0f, 0f);
    }
}
