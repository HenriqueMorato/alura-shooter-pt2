using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour 
{
	[Range(1, 500)]
	public float VidaInicial = 100;
	[HideInInspector]
	public float Vida;
	[Range(1, 100)]
	public float Velocidade = 5;
	public float DistanciaDeVisao = 15;	

	void Start ()
	{
		Vida = VidaInicial;
	}
}
