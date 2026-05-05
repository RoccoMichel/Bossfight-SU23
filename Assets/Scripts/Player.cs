using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    public float timePerAttack = 1.1f;
    private float nextAttackTime = 0;
    private InputAction attackAction;

    public void Attack()
    {
        Instantiate(Resources.Load<GameObject>("Spin Attack"), transform);
    }

    protected override void OnStart()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        base.OnStart();
    }

    private void Update()
    {
        if (attackAction.WasPressedThisFrame() && Time.time > nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + timePerAttack;
        }
    }


    public override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    protected override void OnReset()
    {
        gameObject.tag = "Player";
        identity = "Player";
        maxHealth = 100;
    }
}
