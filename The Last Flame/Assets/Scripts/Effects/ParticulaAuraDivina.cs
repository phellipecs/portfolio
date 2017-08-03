using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulaAuraDivina : MonoBehaviour {

    public ParticleSystem[] effects;
    public float size;
    public float duracao;
    public int dano;
    public bool auraAtiva = false;

    private SphereCollider collider;
    private DestroyEffect destroyScript;
    private float cooldownDano = 1f;
    private float timerDano = 0f;

    private void Start()
    {
        effects = GetComponentsInChildren<ParticleSystem>();
        collider = GetComponent<SphereCollider>();
        destroyScript = GetComponent<DestroyEffect>();

        destroyScript.timeToDestroy = duracao + 1;
        effects[0].startSize = size;
        collider.radius = size / 3;
    }

    private void Update()
    {
        if (timerDano > 0)
            timerDano -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (auraAtiva)
        {
            if (other.GetComponent<Enemy>())
            {
                if (timerDano <= 0)
                {
                    Debug.Log("Dano por segundo");
                    other.GetComponent<Enemy>().TakeDamage(dano);
                    timerDano = cooldownDano;
                }

            }
        }

    }

}
