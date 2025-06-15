using UnityEngine;

public class BotShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public Transform targetA;
    public Transform targetB;

    public float timeBetweenTargets = 1.0f;
    public float pauseDuration = 3.0f;

    public GameObject objectToToggle;

    private enum ShootStep { Paused, ShootA, WaitToShootB, ShootB }
    private ShootStep currentStep = ShootStep.Paused;

    private float stepStartTime;

    public float bulletSpeed = 20f;

    void Start()
    {
        currentStep = ShootStep.Paused;
        stepStartTime = Time.time;
    }

    void Update()
    {
        switch (currentStep)
        {
            case ShootStep.Paused:
                if (Time.time - stepStartTime >= pauseDuration)
                {
                    if (objectToToggle != null) objectToToggle.SetActive(false);
                    ShootAt(targetA);
                    currentStep = ShootStep.WaitToShootB;
                    stepStartTime = Time.time;
                }
                break;

            case ShootStep.WaitToShootB:
                if (Time.time - stepStartTime >= timeBetweenTargets)
                {
                    ShootAt(targetB);
                    currentStep = ShootStep.Paused;
                    stepStartTime = Time.time;
                    if (objectToToggle != null) objectToToggle.SetActive(true);
                }
                break;
        }
    }

    void ShootAt(Transform target)
    {
        if (bulletPrefab != null && firePoint != null && target != null)
    {
        Vector3 direction = (target.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
    }
}
