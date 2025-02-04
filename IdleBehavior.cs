using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    float timer;
    Transform player;
    EnemyTakeDamage enemy;
    float chaseRange = 10;

    private float patrolActivationTime = 10f; // время от 0 до patrolActivationTime для срабатывания
    private float randomPatrolTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = animator.GetComponent<EnemyTakeDamage>(); 
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        // Генерируем случайное время для активации патрулирования, если еще не задано
        if (randomPatrolTime <= 0)
        {
            randomPatrolTime = Random.Range(0f, patrolActivationTime);
        }

        // Если прошедшее время больше случайного времени, активируем патрулирование
        if (timer > randomPatrolTime)
        {
            animator.SetBool("isPatrolling", true);
        }

        float distance = Vector3.Distance(animator.transform.position, player.position);
        
        if (distance < chaseRange || enemy.health < 100)
        {
            animator.SetBool("isChasing", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
