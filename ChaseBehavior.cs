using UnityEngine;
using UnityEngine.AI;

public class ChaseBehavior : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    EnemyTakeDamage enemy;
    float attackRange = 2;
    float chaseRange = 16;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 7;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = animator.GetComponent<EnemyTakeDamage>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < attackRange)
            animator.SetBool("isAttacking", true);

        if (distance > chaseRange && enemy.health == 100)
            animator.SetBool("isChasing", false);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        agent.speed = 0.5f;
    }
}
