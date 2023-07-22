using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyControleur : PersonnagesControl
{
    private NavMeshAgent navAgent;
    //private float wanderDistance = 4f;

    [Header("Combat")]
    [SerializeField] float attackCD = 1.5f;
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float aggroRange = 8f;
    bool aggro = false;

    GameObject player;
    float timePassed;
    float newDestinationCD = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // S'il y a un NavMeshAgent sur l'empty, on le recupère (toujours appelé avec notre exemple)
        if (navAgent == null)
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (inLife)
        {

            if (!inAnimation)
            {

                if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
                {
                    navAgent.SetDestination(transform.position);
                    run(false);

                    if (timePassed >= attackCD)
                    {
                        animator.SetTrigger("Attack");
                        timePassed = 0;
                    }
                }
                else if (newDestinationCD <= 0 && (Vector3.Distance(player.transform.position, transform.position) <= aggroRange || aggro))
                {
                    aggro = true;
                    newDestinationCD = 0.5f;
                    run(true);
                    navAgent.SetDestination(player.transform.position);
                }

                timePassed += Time.deltaTime;
                newDestinationCD -= Time.deltaTime;
                transform.LookAt(player.transform);
            }
        }
        
    }

    public void die()
    {
        Destroy(gameObject);
    }

    public void stopMove()
    {
        navAgent.isStopped= true;
    }

    //Run
    public void run(bool situation)
    {
        animator.SetBool("Run", situation);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
