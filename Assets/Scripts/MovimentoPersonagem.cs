using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour {

	protected Rigidbody rigidbodyJogador;
    protected void Awake()
    {
        rigidbodyJogador = GetComponent<Rigidbody>();
    }
	
	public void Movimentar (Vector3 direcao, float velocidade)
	{
		rigidbodyJogador.MovePosition
            (rigidbodyJogador.position +
            (direcao * velocidade * Time.deltaTime));
	}

    public void Rotacionar (Vector3 direcao)
    {
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        rigidbodyJogador.MoveRotation(novaRotacao);
    }

    public void CairPeloChao ()
    {
        rigidbodyJogador.constraints = RigidbodyConstraints.None;
        rigidbodyJogador.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        this.enabled = false;
    }
}
