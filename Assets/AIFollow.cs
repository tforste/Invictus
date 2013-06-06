using UnityEngine;
using System.Collections;

public class AIFollow : MonoBehaviour {

    private Transform target;

    public float moveSpeed = 20;
    public float rotationSpeed = 5;
    public float playerDetection = 50;

    private CharacterController enemyControl;
    private float lastDist;
    private Vector3 dir;

    private bool isWalking = false;

	// Use this for initialization
	void Start () {
        target = GameObject.FindWithTag("Player").transform;
        enemyControl = GetComponent<CharacterController>();

        lastDist = Vector3.Distance(transform.position, target.position);

        animation.CrossFade("waitingforbattle");
	}
	
	// Update is called once per frame
	void Update () {
        //float hitdist = 0.0f;
        lastDist = Vector3.Distance(transform.position, target.position);


        if(lastDist <= playerDetection)
        {
            dir = target.position - transform.position;
            dir.y = 0; //keep only the horizontal direction
            transform.rotation = Quaternion.LookRotation(dir);

            //Always try to move towards the player's position
            enemyControl.SimpleMove(dir.normalized * moveSpeed);

            isWalking = true;
        }
        else if (lastDist > playerDetection)
        {
            isWalking = false;
        }

        if(lastDist < 8)
        {
            animation.CrossFade("attack");
        }
        else if (isWalking)
        {
            animation.CrossFade("run");
        }
        else
        {
            animation.CrossFade("waitingforbattle");
        }

	}
}
