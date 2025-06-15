using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
     public float maxHealth = 100f;
    private float currentHealth;

    public Image healthBar; // imagem com tipo "Filled"
    public float damageAmount = 10f;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            TakeDamage(damageAmount);
        }
    }

    void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

