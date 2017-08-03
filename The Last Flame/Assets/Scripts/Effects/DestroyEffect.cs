using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour {

    public float timeToDestroy;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

}
