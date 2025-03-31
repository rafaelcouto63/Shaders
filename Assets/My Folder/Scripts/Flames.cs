using System.Collections;
using UnityEngine;

public class Flames : MonoBehaviour
{
    public ParticleSystem fireParticles; // Sistema de partículas para o fogo
    public float fireDuration = 3f;      // Duração do fogo ativo (em segundos)
    public float waitDuration = 3f;      // Duração de espera entre os disparos de fogo (em segundos)

    public GameObject target;            // O GameObject da mira (pode ser qualquer objeto no jogo)
    
    private void Start()
    {
        // Inicia a corrotina que controla o ciclo do flamethrower
        StartCoroutine(FlameCycle());
    }

    private void Update()
    {
        if (target != null)
        {
            // Faz o objeto olhar para a mira (target) a cada frame
            transform.LookAt(target.transform);
        }
    }

    private IEnumerator FlameCycle()
    {
        while (true)
        {
            // Ativa o fogo (inicia o sistema de partículas)
            fireParticles.Play();

            // Espera pelo tempo que o fogo deve durar
            yield return new WaitForSeconds(fireDuration);

            // Desativa o fogo (para o sistema de partículas)
            fireParticles.Stop();

            // Espera antes de iniciar o próximo ciclo de fogo
            yield return new WaitForSeconds(waitDuration);
        }
    }
}
