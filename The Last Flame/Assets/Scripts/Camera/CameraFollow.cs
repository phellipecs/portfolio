using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //[HideInInspector]
    public Player player;
    public float offsetPosition = 20f;
    public bool lockInPlayer = true;
    public float zoomMax, zoomMin;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (lockInPlayer)
        {
            if (player)
            {
                LockPlayer();
            }
        }
        else
        {
            MoveCameraWithMouse();
        }

        Zoom();

        if (Input.GetKeyDown(KeyCode.Y))
        {
            lockInPlayer = !lockInPlayer;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            LockPlayer();
        }
    }

    void Zoom()
    {
        Vector2 scrollMouse = Input.mouseScrollDelta;

        offsetPosition += -scrollMouse.y;
        offsetPosition = Mathf.Clamp(offsetPosition, zoomMin, zoomMax);
    }

    public void LockPlayer()
    {
        transform.position = new Vector3(player.transform.position.x - offsetPosition, player.transform.position.y + offsetPosition * 2f, player.transform.position.z - offsetPosition);
    }

    void MoveCameraWithMouse()
    {
        float mouseX = Input.mousePosition.x / Screen.width;
        float mouseY = Input.mousePosition.y / Screen.height;
        float velMouse = 1f;
        Vector2 mousePos = new Vector2(mouseX, mouseY);
        Vector3 mouseMove = new Vector2();

        if(mousePos.x >= 0.9f)
            mouseMove = new Vector3(velMouse, 0f, 0f);

        if (mousePos.x <= 0.1f)
            mouseMove = new Vector3(-velMouse, 0f, 0f);

        if (mousePos.y >= 0.9f)
            mouseMove = new Vector3(0f, 0f, velMouse);

        if (mousePos.y <= 0.1f)
            mouseMove = new Vector3(0f, 0f, -velMouse);

        transform.position += mouseMove;
    }

}
