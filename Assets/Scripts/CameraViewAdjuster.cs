using Unity.Cinemachine;
using UnityEngine;

public class CameraViewAdjuster : MonoBehaviour
{
    [SerializeField] private float maxZoom;
    [SerializeField] private float baseZoom;
    [SerializeField] private Vector2 zoomOffset;
    [SerializeField] private float zoomSpeed;

    private GameObject playerTimeTraveled;
    private CinemachineCamera vcam;

    private void Start()
    {
        vcam = GetComponent<CinemachineCamera>();
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
        Vector2 distance = Vector2.zero;
        if (playerTimeTraveled != null)
            distance = new Vector2(Mathf.Abs(transform.position.x - playerTimeTraveled.transform.position.x), Mathf.Abs(transform.position.y - playerTimeTraveled.transform.position.y));
        
        float sizeX = distance.x / zoomOffset.x;
        float sizeY = distance.y / zoomOffset.y;

        if (sizeX > sizeY)
        {
            if (sizeX < baseZoom)
            {
                sizeX = baseZoom;
            }
            else if (sizeX > maxZoom)
            {
                sizeX = maxZoom;
            }
            vcam.Lens.OrthographicSize = Mathf.Lerp(vcam.Lens.OrthographicSize, sizeX, Time.deltaTime * zoomSpeed);
        }
        else
        {
            if (sizeY < baseZoom)
            {
                sizeY = baseZoom;
            }
            else if (sizeY > maxZoom)
            {
                sizeY = maxZoom;
            }
            vcam.Lens.OrthographicSize = Mathf.Lerp(vcam.Lens.OrthographicSize, sizeY, Time.deltaTime * zoomSpeed);
        }
    }
}
