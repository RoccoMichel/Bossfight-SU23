using UnityEngine;

public class HierarchyDivider : MonoBehaviour
{
	[SerializeField] string dividerName = "--------------------";

#if UNITY_EDITOR
    private void CleanUp()
    {
        transform.position = Vector3.zero;
        gameObject.name = dividerName;
    }
    private void OnValidate() => CleanUp();
	private void Reset() => CleanUp();
    private void OnEnable() => CleanUp();

#endif
}
