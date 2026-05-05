using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float distanceFromPlayer = 13;
    public float height = 4;
    [Range(0f,1f)] public float horizontalPullEffect = 0.2f;
    private Transform focus;

    // Update is called once per frame
    void Update()
    {
        if (focus == null)
        {
            focus = GetPlayer();
        }

        transform.LookAt(focus);
        Vector3 newPos = new Vector3
        {
            x = Mathf.Lerp(0, focus.position.x, horizontalPullEffect),
            y = height,
            z = focus.position.z - distanceFromPlayer
        };
        transform.position = newPos;
    }

    Transform GetPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player").transform;
    }
}
