using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRB;
    public Animator playerAnim;
    

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
        playerRB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerGroundRaycast();
        Jump();
    }

    private void FixedUpdate()
    {
        Movement();
       
    }


    [Header("Mouvement du Joueur")]
    [SerializeField]
    Vector3 playerVelocityMod;
    float persoAngleY = 0;
    /**
     * 
     * 
     */
    private void Movement()
    {
        playerRB.velocity = new Vector3(Input.GetAxis("Horizontal") * playerVelocityMod.x, playerVelocityMod.y, Input.GetAxis("Vertical") * playerVelocityMod.z);

        if (playerRB.velocity.x == 0 && playerRB.velocity.z == 0)
        {
            playerAnim.SetBool("isRunning", false);
        }
        else
        {
            persoAngleY = Mathf.Rad2Deg * Mathf.Atan2(playerRB.velocity.x, playerRB.velocity.z);
            print(persoAngleY);

            playerRB.rotation = Quaternion.Euler(0, persoAngleY, 0);

            playerAnim.SetBool("isRunning", true);

            //transform.forward
        }

        


    }


    public bool isGrounded;
    /**
    * 
    * 
    */
    void Jump()
    {
        if (isGrounded)
        {
            //Peut sauter
            //Saut
        }
        else
        {
            //Ne peut pas sauter
            //Ne Touche rien
            isGrounded = false;

            vitesseDeChute += Time.deltaTime / 2;

            if (vitesseDeChute > vitesseDeChuteMax)
            {
                vitesseDeChute = vitesseDeChuteMax;
            }

            playerRB.AddForce(new Vector3(0, -vitesseDeChuteMax, 0));
        }

        
        
    }


    public float vitesseDeChuteMax;
    float vitesseDeChute = 0;
    void PlayerGroundRaycast()
    {

        RaycastHit enDessous;

        if (Physics.Raycast(gameObject.transform.position,Vector3.down,out enDessous, 0.1f))
        {


            if (enDessous.collider.gameObject.layer == 8)
            {

                //La couche #8 est le sol
                //Touche le sol (peut sauter)
                isGrounded = true;
                vitesseDeChute = 0;

            }


        }
        else
        {
            isGrounded = false;
        }
            //Touche qqch
       

        //Visualiser le raycast
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0, -0.1f, 0));
    }

    void OnAnimatorIK()
    {
        print("OnAnimIk");
       
    }
}
