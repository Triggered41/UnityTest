using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public Vector2 mouseSensitivity;
    public Rigidbody rb;
    public Camera cam;
    public Transform camHolder;
    public Transform originalCamPos;
    public Transform groundLoc;
    public Transform viz;
    public Animator anim;
    public PointsManager pointsManager;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Movement();
        CameraLook();
        CameraPos();

    }

    void Update(){
        anim.SetBool("inAir", true); //by default assume in air

        if (checkGround()){
            anim.SetBool("inAir", false); //Set grounded(not in air) if On Ground
            
            // If grounded and Jump button is pressed, Jump
            if(Input.GetButtonDown("Jump")){
                Jump();
            }
        }        
    }

    // Handling the movement of the player
    void Movement(){
        float X = Input.GetAxisRaw("Vertical")*speed*Time.fixedDeltaTime;
        float Y = Input.GetAxisRaw("Horizontal")*speed*Time.fixedDeltaTime;
        rb.MovePosition(transform.position + rb.transform.forward*X + rb.transform.right*Y);
        anim.SetBool("movement", (X+Y)!=0f);
    }

    // Controlling the camera with mouse
    void CameraLook(){
        float mouseX = -Input.GetAxis("Mouse Y") * mouseSensitivity.y;
        float mouseY = Input.GetAxis("Mouse X") * mouseSensitivity.x;
    
        camHolder.Rotate(mouseX, 0f, 0f);
        // rb.transform.Rotate(0f, mouseY, 0f, 0f);
        transform.RotateAround(transform.position, transform.up, mouseY);

        // Rotate the Hologram to always point to the last direction
        viz.RotateAround(transform.position, transform.up, -mouseY);
    }

    // Correct camera position when back is towards an object (Wall etc.)
    void CameraPos(){
        Vector3 dir = originalCamPos.transform.position-camHolder.position;
        RaycastHit hitObj;
        bool hit = Physics.Raycast(camHolder.position, dir.normalized, out hitObj, dir.magnitude*1.25f);
        if (hit){
            cam.transform.position = hitObj.point-dir.normalized*0.25f;
        }else{
            cam.transform.position = Vector3.Lerp(cam.transform.position, originalCamPos.transform.position, 0.5f);
        }
    }

    // Player Jump
    void Jump(){
        float jForce = jumpForce;
        if(rb.velocity.y < 0){
            jForce = jumpForce-rb.velocity.y;
        }
        rb.AddForce(transform.up*jForce, ForceMode.Impulse);
    }
    bool checkGround(){
        int groundLayer = LayerMask.GetMask("Ground");
        return Physics.CheckSphere(groundLoc.position, 0.5f, groundLayer);
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "Points"){
            pointsManager.ConsumePoint(other.gameObject);
        }else{
            SceneManager.LoadScene("GameOver");
        }
    }

}
