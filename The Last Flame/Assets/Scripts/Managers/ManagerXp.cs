using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SKILLSN
{
    GIRO,
    CURA,
    RAIO,
    AURA
}


public class ManagerXp : Singleton<ManagerXp> {

    public Player player;
    public TutorialManager tutorialGO;
    public bool passouNivel, gastouPonto;
    public int level = 0;
    SKILLSN skillEnum;

    public int skillPoints;
    public int skill1 = 0;
    public int skill2 = 0;
    public int skill3 = 0;
    public int skill4 = 0;
    public int skillMaxLevel = 4;

    public int experienciaUp = 1;
	
	void Start ()
    {
        Debug.Log("Start do manager xp");
        this.player = FindObjectOfType<Player>();
        skillPoints = 0;

        gastouPonto = false;
        passouNivel = true;
	}

	void Update ()
    {
        Up();
        
	}

    public void Up()
    {
        if (player.experiencia >= experienciaUp)
        {
            if (passouNivel)
            {
                tutorialGO.trig_3 = true;
            }            
            Debug.Log("Upou");
            Instantiate(player.efeitoLevelUp, player.transform.position, Quaternion.identity);
            level++;
            skillPoints++;
            player.experiencia = 0;
            experienciaUp *= 2;
        }
    }


   
    public void GastarPontos(int skillN)
    {
        Debug.Log("Click");
        gastouPonto = true;

        if (skillPoints > 0)
        {
            switch (skillN)
            {
                case 0:
                    if(skill1 < skillMaxLevel)
                        skill1++;
                    break;

                case 1:
                    if (skill2 < skillMaxLevel)
                        skill2++;
                    break;

                case 2:
                    if (skill3 < skillMaxLevel)
                        skill3++;
                    break;

                case 3:
                    if (skill4 < skillMaxLevel)
                        skill4++;
                    break;

            }

            skillPoints--;
        }

        //if((gameObject.name=="ButtonSkill1" && skillPoints > 0))
        //{
        //    skill1++;
        //    Debug.Log("Cliclou" + skill1);
        //}

        //if ((gameObject.name == "ButtonSkill2" && skillPoints > 0))
        //{
        //    skill2++;
        //}

        //if ((gameObject.name == "ButtonSkill3" && skillPoints > 0))
        //{
        //    skill3++;
        //}

        //if ((gameObject.name == "ButtonSkill4" && skillPoints > 0))
        //{
        //    skill4++;
        //}
        
    }


}
