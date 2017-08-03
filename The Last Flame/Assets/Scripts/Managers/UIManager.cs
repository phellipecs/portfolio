using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    private GameObject[] cooldownImagens;
    private Image healthBar;
    private PlayerUriel player;
    private GameObject[] btnSkills;
    private GameObject[] iconsNvSkills;

    private void Start()
    {
        player = FindObjectOfType<PlayerUriel>();
        cooldownImagens = new GameObject[4];
        healthBar = GameObject.Find("HP_Bar").GetComponent<Image>();
        btnSkills = new GameObject[4];
        iconsNvSkills = new GameObject[GameObject.FindGameObjectsWithTag("iconNvSkill").Length];

        BuscarIconesNivelSkills();
        BuscarIconesCooldown();
        BuscarBotoesUpSkill();
    }

    private void Update()
    {
        if(LevelManager.instance.currentScene == "Game")
        {
            AtualizarNivelSkill();
            AtualizarCooldown();
            AtualizarBarraHP();
            HabilitarIconesSkills();
            activeBtn();
        }
    }

    void BuscarBotoesUpSkill()
    {
        for (int i = 0; i < 4; i++)
        {
            btnSkills[i] = GameObject.Find("LevelUpSkill" + (i + 1));
        }
    }

    void BuscarIconesCooldown()
    {
        for (int i = 0; i < 4; i++)
        {
            cooldownImagens[i] = GameObject.Find("CDSkill" + (i + 1));
        }
    }

    void BuscarIconesNivelSkills()
    {
        int countNvSkills = 0;

        for (int i = 1; i < 5 + 1; i++)
        {
            for (int j = 1; j < 5; j++)
            {
                if (countNvSkills < iconsNvSkills.Length)
                {
                    iconsNvSkills[countNvSkills] = GameObject.Find("iconNvSkill" + i + "_" + j);
                    iconsNvSkills[countNvSkills].SetActive(false);
                    countNvSkills++;
                }
            }
        }
    }

    void AtualizarNivelSkill()
    {
        #region Skill 1
        switch (ManagerXp.instance.skill1)
        {
            case 1:
                iconsNvSkills[0].SetActive(true);
                break;
            case 2:
                iconsNvSkills[1].SetActive(true);
                break;
            case 3:
                iconsNvSkills[2].SetActive(true);
                break;
            case 4:
                iconsNvSkills[3].SetActive(true);
                break;
        }
        #endregion

        #region Skill 2

        #endregion

        #region Skill 3

        #endregion

        #region Skill 4

        #endregion
    }

    void AtualizarBarraHP()
    {
        healthBar.fillAmount = player.hp * 0.01f;

        if(healthBar.fillAmount >= 1 && healthBar.fillAmount > 0.5f)
        {
            healthBar.color = Color.green;
        }

        if (healthBar.fillAmount <= 0.5f && healthBar.fillAmount > 0.3f)
        {
            healthBar.color = Color.yellow;
        }

        if (healthBar.fillAmount <= 0.3f)
        {
            healthBar.color = Color.red;
        }
    }

    void AtualizarCooldown()
    {
        if(ManagerXp.instance.skill1 >= 1)
            cooldownImagens[0].GetComponent<Image>().fillAmount = player.timerGiro / player.cooldownGiro;
        if (ManagerXp.instance.skill2 >= 1)
            cooldownImagens[1].GetComponent<Image>().fillAmount = player.timerCura / player.cooldownCura;
        if (ManagerXp.instance.skill3 >= 1)
            cooldownImagens[2].GetComponent<Image>().fillAmount = player.timerRaio / player.cooldownRaio;
        if (ManagerXp.instance.skill4 >= 1)
            cooldownImagens[3].GetComponent<Image>().fillAmount = player.timerAura / player.cooldownAura;
    }

    void HabilitarIconesSkills()
    {
        if (ManagerXp.instance.skill1 < 1)
            ToggleCooldownIcon(0, true);
        if (ManagerXp.instance.skill2 < 1)
            ToggleCooldownIcon(1, true);
        if (ManagerXp.instance.skill3 < 1)
            ToggleCooldownIcon(2, true);
        if (ManagerXp.instance.skill4 < 1)
            ToggleCooldownIcon(3, true);
    }

    public void ToggleCooldownIcon(int indice, bool value)
    {
        cooldownImagens[indice].SetActive(value);
    }

    public void activeBtn()
    {
        for (int i = 0; i < btnSkills.Length; i++)
        {
            btnSkills[i].SetActive(false);
        }

        if (ManagerXp.instance.skillPoints >= 1)
        {
            for (int i = 0; i < btnSkills.Length; i++)
            {
                btnSkills[i].SetActive(true);
            }
        }
       
    }

}
