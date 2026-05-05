using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Minion : Entity
{
    [Header("Minion Attributes")]
    public float strength = 2;
    public float timePerAttack = 1.5f;
    private float nextAttackTime = 0;
    private NavMeshAgent agent;
    private Transform target;
    private Player player;

    protected override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = target.GetComponent<Player>();
        base.OnStart();
    }

    public void Attack()
    {
        if (nextAttackTime > Time.time) return;
        print(identity);
        nextAttackTime = Time.time + timePerAttack;
        player.Health -= strength;
    }

    private void Update()
    {
        agent.SetDestination(target.position);
        
        if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 1.6f) Attack();
    }
}
