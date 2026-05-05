using System.Collections;
using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    public float strength = 10;
    public float speed = 3;
    public Vector2 xLimits = new(-18, 20);
    public Vector2 averageTime = new(3, 6);
    [SerializeField] private GameObject warningArea;

    private void Start() => StartCoroutine(Sequence());

    private void SetXPosition(float value)
    {
        transform.position = new Vector3
        {
            x = value,
            y = transform.position.y,
            z = transform.position.z
        };
    }

    private IEnumerator Sequence()
    {
        float moveTime = Time.time + Random.Range(averageTime.x, averageTime.y);
        SetXPosition(Random.Range(xLimits.x, xLimits.y));

        while(moveTime > Time.time)
        {
            if (transform.position.x < xLimits.x) speed = Mathf.Abs(speed);
            else if (transform.position.x > xLimits.y)
            {
                speed *= -1f;
                SetXPosition(xLimits.y);
            }

            SetXPosition(transform.position.x + (speed * Time.deltaTime));

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1.5f);

        // fall and damage
        transform.position = warningArea.transform.position;
        warningArea.SetActive(false);
        Destroy(Instantiate(Resources.Load<GameObject>("Effects/Impact Effect"), transform.position, Quaternion.identity), 3f);

        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);

        yield break;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().Health -= strength;
            return;
        }

        try
        {
            collision.collider.GetComponent<Entity>().Health -= strength;
        }
        catch { }
    }
}
