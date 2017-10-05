using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitPrimeirosSocorros : MonoBehaviour 
{
	void Start ()
	{
		Destroy(gameObject, 5);
	}
	void OnTriggerEnter(Collider objeto)
	{
		if(objeto.tag == Tags.Jogador)
		{
			objeto.GetComponent<ControlaJogador>().RecuperarVida(Random.Range(10, 20));
			Destroy(gameObject);
		}	
	}
}
