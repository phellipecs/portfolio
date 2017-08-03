using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBehavior : MonoBehaviour {



	void Update () {

        Destroy(this.gameObject, 2);
	}
}
