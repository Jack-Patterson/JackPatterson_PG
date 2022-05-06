using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForestCreature : ControlScript
{
    Animator animator;
    NavMeshAgent agent;

    private Vector2 minPos = new Vector2(-380f, -210f);
    private Vector2 maxPos = new Vector2(140f, 75f);
    private Vector2 minTownBounds = new Vector2(-150f, -85f);
    private Vector2 maxTownBounds = new Vector2(20f, 15f);

    bool isSprinting = false;
    bool isWaiting = false;

    bool hasTarget = false;
    Vector3 target;
    float check = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 4;
        setTarget(moveTarget(out target));
    }

    void Update()
    {
        check -= Time.deltaTime;

        if (check <= 0)
        {
            checkNotInTown();
            check = 5f;
        }

        if (hasTarget)
        {
            if (agent.pathPending)
            {
                return;
            }
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                hasTarget = false;
                isWaiting = true;
                wait(5);
            }
        }
        if (!isWaiting && !hasTarget)
        {   
            setTarget(moveTarget(out target));
        }
    }

    internal override void setTarget(Vector3 position)
    {
        agent.SetDestination(position);
    }

    private Vector3 moveTarget(out Vector3 target)
    {
        float x = Random.Range(minPos.x, maxPos.x);
        float z = Random.Range(minPos.y, maxPos.y);

        Ray ray = new Ray(new Vector3(x, 1000, z), Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 pos = new Vector3(x, hit.point.y, z);

        if (x < maxTownBounds.x && x > minTownBounds.x && z < maxTownBounds.y && z > minTownBounds.y)
        {
            return moveTarget(out target);
        }

        target = pos;
        hasTarget = true;
        return pos;
    }

    internal void toggleSprint()
    {
        if (isSprinting)
        {
            isSprinting = false;
        }
        else
        {
            isSprinting = true;
        }
    }

    private void wait(float time)
    {
        StartCoroutine(waitIE(time));
    }

    private IEnumerator waitIE(float time)
    {
        yield return new WaitForSeconds(time);

        Debug.Log("Waited");
        isWaiting = false;
    }

    void checkNotInTown()
    {
        if (transform.position.x < maxTownBounds.x && transform.position.x > minTownBounds.x &&
            transform.position.z < maxTownBounds.y && transform.position.z > minTownBounds.y)
        {
            setTarget(moveTarget(out target));
        }
    }

    internal override void CharacterStates()
    {
        throw new System.NotImplementedException();
    }
}
