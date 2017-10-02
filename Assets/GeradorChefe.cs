using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour 
{
	public GameObject ChefePrefab;
	private float contadorTempo = 0;
    public float TempoGerarBoss = 60;

	void Start ()
	{
		contadorTempo = TempoGerarBoss;
	}


	void Update ()
	{
		if(Time.timeSinceLevelLoad > contadorTempo)
		{
			contadorTempo = Time.timeSinceLevelLoad + TempoGerarBoss;
			Instantiate(ChefePrefab, transform.position, transform.rotation);
			ControlaJogo.instancia.TextoChefeAparece();
		}
	}
}
