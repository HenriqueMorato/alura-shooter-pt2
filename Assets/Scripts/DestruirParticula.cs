using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestruirParticula : MonoBehaviour 
{
	private ParticleSystem particula;

	void Start()
	{
		particula = GetComponent<ParticleSystem>();
	}

	void Update()
	{
		if(particula.IsAlive() == false)
		{
			Destroy(gameObject);
		}
	}
}
