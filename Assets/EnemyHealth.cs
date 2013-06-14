using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int MaxHealth = 100; //This is the Beginning health for the enemy
    public int CurrHealth;
    //public bool Dead = false;

    AIFollow AI;

	// Use this for initialization
	void Start () {
        CurrHealth = MaxHealth;

        AI = GetComponent<AIFollow>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    //Adjusts the current health by the number passed in
    public void AdjustCurrHealth(int adjust)
    {
        CurrHealth += adjust;

        if (CurrHealth < 0)
        {
            CurrHealth = 0;
        }

        if (CurrHealth > MaxHealth)
        {
            CurrHealth = MaxHealth;
        }

        if (MaxHealth < 1)
        {
            MaxHealth = 1;
        }


        if (CurrHealth == 0)
        {
            Died();
        }
    }

    //The Enemy died. Disable the AI and play animation.
    public void Died()
    {
        AI.enabled = false;
        animation.CrossFade("die");
        Debug.Log("The Enemy Died RIP");

        //Dead = true; 
    }
}
