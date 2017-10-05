using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimacaoPersonagem : MonoBehaviour 
{
	private Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void AnimarMovimento (Vector3 direcao)
	{
		animator.SetFloat("Movendo", direcao.magnitude);
	} 		

	public void Atacar (bool estado)
	{
		animator.SetBool("Atacando", estado);
	}

	public void Morte ()
	{
		animator.SetTrigger("Morte");
	}
}
