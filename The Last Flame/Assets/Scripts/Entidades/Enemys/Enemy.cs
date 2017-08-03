using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Humanoid {

    public Player player;
    private Player alvo;
    public float attackInterval = 2f, timerAttack;
    public int quantidadeXP;
    public GameObject efeitoSangue;
    public bool clicouEnemy;

    [SerializeField]
    private bool levandoDano=false;
    public float timerlevandoDano;

    EnemyManager enemyManager;

    public Texture2D cursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public Material initialMaterial, finalMaterial;
    public SkinnedMeshRenderer esseRenderer;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        enemyManager = FindObjectOfType<EnemyManager>();
    }

	public void Update()
    {
        if (!dead)
        {
            if (timerAttack > 0)
                timerAttack -= Time.deltaTime;
        }

		if (levandoDano)  
		{
			timerlevandoDano += Time.deltaTime;
		}

		if (timerlevandoDano >= 2) 
		{
			levandoDano = false;
			timerlevandoDano = 0;
		}

			
    }

    private void OnMouseOver()
    {
        esseRenderer.material = finalMaterial;
        Cursor.SetCursor(cursor, hotSpot, cursorMode);
        clicouEnemy = true;
    }
    private void OnMouseExit()
    {
        esseRenderer.material = initialMaterial;
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        clicouEnemy = false;
    }

    public override void Idle()
    {
        if (player)
        {
            targetPos = player.transform.position;
            targetPos.y = transform.position.y;
            state = STATES.MOVE;
        }

        base.Idle();
    }

    public override void Move()
    {
        targetPos = player.transform.position;
        targetPos.y = transform.position.y;
        base.Move();
    }

    public override void Attack()
    {
        if (!levandoDano)
        {
            if(timerAttack <= 0f)
            {
                base.Attack();

                timerAttack = attackInterval;
            }
        }
    }

    public override void DealDamage(int damage)
    {
        damage += this.damage;

        if (player)
        {
            player.TakeDamage(damage);
        }
        else
        {
            Debug.LogError("Referencia do inimigo nulo");
        }
    }

    public override void Die()
    {
        player.experiencia += quantidadeXP;
        enemyManager.NumberEnemy--;

        Instantiate(efeitoSangue, transform.position, Quaternion.identity);

        Destroy(gameObject);
       
        //base.Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Trigger enter com player");
            if (player)
            {
                Debug.Log("ENTROU AQUI!");
                if (!player.dead)
                {
                    Debug.Log("ENTROU AQUI DOIS!");
                    state = STATES.ATTACK;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Trigger exit com player");
            if (player)
            {
                if (!player.dead)
                {
                    state = STATES.IDLE;
                }
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        hp -= damage;
		levandoDano = true;

		Instantiate(efeitoSangue, transform.position, Quaternion.identity);


        base.TakeDamage(damage);
    }

}

