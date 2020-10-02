using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public GameObject player;

    public float mouseSensitivity;
    public float CameraMoveSpeed = 120;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float step = CameraMoveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position,player.transform.position,step);

        transform.Rotate(new Vector3(0, Input.GetAxis("HorizontalRightJoy"), 0));

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0));

    }
}
