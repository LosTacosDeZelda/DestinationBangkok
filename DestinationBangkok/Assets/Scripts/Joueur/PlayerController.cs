using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Auteurs : Raph, Hao et François
public class PlayerController : MonoBehaviour
{
  public Rigidbody playerRB;
  public Animator playerAnim;

  public GameObject camPivot;

  public AudioSource marcheSol;
  public AudioClip marche;

  [Header("Gestion du Saut")]
  public bool closeToGround;

  public GestionStatus refGestionStatus;

  public float monDelai = 1.5f;
  public bool estMort = false;


  void Start()
  {

    playerAnim = gameObject.GetComponent<Animator>();
    playerRB = gameObject.GetComponent<Rigidbody>();

    marcheSol = GetComponent<AudioSource>();
  }


  void Update()
  {
        if (estMort == false)
        {
            //Besoin du Update normal, pour le getButtonDown
            Jump();
        }
    
        if (monDelai > 0) { monDelai -= Time.deltaTime; }
  }

  public float longueurRaycast;
  public RaycastHit solPres;
  public RaycastHit infoDecal;
  private void FixedUpdate()
  {
        if (estMort == false)
        {
            Movement();
        }
        else
        {
            playerRB.velocity = Vector3.zero;
        }
    

    //Raycast pour savoir si le joueur est proche du sol
    if (Physics.Raycast(transform.position, Vector3.down, out solPres, longueurRaycast))
    {

      closeToGround = true;
      if (playerAnim.GetBool("procheDuSol") == false && !isGrounded && playerRB.velocity.y < 0)
      {
        playerAnim.SetBool("procheDuSol", true);
      }

    }
    else
    {
      closeToGround = false;
    }

    if (Physics.Raycast(transform.position, Vector3.down, out infoDecal, 100))
    {

    }

    Debug.DrawLine(transform.position, transform.position + (Vector3.down * 100), Color.red);


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
      if (playerAnim.GetBool("tombe") == false)
      {
        playerAnim.SetBool("tombe", true);
      }

    }


    //En gros, on veut ignorer la rotation en X pour la direction du mouvement du joueur
    Transform dirCam = camPivot.transform;
    dirCam.rotation = Quaternion.Euler(0, camPivot.transform.eulerAngles.y, camPivot.transform.eulerAngles.z);


    //Wooow le calcul qui me pete le cerveau
    moveDirection = (Input.GetAxis("Vertical") * dirCam.transform.forward * playerVelocityMod.z) + (Input.GetAxis("Horizontal") * dirCam.transform.right * playerVelocityMod.x);

    var newVelocity = playerRB.velocity;
    newVelocity.x = moveDirection.x;
    newVelocity.z = moveDirection.z;

    playerRB.velocity = newVelocity;

    if (playerRB.velocity.x == 0 && playerRB.velocity.z == 0)
    {
      playerAnim.SetBool("court", false);
    }
    else
    {
      persoAngleY = Mathf.Rad2Deg * Mathf.Atan2(playerRB.velocity.x, playerRB.velocity.z);


      playerRB.rotation = Quaternion.Euler(0, persoAngleY, 0);

      playerAnim.SetBool("court", true);

      marcheSol.PlayOneShot(marche, 0.7F);

    }

    //Animation de course accélère progressivement
    playerAnim.SetFloat("multiplicateurVitesse", Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical")));




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


      playerAnim.SetBool("tombe", false);
      playerAnim.SetBool("procheDuSol", false);

      //Peut sauter
      //Saut
      if (Input.GetButtonDown("Jump"))
      {
        print("pressed A");


        playerRB.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);

        playerAnim.SetTrigger("saute");


      }
    }
    else
    {
      playerRB.AddForce(new Vector3(0, -vitesseDeChute * Time.deltaTime, 0));
    }





  }


  private void OnTriggerEnter(Collider colEnter)
  {

    if (colEnter.gameObject.tag == "Sol")
    {
      // La couche #8 est le sol
      //Touche le sol (peut sauter)
      isGrounded = true;

      playerVelocityMod = new Vector3(10, 0, 10);




    }

  }


  private void OnTriggerExit(Collider colExit)
  {
    if (colExit.gameObject.tag == "Sol")
    {
      // La couche #8 est le sol
      //Ne touche plus le sol (ne peut pas sauter)
      isGrounded = false;
      playerVelocityMod = new Vector3(6, 0, 6);



    }
  }

  public void JoueurBlesse(string typePiege)
  {
    if (monDelai <= 0)
    {
      switch (typePiege)
      {
        case "Flamme":
          //Applique leffet Flamme (brûlé)
          refGestionStatus.listeCompteStatus["Flamme"]++;
          print(refGestionStatus.listeCompteStatus["Flamme"]);
          refGestionStatus.MettreAJourTexteFeu();
          // ajouter delai ici

          if (refGestionStatus.listeCompteStatus["Flamme"] == 3)
          {
                //Démarrer Séquence de mort
                SequenceDeMort();
          }
          break;

        case "Perforation":
          //Applique leffet perforation (saignements)
          refGestionStatus.listeCompteStatus["Perforation"]++;
          print(refGestionStatus.listeCompteStatus["Perforation"]);
          refGestionStatus.MettreAJourTexteFeu();
          if (refGestionStatus.listeCompteStatus["Perforation"] == 3)
          {
                //Démarrer Séquence de mort
                SequenceDeMort();
          }
          break;

        case "Poison":
          //Applique leffet poison (empoisonné)
          refGestionStatus.listeCompteStatus["Poison"]++;
          print(refGestionStatus.listeCompteStatus["Poison"]);
          refGestionStatus.MettreAJourTexteFeu();
          if (refGestionStatus.listeCompteStatus["Poison"] == 3)
          {
                //Démarrer Séquence de mort
                SequenceDeMort();
          }
          break;
        default:
          break;
      }
      monDelai = 1.5f;
    }
    print(typePiege);
  }

  void SequenceDeMort()
  {
        //Désactiver l
        estMort = true;
        //Animation de mort du joueur
        playerAnim.SetBool("estMort", estMort);

        //Changement de cam et animation de cam

        //Animation de l'effet vignette (post-processing)
  }



}