using UnityEngine;

public class ParticleCollector : MonoBehaviour
{
    public int particlesCollected;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Particle")) return;
        
        var o = other.gameObject;
        o.SetActive(false);
        Destroy(o);

        particlesCollected++;
    }
}
