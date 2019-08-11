using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleManager : MonoBehaviour
{
    public GameObject particle;
    public int maxParticles = 100;

    private int _particleCounter;


    public void StartSpawning()
    {
        Clear();
        StartCoroutine(SpawnParticle());
    }

    private IEnumerator SpawnParticle()
    {
        for (_particleCounter = 0; _particleCounter <= maxParticles; _particleCounter++)
        {
            var o = Instantiate(particle, transform);
            var shift = 0f;

            if (_particleCounter % 2 == 0)
                shift = .5f;
            if (_particleCounter % 3 == 0)
                shift = -.5f;

            o.transform.position += Vector3.left * shift;

            yield return new WaitForSeconds(.03f);
        }
    }

    private void Clear()
    {
        _particleCounter = 0;
        var toDelete = new List<GameObject>();
        foreach (Transform child in transform)
        {
            toDelete.Add(child.gameObject);
        }

        toDelete.ForEach(o =>
            {
                o.SetActive(false);
                Destroy(o);
            }
        );
    }
}