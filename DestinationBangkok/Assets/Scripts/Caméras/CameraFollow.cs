using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float cameraMoveSpeed = 120f;
    public GameObject cameraFollowObject;
    public float clampAngleMin;
    public float clampAngleMax;
    public float inputSensitivity = 150f;
    public GameObject cameraObj;
    public GameObject playerObj;
    public float camDistanceXToPlayer;
    public float camDistanceYToPlayer;
    public float camDistanceZToPlayer;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    float rotY = 0f;
    float rotX = 0f;




    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("HorizontalRightJoy");
        float inputZ = Input.GetAxis("VerticalRightJoy");

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;

        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX += finalInputZ * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngleMin, clampAngleMax);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0f);

        transform.rotation = localRotation;


    }

    private void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        Transform target = cameraFollowObject.transform;

        //Bouger vers la cible
        float step = cameraMoveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
