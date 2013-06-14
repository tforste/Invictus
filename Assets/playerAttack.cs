using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

    public float playerAttackRange = 8;
    public int playerAttackDamage = 10;
    public float attackSpeed = 2; //Seconds Per Attack

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    //Execute the player attack. Recieves a float (Distance the player is from the target) to compare with the attack Range before executing the attack
    public void Attack( GameObject target )
    {
        EnemyHealth eh = target.GetComponent<EnemyHealth>();

        eh.AdjustCurrHealth(-playerAttackDamage);

        Debug.Log(eh.CurrHealth);
    }
}
