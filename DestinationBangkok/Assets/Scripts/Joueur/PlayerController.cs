using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRB;
    public Animator playerAnim;

    public GameObject camPivot;

    bool raycastTouched = false;
    public bool closeToGround;

    
    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
        playerRB = gameObject.GetComponent<Rigidbody>();
    }

  
    void Update()
    {
        //Besoin du Update normal, pour le getButtonDown
        Jump();

    }

    private void FixedUpdate()
    {
        
        Movement();

        RaycastHit playerFeet;

        if (Physics.Raycast(transform.position, Vector3.down, out playerFeet, 5))
        {
            print("Raycast hit :" + playerFeet.collider.gameObject.layer);

            closeToGround = true;
            if (playerAnim.GetBool("CloseToGround") == false && !isGrounded && playerRB.velocity.y < 0)
            {
                playerAnim.SetBool("CloseToGround", true);
            }

        }
        else
        {
            closeToGround = false;
        }

        Debug.DrawLine(transform.position, transform.position + (Vector3.down * 5), Color.red);


    }

 

    [Header("Mouvement du Joueur")]
    Vector3 playerVelocityMod;
    float persoAngleY = 0;

    /**
     * 
     * 
     */
    Vector3 moveDirection;
    private void Movement()
    {

        if (playerRB.velocity.y < 5)
        {
            if (playerAnim.GetBool("isFalling") == false)
            {
                playerAnim.SetBool("isFalling", true);
            }

        }

        //En gros, on veut ignorer la rotation en X pour la direction du mouvement du joueur
        Transform dirCam = camPivot.transform;
        dirCam.rotation = Quaternion.Euler(0, camPivot.transform.eulerAngles.y, camPivot.transform.eulerAngles.z);

        //dirCam.rotation = rotCam;

        //Wooow le calcul qui me pete le cerveau
        moveDirection = (Input.GetAxis("Vertical") * dirCam.transform.forward * playerVelocityMod.z) + (Input.GetAxis("Horizontal") * dirCam.transform.right * playerVelocityMod.x);

        var newVelocity = playerRB.velocity;
        newVelocity.x = moveDirection.x;
        newVelocity.z = moveDirection.z;

        playerRB.velocity = newVelocity;

        if (playerRB.velocity.x == 0 && playerRB.velocity.z == 0)
        {
            playerAnim.SetBool("isRunning", false);
        }
        else
        {
            persoAngleY = Mathf.Rad2Deg * Mathf.Atan2(playerRB.velocity.x, playerRB.velocity.z);
           

            playerRB.rotation = Quaternion.Euler(0, persoAngleY, 0);

            playerAnim.SetBool("isRunning", true);

         
           
        }


        

    }

    public float vitesseDeChute;
    //public float vitesseDeChuteMax;
    public bool isGrounded;
    public float jumpPower;
    /**
    * Fonction gérant le saut : force du saut, input et quand est-ce que l'on peut sauter
    * 
    */

    void Jump()
    {
        

        if (isGrounded)
        {

            raycastTouched = false;
            playerAnim.SetBool("isFalling", false);
            playerAnim.SetBool("CloseToGround", false);

            //Peut sauter
            //Saut
            if (Input.GetButtonDown("Jump"))
            {
                print("pressed A");
               

                playerRB.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);

                playerAnim.SetTrigger("Jumps");
                


            }

        }
        else
        {
            playerRB.AddForce(new Vector3(0, -vitesseDeChute * Time.deltaTime, 0));
        }

        

        
        
    }

    private void OnTriggerEnter(Collider colEnter)
    {
       
        if (colEnter.gameObject.layer == 8)
        {
            // La couche #8 est le sol
            //Touche le sol (peut sauter)
            isGrounded = true;
            
            playerVelocityMod = new Vector3(10, 0, 10);

        }
       
    }

    private void OnTriggerExit(Collider colExit)
    {
        if (colExit.gameObject.layer == 8)
        {
            // La couche #8 est le sol
            //Touche le sol (peut sauter)
            isGrounded = false;
            playerVelocityMod = new Vector3(4, 0, 4);

        }
    }

    void OnAnimatorIK()
    {
        print("OnAnimIk");
       
    }
}
