using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDretch : Enemy {

    public float timerAterrorizado;
    public int chanceAterrorizar;
    private float contador;
    private float contadorLimite = 10;


    public void Update()
    {
        //Aterrorizar();
        Contar();

        base.Update();
    }


    public void Contar()
    {
        if (contador <= contadorLimite)
        {
            contador += Time.deltaTime;
        }
        else if (contador > contadorLimite)
        {
            chanceAterrorizar = Random.Range(0, 100);
            contador = 0;
        }
    }

    public void Aterrorizar()
    {
        if (chanceAterrorizar >= 90)
        {
            Debug.Log("SILENCE!");
            player.silence = true;
            timerAterrorizado += Time.deltaTime;

            if (timerAterrorizado >= 2f)
            {
                player.silence = false;
                timerAterrorizado = 0;
            }
        }

    }

}
