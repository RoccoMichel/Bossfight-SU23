using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    public override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    protected override void OnReset()
    {
        gameObject.tag = "Player";
        identity = "Player";
        maxHealth = 100;
        base.OnReset();
    }
}
