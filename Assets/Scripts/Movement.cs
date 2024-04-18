using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Movement : MonoBehaviour
{
    //Essentials
    public Transform mainCam;
    CharacterController controller;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //Animation
    public Animator PlayerAnimator;

    //Movement
    Vector2 movement;
    public float walkSpeed = 4;
    public float sprintSpeed = 6;
    float trueSpeed;

    //Jumping
    //public float jumpHeight = 1;
    public float gravity = (int)9.82;
    bool isGrounded;
    Vector3 velocity;
    private int JumpAmount = 1;
    private int counter;


    //Conversation manager

    //public AudioSource Convo1;
    //public AudioSource Convo2;
    //public AudioSource Convo3;
    

    //IEnumerator FinishCut()
    //{
    //    // Check if all followers are acquired
    //    if (PlayerPrefs.GetInt("TurtleFollow") == 1 && PlayerPrefs.GetInt("SnakeFollow") == 1 && PlayerPrefs.GetInt("HorseFollow") == 1)
    //    {
    //        Debug.Log("All followers acquired!");
    //        yield return new WaitForSeconds(3);
    //        SceneManager.LoadScene(11);
    //    }
    //}
    void Start()
    {
        Animator animator;
        animator = GetComponent<Animator>();

        //No cursor & Move
        trueSpeed = walkSpeed;
        controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;

        //StartCoroutine(FinishCut());

        //Grounding
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1;
        }


        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;

        //Set rotation equal to the look direction
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * trueSpeed * Time.deltaTime);
        }

        //Sprinting
        if ((Input.GetKeyDown(KeyCode.LeftShift)))
        {
            trueSpeed = sprintSpeed;
        }
        if ((Input.GetKeyUp(KeyCode.LeftShift)))
        {
            trueSpeed = walkSpeed;
        }

        //Jumping (max doublejump)
        if (isGrounded)
        {
            counter = JumpAmount;
        }

        //if (Input.GetKeyDown(KeyCode.Space) && counter > 0)
        //{
         //   velocity.y = Mathf.Sqrt((jumpHeight * 10) * -2f * gravity);
         //   counter = counter - 1;
       // }


        if (velocity.y > -20)
        {
            velocity.y += (gravity * 30) * Time.deltaTime;
        }
        
        controller.Move(velocity * Time.deltaTime);

        //        //Animation
                if (direction.magnitude <= 0)
                {
                    PlayerAnimator.SetBool("isMoving", false);
                }
                else
                {
                    PlayerAnimator.SetBool("isMoving", true);
                }

            }



        //if (Convo1.isPlaying || Convo2.isPlaying || Convo3.isPlaying)
        //{
            // If any of the convos  are playing, stop the other convos
            //if (Convo1.isPlaying)
            //{
            //    Convo2.Stop();
            //    Convo3.Stop();
            //}
            //else if (Convo2.isPlaying)
            //{
            //    Convo1.Stop();
            //    Convo3.Stop();
            //}
            //else if (Convo3.isPlaying)
            //{
            //    Convo1.Stop();
            //    Convo2.Stop();
            //}

          
        //}

}

