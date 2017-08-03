using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]   // Obriga que o game object tenha um Rigidbody para que o script funcione
[RequireComponent(typeof(NavMeshAgent))]    // Obriga que o game object tenha um NavMeshAgent para que o script funcione
[RequireComponent(typeof(Animator))]
public class Humanoid : MonoBehaviour {

    // COMPONENTES
    protected Rigidbody rgbd; // Referência do rigidbody
    protected NavMeshAgent navMeshAgent;
    protected AudioSource source;
    protected Animator anim;
    protected CapsuleCollider collider;

    // ATRIBUTOS
    [Header("-Atributos Gerais-", order = 0)]
    public int maxHp;
    public int hp;    // Vida do personagem / inimigo
    public int damage;  // Dano causado pelo personagem / inimigo
    public int nivel = 0;
    public float moveSpeed;
    public float meleeAttackRange;

    [Header("-Outros-", order = 2)]
    public AudioClip[] arraySFX;
    public bool dead;  // Flag indicando se o personagem / inimigo está morto

    protected bool moving = false;
    private bool running = false;

    protected Vector3 targetPos;
    protected Transform targetTransform;
    protected STATES state;

    protected void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = 2;

        navMeshAgent.speed = moveSpeed;
        hp = maxHp;
        state = STATES.IDLE;
    }

    void FixedUpdate()
    {
        UpdateAnimator();

        switch (state)
        {
            case STATES.IDLE:
                Idle();
                break;
            case STATES.MOVE:
                Move();
                break;
            case STATES.ATTACK:
                Attack();
                break;
            case STATES.DEAD:
                Die();
                break;
        }
    }

    public virtual void Idle()
    {
        CheckHP();
        rgbd.velocity = Vector3.zero;
        moving = false;
        running = false;
    }

    // Método para mover
    public virtual void Move()
    {
        CheckHP();

        float distanciaAlvo = Vector3.Distance(transform.position, targetPos);

        // Se a distancia entre o objeto e o target for maior do que a distancia de parada do navMeshAgent
        if (distanciaAlvo > navMeshAgent.stoppingDistance)
        {
            transform.LookAt(targetPos);    // Olha para o alvo
            moving = true; // Ativa a flag de movimento
            //rgbd.velocity = Vector3.zero;
            //rgbd.AddForce(transform.forward * moveSpeed);
            navMeshAgent.destination = targetPos;
        }
        else
        {
            state = STATES.IDLE;
        }
    }

    // Método para atacar
    public virtual void Attack()
    {
        CheckHP();
        moving = false;
        anim.SetTrigger("Attack");
        anim.ResetTrigger("Hit");
    }

    // Método chamado quando receber dano
    public virtual void TakeDamage(int damage)
    {
        CheckHP();
        anim.SetTrigger("Hit");
        anim.ResetTrigger("Attack");
    }

    public virtual void DealDamage(int damage)
    {
        CheckHP();
    }

    // Método chamado ao morrer
    public virtual void Die()
    {
        collider.enabled = false;
    }

    private void CheckHP()
    {
        // Se o HP acabar, ativa a flag dead
        if (hp <= 0)
        {
            dead = true;
        }

        if (dead)
        {
            state = STATES.DEAD;
        }
    }

    private void UpdateAnimator()
    {
        anim.SetBool("Moving", moving);
        anim.SetBool("Running", running);
        anim.SetBool("Dead", dead);
    }
}
