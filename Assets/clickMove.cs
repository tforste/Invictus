using UnityEngine;
using System.Collections;

public class clickMove : MonoBehaviour {

    public float speed = 0.5f;

    private Vector3 targetPosition; //current target position
    private CharacterController character;
    private float lastDist;
    private bool isWalking;
    private bool attackMode;
    private RaycastHit hit;

    private Vector3 dir;

    //Variables for attacking
    public GameObject target;
    private Vector3 targetPos;
    private float distToTarget;
    playerAttack playerAttack;

    void Start()
    {
        character = GetComponent<CharacterController>();
        playerAttack = GetComponent<playerAttack>();
        targetPosition = transform.position;

        //tag = "no tag selected";
    }

    void Update()
    {
        if (Input.GetKeyUp("a") )
        {
          animation.CrossFade("die");
        }

        if (Input.GetMouseButtonDown(0))
        {
            //create a logical horizontal plane at the player position
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0.0f;

            //find point clicked in the plane (if any)
            if (playerPlane.Raycast(ray, out hitdist))
            {
                //update targetPosition to the clicked point
                targetPosition = ray.GetPoint(hitdist);
                dir = targetPosition - transform.position;
                dir.y = 0; //keep only the horizontal direction
                transform.rotation = Quaternion.LookRotation(dir);

                lastDist = Vector3.Distance(transform.position, targetPosition);
                isWalking = true;
            }

            //Detect if the player clicked on an enemy
            if (Physics.Raycast(ray, out hit))
            {
                tag = hit.collider.gameObject.tag;

                //Debug.Log(tag);

                if (hit.collider.gameObject.tag != "Untagged")
                {
                    if (hit.collider.gameObject.tag == "Finish")
                    {
                        target = hit.collider.gameObject;
                        InvokeRepeating("attackIfClose", 0, playerAttack.attackSpeed);
                        //checkDead(target);
                        //attackIfClose( ); //attack the enemy if hes within attack range
                    }
                    else
                    {
                        //Debug.Log("NOTHING BRO");
                        attackMode = false;
                    }

                }
                else
                {
                    CancelInvoke("attackIfClose");
                    attackMode = false;
                }
            }

        }

        // always try to move the character to targetPosition:
        if (isWalking)
        {
            animation.CrossFade("walk");
            character.SimpleMove(dir.normalized * speed); // move taking gravity into account

            float distance = Vector3.Distance(transform.position, targetPosition);

            if (distance > lastDist)
            {
                isWalking = false;
                animation.CrossFade("idle");
            }
            else
            {
                lastDist = distance;
            }
        }

        //Handle the Attacking animation.
        if (attackMode)
        {
            animation.CrossFade("attack");
            //attackMode = false;
        }

    }

    //Recieves a game object (the target)
    //Checks the playe distance from the target, if it's within the attack range, attack
    private void attackIfClose( )
    {
        targetPos = target.transform.position;

        distToTarget = Vector3.Distance(transform.position, targetPos);

        if (distToTarget <= playerAttack.playerAttackRange)
        {
            attackMode = true;
            playerAttack.Attack( target );
        }

        //Debug.Log(distToTarget);
    }

   // void checkDead( GameObject target )
    //{
        //Dont attack if hes dead!
     //   EnemyHealth eh = target.GetComponent<EnemyHealth>();

   //     if (eh.Dead)
    //   {
   //         CancelInvoke("attackIfClose");
  //          attackMode = false;
  //      }
   // }

}
