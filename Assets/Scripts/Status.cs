using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour 
{
	[Range(1, 500)]
	public float Vida = 100;
	[Range(1, 100)]
	public float Velocidade = 5;
	public float DistanciaDeVisao = 15;	
}
