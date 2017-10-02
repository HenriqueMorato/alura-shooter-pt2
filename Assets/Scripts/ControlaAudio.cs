using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour {

	public AudioSource audioSource;
	public static AudioSource instancia;

	void Start()
	{
		instancia = audioSource;
	}
}
