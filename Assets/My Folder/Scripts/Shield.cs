using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public float maxShield = 100f;
    public float currentShield;
    public Image shieldBar; // Arraste a Image do Canvas aqui
    public float damage = 5f;
    public float damageFire = 1f;


    void Start()
    {
        currentShield = maxShield;
        UpdateShieldUI();
    }

    public void TakeDamage(float damage)
    {
        currentShield -= damage;
        currentShield = Mathf.Clamp(currentShield, 0, maxShield);
        UpdateShieldUI();

        if (currentShield <= 0)
        {
            Destroy(gameObject); // Destroi o escudo quando chega a 0
        }
    }

    void UpdateShieldUI()
    {
        if (shieldBar != null)
        {
            shieldBar.fillAmount = currentShield / maxShield;
        }
    }

    // Simulação de dano ao colidir com um inimigo ou projétil
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger ativado por: " + other.gameObject.name);
        Debug.Log("Tag detectada: " + other.gameObject.tag);
        Debug.Log("Layer detectada: " + other.gameObject.layer);
        if (other.CompareTag("EnemyProjectile") || other.gameObject.layer == 3) // Certifique-se de marcar os projéteis com essa Tag
        {
            TakeDamage(damage);
            Destroy(other.gameObject); // Remove o projétil
        }
    }

    private void OnParticleCollision(GameObject other)
{
    Debug.Log("Colisão detectada com partículas de: " + other.gameObject.name);

    if (other.CompareTag("EnemyProjectile"))
    {
        TakeDamage(damageFire);
    }
}

    
}
