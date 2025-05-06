using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    void Start()
    {
        CameraViewAdjuster cameraViewAdjuster = FindFirstObjectByType<CameraViewAdjuster>();
        if (cameraViewAdjuster != null)
        {
            cameraViewAdjuster.AddNewTarget(gameObject);
        }
    }
}
