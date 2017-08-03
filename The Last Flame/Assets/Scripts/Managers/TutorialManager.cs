using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    public Image msg_1, msg_2, msg_3;
    public bool trig_1, trig_2, trig_3;
    public Player player;
    public ManagerXp xp;
    public Enemy enemy;

    private void Awake()
    {
        Time.timeScale = 0;
    }
    void Start () {

        enemy = GameObject.FindObjectOfType<Enemy>();     
        trig_1 = true;
        trig_2 = false;
        trig_3 = false;
        msg_2.gameObject.active = false;
        msg_3.gameObject.active = false;
    }
	

	void Update () {

        if(Input.GetMouseButton(0) && trig_1)
        {
            Time.timeScale = 1;
            msg_1.gameObject.active = false;
            trig_1 = false;
        }

        if (trig_2)
        {
            if (player.colidiuEnemy == true)
            {
                //Time.timeScale = 0;
                msg_2.gameObject.active = true;

                if (Input.GetMouseButton(0) && trig_2 && enemy.clicouEnemy == true)
                {
                    Time.timeScale = 1;
                    msg_2.gameObject.active = false;
                    trig_2 = false;
                    player.colidiuEnemy = false;
                }
            }
        }
        if(trig_3)
        {
            msg_3.gameObject.active = true;

            if (Input.GetMouseButton(0) && trig_3 && xp.gastouPonto == true)
            {
                msg_3.gameObject.active = false;
                trig_3 = false;
                xp.passouNivel = false;
            }
        }

    }

}
