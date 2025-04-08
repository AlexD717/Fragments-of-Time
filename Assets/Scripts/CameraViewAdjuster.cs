using Unity.Cinemachine;
using UnityEngine;

public class CameraViewAdjuster : MonoBehaviour
{
    [SerializeField] private float maxZoom;
    [SerializeField] private float baseZoom;
    [SerializeField] private float zoomMultiplier;
    [SerializeField] private float minDistanceBeforeZoom;

    private GameObject playerTimeTraveled;
    private CinemachineCamera virtualCamera;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineCamera>();
    }

    private void Update()
    {
        if (playerTimeTraveled == null)
        {
            playerTimeTraveled = GameObject.FindGameObjectWithTag("PlayerTimeTraveled");
        }
        AdjustCameraView();
    }

    private void AdjustCameraView()
    {
        float distance = -minDistanceBeforeZoom;
        if (playerTimeTraveled != null)
        {
            distance = Vector2.Distance(playerTimeTraveled.transform.position, transform.position);
        }
        distance = Mathf.Clamp(distance, 0, Mathf.Infinity);

        float newZoom = baseZoom + (distance * zoomMultiplier);
        newZoom = Mathf.Clamp(newZoom, baseZoom, maxZoom);

        virtualCamera.Lens.OrthographicSize = newZoom;
    }
}
