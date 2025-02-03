using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    private float timer;
    private Transform player;
    private EnemyTakeDamage enemy;
    private float chaseRange = 10;

    private float patrolActivationTime = 10f; // время от 0 до patrolActivationTime для срабатывания 
    private float randomPatrolTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = animator.GetComponent<EnemyTakeDamage>();

        // Подписываемся на событие изменения здоровья
        enemy.OnHealthChanged += HandleHealthChanged;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Отписываемся от изменения здоровья
        enemy.OnHealthChanged -= HandleHealthChanged;
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

        if (distance < chaseRange)
        {
            animator.SetBool("isChasing", true);
        }
    }

    private void HandleHealthChanged(float newHealth)
    {
        // Реакция на изменение здоровья
        if (newHealth < 100)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("isChasing", true);
        }
    }
}
