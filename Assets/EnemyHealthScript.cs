using UnityEngine;
using System.Collections;

public class EnemyHealthScript : MonoBehaviour {

    public Transform target;
    public GameObject enemy;
    GUITexture HealthBar;
    EnemyHealth eh;

	// Use this for initialization
	void Start () {
	    HealthBar = GetComponent<GUITexture>();
        //eh = GameObject.FindWithTag("Finish").GetComponent<EnemyHealth>();
        eh = enemy.GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
    void Update ()
    {
        //WorkdToScreenPoint
        Vector3 wantedPos = Camera.main.WorldToViewportPoint(target.position);
        //wantedPos.z = 0;
        //Vector3 goof = Camera.main.WorldToScreenPoint(target.position);
        //Debug.Log(goof);
        wantedPos.y += .1f;
        wantedPos.x += -.025f;

        transform.position = wantedPos;

         //Debug.Log(wantedPos);

        //Resize health bar
        float newBarSz = 50f * eh.CurrHealth * 0.01f;

        HealthBar.pixelInset = new Rect(0, 0, newBarSz, 10);
    }
}
