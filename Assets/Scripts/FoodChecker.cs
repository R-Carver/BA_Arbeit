using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodChecker : MonoBehaviour {


	public Collider2D[] food;
	//public Collider[] food3d;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		food = Physics2D.OverlapCircleAll(this.transform.position, 3);
		//food3d = Physics.OverlapSphere(transform.position, 5f);
		
	}

	void OnDrawGizmos()
     {
         Gizmos.DrawWireSphere(transform.position, 3f);
     }
}
