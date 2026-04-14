using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform focus;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (focus == null)
        {
            focus = GetPlayer();
        }

        transform.LookAt(focus);
    }

    Transform GetPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player").transform;
    }
}
