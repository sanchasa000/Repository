using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballController : MonoBehaviour
{
    public float speed = 7.0f;
    public int size = 100;

    private int maxVelocity = 14;

    [HideInInspector] public float mouseX;

    private GameObject player;
    private Transform playerTransform;
    private Rigidbody snowballRb;
    private CharacterController controller;

    private GameObject camera;

    [HideInInspector] public Vector3 moveDirection;

    private bool clicked = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player Transform");
        snowballRb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();

        camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            clicked = true;
        }
        else if (Input.GetMouseButtonUp(0)) {
            clicked = false;
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            ResetPosition();
        }

        MovePlayer();
        VelocityController();


        //Debug.Log(mouseX);
        //Debug.Log(moveDirection);
    }

    void LateUpdate()
    {
        transform.localScale = new Vector3((size + 100), (size + 100), (size + 100));
    }


    public void ResetPosition()
    {
        snowballRb.constraints = (RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY);
        snowballRb.velocity = new Vector3(0, snowballRb.velocity.y, 0);
        snowballRb.constraints = RigidbodyConstraints.None;
    }

    public void MovePlayer()
    {
        //This code gets the mouse x input
        mouseX = Input.GetAxis("Mouse X");

        if (clicked && mouseX > 0.1f) {
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
        /*else if (clicked && mouseX < -0.1f) {
            transform.Translate(-moveDirection * speed * Time.deltaTime);
        } */

        if (mouseX >= 0.9 | mouseX <= -0.9) {
            clicked = false;
        }
    }

    public void VelocityController()
    {
        if (snowballRb.velocity.x > maxVelocity) {
            snowballRb.velocity = new Vector3(maxVelocity, snowballRb.velocity.y, snowballRb.velocity.z);
        }
        else if (snowballRb.velocity.x < -maxVelocity) {
            snowballRb.velocity = new Vector3(-maxVelocity, snowballRb.velocity.y, snowballRb.velocity.z);
        }

        if (snowballRb.velocity.y > maxVelocity) {
            snowballRb.velocity = new Vector3(snowballRb.velocity.x, maxVelocity, snowballRb.velocity.z);
        }
        else if (snowballRb.velocity.y < -maxVelocity) {
            snowballRb.velocity = new Vector3(snowballRb.velocity.x, -maxVelocity, snowballRb.velocity.z);
        }

        if (snowballRb.velocity.z > maxVelocity) {
            snowballRb.velocity = new Vector3(snowballRb.velocity.x, snowballRb.velocity.y, maxVelocity);
        }
        else if (snowballRb.velocity.z < -maxVelocity) {
            snowballRb.velocity = new Vector3(snowballRb.velocity.x, snowballRb.velocity.y, -maxVelocity);
        }
    }
}

