using UnityEngine;
using UnityEngine.UI;

public class BotLife : MonoBehaviour
{
    public float maxShield = 100f;
    public float currentShield;
    public Image shieldBar; // Arraste a Image do Canvas aqui
    public float damage = 5f;
    public float damageFire = 1f;

    private Material botMaterial; // Material do bot
    public float cutoffSpeed = 0.1f; // Velocidade de transição do cutoff (quanto mais baixo, mais rápido diminui)

    void Start()
    {
        currentShield = maxShield;
        UpdateShieldUI();

        // Obtém o material do objeto
        botMaterial = GetComponent<Renderer>().material;
    }

    public void TakeDamage(float damage)
    {
        currentShield -= damage;
        currentShield = Mathf.Clamp(currentShield, 0, maxShield);
        UpdateShieldUI();

    }

    void Update()
    {
        // Se o escudo foi destruído, começa a diminuir o Cutoff
        if (currentShield <= 0 && botMaterial != null)
        {
            DecreaseCutoff();
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
        if (other.CompareTag("EnemyProjectile") || other.gameObject.layer == 3) // Certifique-se de marcar os projéteis com essa Tag
        {
            TakeDamage(damage);
            Destroy(other.gameObject); // Remove o projétil
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            TakeDamage(damageFire);
        }
    }

    // Função para diminuir o Cutoff no Update()
    private void DecreaseCutoff()
    {
        float cutoffValue = botMaterial.GetFloat("_CuttOff"); // Obtém o valor atual do CutOff (com "O" maiúsculo)

        // Diminui o valor de Cutoff de 1 para 0 ao longo do tempo
        if (cutoffValue > 0)
        {
            cutoffValue -= cutoffSpeed * Time.deltaTime; // Diminui o Cutoff
            botMaterial.SetFloat("_CuttOff", Mathf.Clamp(cutoffValue, 0f, 1f)); // Atualiza o valor do CutOff
        }

    }
}
