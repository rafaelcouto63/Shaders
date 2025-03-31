using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject muzzle;           // Referência ao Muzzle onde os projéteis serão disparados
    public GameObject bulletPrefab;     // Referência ao Prefab do projétil
    public float timeBetweenShots = 4f; // Tempo entre os disparos (em segundos)
    public int bulletsPerBurst = 3;     // Quantidade de projéteis por vez
    public float bulletSpeed = 10f;     // Velocidade do projétil

    public GameObject target;           // O alvo para a torre mirar

    private void Start()
    {
        // Inicia a corrotina de disparo
        StartCoroutine(ShootBurst());
    }

    private void Update()
    {
        if (target != null)
        {
            // Faz a torre olhar para o alvo
            Vector3 directionToTarget = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 100f); // Rotaciona suavemente
        }
    }

    private IEnumerator ShootBurst()
    {
        while (true)
        {
            for (int i = 0; i < bulletsPerBurst; i++)
            {
                ShootBullet();
                yield return new WaitForSeconds(0.1f); // Intervalo entre os projéteis (ajustável)
            }
            yield return new WaitForSeconds(timeBetweenShots); // Espera entre os disparos (4 segundos)
        }
    }

    private void ShootBullet()
    {
        // Cria uma instância do projétil na posição e rotação do Muzzle
        GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation);

        // Verifica se o projétil tem um RigidBody
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Aplica uma força na direção do Muzzle
            rb.linearVelocity = muzzle.transform.forward * bulletSpeed; // Direção e velocidade
        }
    }
}
