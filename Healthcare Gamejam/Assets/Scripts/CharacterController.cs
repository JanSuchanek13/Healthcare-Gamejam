using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    public Vector3 GoToAllyPoint; // why tho
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        }
    }
}
