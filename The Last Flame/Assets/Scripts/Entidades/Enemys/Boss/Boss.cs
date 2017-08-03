using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System;

public class Boss : Enemy {

    public SoundManager soundBoss;
    public AudioSource bossBattle;
[Header("-Skill: MagiaMao-")]
public float coolDownMagiaMao;
public GameObject particulaMagiaMao;
public int damageMagiaMao = 50;
[HideInInspector]
private float timerMagiaMao=0;
public bool magiaMaoPronto = false;

    /*public void Update()
	{
		if (timerMagiaMao > 0) {
			timerMagiaMao -= Time.deltaTime;
		}

		base.Update ();
	}*/

    public void Start()
    {
        soundBoss = FindObjectOfType<SoundManager>();
        bossBattle = GameObject.Find("somBossBattle").GetComponent<AudioSource>();
        soundBoss.musicSource2.Stop();
        bossBattle.Play();
        bossBattle.volume = 0.3f;
        player = FindObjectOfType<PlayerUriel>();
    }
    public override void Die()

{
    	Debug.Log ("morreu");
        StartCoroutine(SegundosMorte());
        //Destroy (gameObject, 3f);
        //Application.LoadLevel(5);
        ////Invoke("LevelManager.instance.LoadScene('GameOver')", 3f);
        //base.Die();
        //SceneManager.LoadScene("GameOver");
    }

	public void MagiaMao()

	{
		if (timerMagiaMao <= 0)
		{
			///anim.SetTrigger ("SkillMagiaMao");
			moving=false;
			Instantiate (particulaMagiaMao, transform.position, transform.rotation);
			timerMagiaMao = coolDownMagiaMao;
		}
	}
    IEnumerator SegundosMorte()
    {
        anim.SetBool("Dead", true);
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene("GameOver");
    }

}
