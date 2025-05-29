using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 followOffset;

    private void Update()
    {
        transform.position = target.position + followOffset;
    }
}
