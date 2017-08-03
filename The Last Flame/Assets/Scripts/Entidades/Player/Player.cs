using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Humanoid {

    [Header("-Efeitos sonoros skills-")]
    public AudioClip[] skillsSounds;
    public AudioSource sourceSkill;

    [Header("-Atributos Player-")]
    public int defense = 10;
    public int experiencia;
    private int proxNivelXP;
    public float attackInterval = 2f;
    public LayerMask enemyLayer;
    public GameObject efeitoLevelUp, dustFeedback;

    private float timerAttack;
    public Enemy enemy;
    private Enemy alvo;
    private int damageReflet;
    public bool colidiuEnemy;
    [HideInInspector]
    public bool silence = false;
    public TutorialManager tutorialGO;



    private void Start()
    {
        source = GetComponent<AudioSource>();
        colidiuEnemy = true;
    }

    protected void Update()
    {
        if (!dead)
            if (Input.GetMouseButtonDown(0))
                GetMousePosition();

        if (timerAttack > 0)
            timerAttack -= Time.deltaTime;
    }


    private void GetMousePosition()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit))
        {
            if (hit.collider.tag == "clicavel")
            {
                targetPos = hit.point;
                targetPos.y = transform.position.y;
                if(hit.collider.gameObject.layer == 9)
                {
                    Instantiate(dustFeedback, hit.point, hit.transform.rotation);
                }

                if (hit.collider.GetComponent<Enemy>())
                {
                    enemy = hit.collider.GetComponent<Enemy>();
                    

                    if (!enemy.dead)
                    {
                        //Checa distancia do inimigo
                        float distanciaDoInimigo = Vector3.Distance(transform.position, hit.point);

                        if (distanciaDoInimigo > meleeAttackRange)
                        {
                            state = STATES.MOVE;
                        }
                        else
                        {
                            state = STATES.ATTACK;
                        }
                    }
                }
                else
                {
                    // Mover até o local
                    state = STATES.MOVE;
                }
            }
        }
    }

    public override void Move()
    {
        source.clip = arraySFX[2];
        source.loop = true;
        source.Play();

        base.Move();
    }

    public override void Die()
    {
        Application.LoadLevel(5);

        base.Die();
    }

    public override void Attack()
    {
        if (!silence)
        {
            if(timerAttack <= 0f)
            {
                transform.LookAt(targetPos);
                timerAttack = attackInterval;
            }
            else
            {
                state = STATES.IDLE;
            }
        }

        base.Attack();
    }

    public override void TakeDamage(int damage)
    {
        if (damage > defense)
        {
            hp -= damage - defense;
        }

        source.clip = arraySFX[0];
        source.loop = false;
        source.Play();

        base.TakeDamage(damage);
    }

    public override void DealDamage(int damage)
    {
        Debug.Log("Player DealDamage");
        damage += this.damage;

        if (enemy)
        {
            enemy.TakeDamage(damage);
            source.clip = arraySFX[1];
            source.loop = false;
            source.Play();

            Debug.Log("Vida inimigo " + enemy.hp);
        }
        else
        {
            Debug.LogError("Referencia do inimigo nulo");
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            if(colidiuEnemy)
            {
                tutorialGO.trig_2 = true;
            }
        }
    }

}
