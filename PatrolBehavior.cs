using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehavior : StateMachineBehaviour
{
    float timer;
    List<Transform> points = new List<Transform>();
    NavMeshAgent agent;
    Transform player;
    EnemyTakeDamage enemy;
    float chaseRange = 10;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        Transform pointsObject = GameObject.FindGameObjectWithTag("Points").transform;
        int numberOfPoints = pointsObject.childCount;
        for (int i = 0; i < numberOfPoints; i++)
        {
            points.Add(pointsObject.GetChild(i));
        }
        agent = animator.GetComponent<NavMeshAgent>();

        int randomIndex = Random.Range(0, numberOfPoints);
        agent.SetDestination(points[randomIndex].position);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = animator.GetComponent<EnemyTakeDamage>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(points[Random.Range(0, points.Count)].position);
        timer += Time.deltaTime;
        if (timer > 20)
            animator.SetBool("isPatrolling", false);

        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < chaseRange || enemy.health < 100)
            animator.SetBool("isChasing", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}
