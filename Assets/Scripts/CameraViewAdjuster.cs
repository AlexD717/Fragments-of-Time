using Unity.Cinemachine;
using UnityEngine;
using System.Collections.Generic;

public class CameraViewAdjuster : MonoBehaviour
{
    [SerializeField] private float maxZoom;
    [SerializeField] private float baseZoom;
    [SerializeField] private Vector2 zoomOffset;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float cameraZPos;

    private List<GameObject> targets;
    private GameObject playerTimeTraveled;
    private CinemachineCamera vcam;

    private Vector3 velocity;

    private void Start()
    {
        vcam = GetComponent<CinemachineCamera>();

        targets = new List<GameObject>();

        playerTimeTraveled = GameObject.FindGameObjectWithTag("PlayerTimeTraveled");
        if (playerTimeTraveled != null)
        {
            targets.Add(playerTimeTraveled);
        }
        targets.Add(GameObject.FindGameObjectWithTag("Player"));
    }

    private void LateUpdate()
    {
        targets.RemoveAll(item => item == null);
        if (targets.Count <= 0) { return; }

        AdjustCameraPosition();
        AdjustCameraView();
    }

    private void AdjustCameraPosition()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = new Vector3(centerPoint.x, centerPoint.y, cameraZPos);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, moveSpeed);
    }

    private void AdjustCameraView()
    {
        Vector2 boundsSize = GetGreatestDistance();
        float sizeX = boundsSize.x / zoomOffset.x;
        float sizeY = boundsSize.y / zoomOffset.y;
        Vector2 distance = Vector2.zero;

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

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 0)
        {
            return targets[0].transform.position;
        }

        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }

        return bounds.center;
    }

    private Vector2 GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }

        return new Vector2(bounds.size.x, bounds.size.y);
    }
}
