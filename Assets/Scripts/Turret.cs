using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float fireCooldown;
    private float nextFireTime = 0f;
    [SerializeField] private float laserSpeed;

    [Header("References")]
    [SerializeField] private GameObject laser;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject turretHead;
    [SerializeField] private Image fillImage;

    private void Start()
    {
        nextFireTime = Time.time + fireCooldown;
        fillImage.fillAmount = 0f;
    }

    private void Update()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireCooldown;
            FireLaser();
        }
        else
        {
            fillImage.fillAmount = Mathf.Clamp((1 - (nextFireTime - Time.time)/fireCooldown), 0f, 1f);
        }
    }

    private void FireLaser()
    {
        GameObject spawnedLaserObject = Instantiate(laser, firePoint.position, firePoint.rotation);
        Laser spawnedLaser = spawnedLaserObject.GetComponent<Laser>();
        spawnedLaser.speed = laserSpeed;
        spawnedLaser.parentSpawner = turretHead;

        fillImage.fillAmount = 0f;
    }
}
