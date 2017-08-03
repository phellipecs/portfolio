using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUriel : Player {

    [Header("-Skills-")]
    [Header("-Skill: Giro-")]
    public GameObject particulaGiro;
    public float cooldownGiro;
    public float rangeGiro;
    public int maxInimigosHit = 2;
    private int countInimigosHit;
    [HideInInspector]
    public float timerGiro;

    [Header("-Skill: Cura-")]
    public GameObject particulaCura;
    public int taxaCura = 30;
    public float cooldownCura = 15f;
    [HideInInspector]
    public float timerCura = 0f;

    [Header("-Skill: Raio-")]
    public GameObject particulaRaio;
    public int damageRaio = 50;
    public float cooldownRaio = 8f;
    [HideInInspector]
    public float timerRaio = 0f;
    public bool raioPronto = false;
    private GameObject raioTarget;

    [Header("-Skill: Aura-")]
    public GameObject particulaAura;
    public float duracaoAura;
    public float rangeAura;
    public float cooldownAura = 20f;
    public Transform maoEsquerda;
    private int danoAura;
    [HideInInspector]
    public float timerAura;

   public void Update()
    {
        if (timerGiro > 0)
        {
            timerGiro -= Time.deltaTime;
            UIManager.instance.ToggleCooldownIcon(0, true);
        } 

        if (timerCura > 0)
        {
            timerCura -= Time.deltaTime;
            UIManager.instance.ToggleCooldownIcon(1, true);
        }

        if (timerRaio > 0)
        {
            timerRaio -= Time.deltaTime;
            UIManager.instance.ToggleCooldownIcon(2, true);
        }
            
        if(timerAura > 0)
        {
            timerAura -= Time.deltaTime;
            UIManager.instance.ToggleCooldownIcon(3, true);
        }

        if (!dead)
        {
            if(ManagerXp.instance.skill1 >= 1)
                SkillGiroDaFuria();
            if (ManagerXp.instance.skill2 >= 1)
                SkillCuraPelasMaos();
            if (ManagerXp.instance.skill3 >= 1)
                SkillRaioDivino();
            if (ManagerXp.instance.skill4 >= 1)
                SkillAuraSagrada();
        }

        base.Update();
    }

    public void SkillGiroDaFuria()
    {
        if (timerGiro <= 0f)
        {
            UIManager.instance.ToggleCooldownIcon(0, false);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                anim.SetTrigger("Skill1");
                moving = false;

                Ray ray = new Ray(transform.position, Vector3.forward);
                RaycastHit[] hits = Physics.SphereCastAll(ray, rangeGiro, 1f, enemyLayer);

                maxInimigosHit += ManagerXp.instance.skill1;

                for (int i = 0; i < hits.Length; i += 2)
                {
                    if (countInimigosHit <= maxInimigosHit)
                    {
                        hits[i].collider.GetComponent<Enemy>().TakeDamage(damage + (ManagerXp.instance.skill1 * (int) 0.25f));
                        countInimigosHit++;
                    }
                }

                InstanciarEfeito(particulaGiro, Vector3.zero, new Vector3(90f, 0f, 0f));

                sourceSkill.clip = skillsSounds[0];
                sourceSkill.Play();

                timerGiro = cooldownGiro;
            }
        }
    }

    public void SkillCuraPelasMaos()
    {
        if (timerCura <= 0)
        {
            UIManager.instance.ToggleCooldownIcon(1, false);

            if (Input.GetKeyDown(KeyCode.W))
            {
                anim.SetTrigger("Skill2");

                InstanciarEfeito(particulaCura, Vector3.up * 2f, Vector3.zero);

                this.hp += taxaCura + ((int)0.25f * (ManagerXp.instance.skill2));
                this.hp = Mathf.Clamp(hp, 0, maxHp);

                sourceSkill.clip = skillsSounds[1];
                sourceSkill.Play();

                timerCura = cooldownCura;
            }

        }
    }

    public void SkillRaioDivino()
    {
        if (timerRaio <= 0)
        {
            UIManager.instance.ToggleCooldownIcon(2, false);

            if (Input.GetKeyDown(KeyCode.E))
            {
                raioPronto = !raioPronto;
            }
               
            if(Input.GetMouseButtonDown(0) && raioPronto)
            {
                GetMouseClick();

                anim.SetTrigger("Skill3");

                Instantiate(particulaRaio, transform.position, transform.rotation);

                sourceSkill.clip = skillsSounds[2];
                sourceSkill.Play();

                timerRaio = cooldownRaio;
                raioPronto = false;
            }
        }
    }

    public void SkillAuraSagrada()
    {
        if(timerAura <= 0)
        {
            UIManager.instance.ToggleCooldownIcon(3, false);

            if (Input.GetKeyDown(KeyCode.R))
            {
                anim.SetTrigger("Skill3");

                danoAura = damage + ((int)0.25f * ManagerXp.instance.skill4);

                var effect = Instantiate(particulaAura, maoEsquerda.position, Quaternion.identity);
                effect.transform.parent = this.transform;
                var effectObj = effect.GetComponent<ParticulaAuraDivina>();
                effectObj.auraAtiva = true;
                effectObj.dano = danoAura;
                effectObj.duracao = duracaoAura + (5 * ManagerXp.instance.skill4);
                effectObj.size = rangeAura + ManagerXp.instance.skill4;

                sourceSkill.clip = skillsSounds[3];
                sourceSkill.Play();

                timerAura = cooldownAura;
            }
        }
    }

    public void GetMouseClick()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit, enemyLayer))
        {
            if (hit.collider.gameObject.GetComponent<Enemy>())
            {
                raioTarget = hit.collider.gameObject;
                particulaRaio.GetComponent<EffectSettings>().UseMoveVector = false;
                particulaRaio.GetComponent<EffectSettings>().Target = raioTarget;
                raioTarget.GetComponent<Enemy>().TakeDamage(damageRaio + ((int)0.25f * (ManagerXp.instance.skill3)));
            }
            else
            {
                particulaRaio.GetComponent<EffectSettings>().UseMoveVector = true;
                particulaRaio.GetComponent<EffectSettings>().MoveVector = transform.forward.normalized;
            }
        }
    }

    void InstanciarEfeito(GameObject efeito, Vector3 pos, Vector3 rot)
    {
        var effect = Instantiate(efeito, new Vector3(transform.position.x + pos.x, transform.position.y + pos.y, transform.position.z + pos.z), Quaternion.identity);
        effect.transform.parent = this.transform;
        effect.transform.Rotate(rot);
    }

}
