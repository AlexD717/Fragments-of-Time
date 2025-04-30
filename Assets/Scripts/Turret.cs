using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float fireCooldown;
    private float nextFireTime = 0f;
    [SerializeField] private float laserSpeed;
    [SerializeField] private bool cameraShowsThis;

    [Header("References")]
    [SerializeField] private GameObject laser;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject turretHead;

    private void Start()
    {
        if (cameraShowsThis)
        {
            CameraViewAdjuster cameraViewAdjuster = FindFirstObjectByType<CameraViewAdjuster>();
            cameraViewAdjuster.AddNewTarget(gameObject);
        }
    }

    private void Update()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireCooldown;
            FireLaser();
        }
    }

    private void FireLaser()
    {
        GameObject spawnedLaserObject = Instantiate(laser, firePoint.position, firePoint.rotation);
        Laser spawnedLaser = spawnedLaserObject.GetComponent<Laser>();
        spawnedLaser.speed = laserSpeed;
        spawnedLaser.parentSpawner = turretHead;
    }
}
