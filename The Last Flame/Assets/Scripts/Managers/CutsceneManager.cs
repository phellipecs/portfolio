using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour {

    public Camera camera;
    public Animator cameraAnim;
    public EnemyManager enemyPortal;
    public AudioSource teleporte, portal;
    public GameObject luz, rochelLeave, rochel, rochelPosition;
    public bool trigger_1, trigger_2, trigger_3, trigger_4;
    public Image msg_1, msg_2, msg_3, msg_4;

	
	void Start () {

        msg_1.gameObject.active = false;
        msg_2.gameObject.active = false;
        msg_3.gameObject.active = false;
        msg_4.gameObject.active = false;
    }

	void Update () {

		if(Input.GetMouseButtonDown(0) && trigger_1)
        {   
            this.camera.transform.position = new Vector3(9.56f, 4.35f, 0);
            this.camera.transform.eulerAngles = new Vector3(24.332f, 368.159f, -6.213f);
            msg_2.gameObject.active = true;
            msg_1.gameObject.active = false;
            trigger_2 = true;
            trigger_1 = false;
        }
        else if (Input.GetMouseButtonDown(0) && trigger_2)
        {
            this.camera.transform.position = new Vector3(10.1f, 4.3f, -0.1f);
            this.camera.transform.eulerAngles = new Vector3(20.234f, 177.047f, 0.659f);
            msg_3.gameObject.active = true;
            msg_2.gameObject.active = false;
            trigger_2 = false;
            trigger_3 = true;
        }
        else if (Input.GetMouseButtonDown(0) && trigger_3)
        {
            cameraAnim.enabled = true;
            cameraAnim.SetTrigger("Portal");
            Instantiate(rochelLeave, rochel.transform.position, rochel.transform.rotation);
            teleporte.Play();
            msg_3.gameObject.active = false;
            trigger_3 = false;
            rochel.active = false;
            luz.active = true;
        }

        else if (Input.GetMouseButtonDown(0) && trigger_4)
        {
            trigger_4 = false;
            msg_4.gameObject.active = false;
            cameraAnim.SetTrigger("Portal");
            Application.LoadLevel(1);
        }
    }
    public void ComecouCutscene()
    {
        msg_1.gameObject.active = true;
        trigger_1 = true;
        this.camera.transform.position = new Vector3(10.1f, 4.3f, -0.1f);
        this.camera.transform.eulerAngles = new Vector3(20.234f, 177.047f, 0.659f);
        this.cameraAnim.enabled = false;
    }
    public void PortalCutscene()
    {
        msg_4.gameObject.active = true;
        enemyPortal.gameObject.active = true;
        msg_4.gameObject.active = true;
        trigger_4 = true;
        portal.Play();
    }
}
