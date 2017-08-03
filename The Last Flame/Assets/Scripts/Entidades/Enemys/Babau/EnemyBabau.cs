using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBabau : Enemy {


    public override void Die()
    {
        if(nivel == 3)
        {
            Invoke("LevelManager.instance.LoadScene('GameOver')", 3f);
        }

        base.Die();
    }
}
