using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    ParticleSystem particlesCopy;
    public Vector2 particlesPosition;
    public bool particlesState = true;
    void Start()
    {
         particlesCopy = Instantiate(particles,transform.position,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(particlesCopy != null)
        {
            particlesCopy.transform.position = particlesPosition;
            if (!particlesState)
            {
                particlesCopy.GetComponent<ParticlesDestroy>().haveToDie = true;
            }
        }

    }
}
