using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraMovement : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 10;
    private Vector3 moveVector;
    private float sensitivity = 5f;
    private float rotationX = 0;
    private float rotationY = 0;
    private Quaternion rot;

    [SerializeField] private TMP_Text speedDisplay;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;

        if (Input.GetKey(KeyCode.W))
            moveVector = transform.forward * speed;

        if (Input.GetKey(KeyCode.S))
            moveVector = -transform.forward * speed;

        if (Input.GetKey(KeyCode.D))
            moveVector = transform.right * speed;

        if (Input.GetKey(KeyCode.A))
            moveVector = -transform.right * speed;

        if (Input.GetKey(KeyCode.Space))
            moveVector.y++;

        if (Input.GetKey(KeyCode.LeftShift))
            moveVector.y--;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            speed = Mathf.Min(++speed, 50);
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            speed = Mathf.Max(--speed, 1f);

        speedDisplay.text = "Speed: " + speed.ToString();
    }
    private void FixedUpdate()
    {
        moveVector *= Time.fixedDeltaTime;
        controller.Move(moveVector);

        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

        transform.localRotation = rot * xQuaternion * yQuaternion;
    }

    public void SetPosition()
    {
        controller.Move(new Vector3(0, 1, -10) - transform.position);
        moveVector = new Vector3(0,0,0);
    }
}
