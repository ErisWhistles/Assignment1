
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public ParticleSystem jumpParticle;
    public static int jumps;
    public Rigidbody rb;
    private Vector3 respawnPoint;
    public Vector3 gravity;
    public Vector3 playerVelocity;
    public bool groundedPlayer;
    public float mouseSensitivy = 5.0f;
    private float jumpHeight = 2.0f;
    private float gravityValue = -9.81f;
    private CharacterController controller;
    private Animator animator;
    public Camera playerCam;
    private float walkSpeed = 5;
    private float runSpeed = 8; 
    public bool canJump;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        jumps = 1;
        respawnPoint = new Vector3(0.1f,3.5f,-0.3f);
    }

    void Update()
    {
        UpdateRotation();
        ProcessMovement();
    }

    void UpdateRotation()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X")* mouseSensitivy, 0, Space.Self);
    }
    void ProcessMovement()
    { 
        // Moving the character foward according to the speed
        float speed = GetMovementSpeed();

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Making sure we dont have a Y velocity if we are grounded
        // controller.isGrounded tells you if a character is grounded ( IE Touches the ground)
        groundedPlayer = controller.isGrounded;
        animator.SetBool("isGrounded", groundedPlayer); 
        if(groundedPlayer) canJump = true;

        gravity.y += gravityValue * Time.deltaTime;

        if(Input.GetButtonDown("Jump")) {
            if (groundedPlayer) {
                animator.SetTrigger("jump");
                gravity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

            } else {
                if (jumps > 0 && canJump) {
                    jumps--;
                    canJump = false;
                    animator.SetTrigger("doublejump");
                    Instantiate(jumpParticle, transform.position, Quaternion.identity);
                    gravity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                }
            }
        }
        else {
            if (groundedPlayer) gravity.y = -1.0f;
        }

        Vector3 movement = move.z *transform.forward  + move.x * transform.right;

        var moving = true;
        if(movement.magnitude == 0){
            moving = false;
        }
        animator.SetBool("moving", moving);

        playerVelocity = gravity * Time.deltaTime + movement * Time.deltaTime * speed;
        controller.Move(playerVelocity);
    }

    float GetMovementSpeed()
    {
        if (Input.GetButton("Fire3"))// Left shift
        {
            animator.SetBool("running", true);
            return runSpeed;
        }
        else
        {
            animator.SetBool("running", false);
            return walkSpeed;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy"){
            RespawnPlayer();
        }
        if(other.tag == "Respawn"){
            controller.enabled = false;
            transform.position = respawnPoint;
            controller.enabled = true;
        }

        if(other.tag == "NextLevel"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
    public static void RespawnPlayer()
    {
    if(SceneManager.GetActiveScene().buildIndex == 2){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    else {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
    }
    }
}
