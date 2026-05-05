using UnityEngine;

public class SpinAttack : MonoBehaviour
{
    public float damage = 5;
    public float lifetime = 0.8f;
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            Entity entity = other.GetComponent<Entity>();
            entity.Health -= damage;
        }
        catch { /*Object on entity layer does not have an entity component*/ }
    }

    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            Entity entity = collision.transform.GetComponent<Entity>();
            entity.Health -= damage;
        }
        catch { /*Object on entity layer does not have an entity component*/ }
    }
}
