using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour {

	protected Rigidbody rigidbodyJogador;
    protected virtual void Start()
    {
        rigidbodyJogador = GetComponent<Rigidbody>();
    }
	
	public void Movimentacao (Vector3 direcao, float velocidade)
	{
		rigidbodyJogador.MovePosition
            (rigidbodyJogador.position +
            (direcao * velocidade * Time.deltaTime));
	}

    public void Rotacao (Quaternion rotacao)
    {
        rigidbodyJogador.MoveRotation(rotacao);
    }

    public void CairPeloChao ()
    {
        rigidbodyJogador.constraints = RigidbodyConstraints.None;
        GetComponent<Collider>().enabled = false;
    }
}
