using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour 
{
	
	// Update is called once per frame
	void Update () {

		transform.LookAt(transform.position + Camera.main.transform.forward, Camera.main.transform.up);
		//transform.LookAt(transform.position + Camera.main.transform.forward, transform.up + Camera.main.transform.up);
		
	}
}
