using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRB;
    public Animator playerAnim;

    public GameObject camRig;
    

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
        playerRB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Besoin du Update normal, pour le getButtonDown
        Jump();
        print(playerVelocityMod);

    }

    private void FixedUpdate()
    {
        
        Movement();


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

        //Wooow le calcul qui me pete le cerveau
        moveDirection = (Input.GetAxis("Vertical") * camRig.transform.forward * playerVelocityMod.z) + (Input.GetAxis("Horizontal") * camRig.transform.right * playerVelocityMod.x);

        var newVelocity = playerRB.velocity;
        newVelocity.x = moveDirection.x;
        newVelocity.z = moveDirection.z;

        playerRB.velocity = newVelocity;
        //playerRB.AddRelativeForce(moveDirection, ForceMode.Force);
        

       

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

    float vitesseDeChute = 0;
    public float vitesseDeChuteMax;
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
            //Ne peut pas sauter
            //Ne Touche rien

            vitesseDeChute += Time.deltaTime / 2;

            if (vitesseDeChute > vitesseDeChuteMax)
            {
                vitesseDeChute = vitesseDeChuteMax;
            }

            playerRB.AddForce(new Vector3(0, -vitesseDeChuteMax, 0));


        }

        

        
        
    }

    private void OnCollisionEnter(Collision colEnter)
    {
       
        if (colEnter.collider.gameObject.layer == 8)
        {
            // La couche #8 est le sol
            //Touche le sol (peut sauter)
            isGrounded = true;
            vitesseDeChute = 0;
            playerVelocityMod = new Vector3(10, 0, 10);

        }
       
    }

    private void OnCollisionExit(Collision colExit)
    {
        if (colExit.collider.gameObject.layer == 8)
        {
            // La couche #8 est le sol
            //Touche le sol (peut sauter)
            isGrounded = false;
            playerVelocityMod = new Vector3(5, 0, 5);

        }
    }

    void OnAnimatorIK()
    {
        print("OnAnimIk");
       
    }
}
