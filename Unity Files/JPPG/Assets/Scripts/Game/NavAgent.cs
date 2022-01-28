using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgent : MonoBehaviour { 

    public Camera cam;

    public NavMeshAgent agent;

    public AICharacterMove aICharacterMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.getBuildMode())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);
                }
            }

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                aICharacterMove.Move(agent.desiredVelocity);
            }
            else
            {
                aICharacterMove.Move(Vector3.zero);
            }
        }
    }
}
