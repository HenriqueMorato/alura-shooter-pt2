using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

    public float Velocidade = 20;
    public int Dano = 10;
    private Rigidbody rigidbodyBala;
	public GameObject SangueZumbi;

    private void Start()
    {
        rigidbodyBala = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        rigidbodyBala.MovePosition
            (rigidbodyBala.position + 
            transform.forward * Velocidade * Time.deltaTime);
	}

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        switch (objetoDeColisao.tag)
        {
            case Tags.Inimigo:
                objetoDeColisao.GetComponent<ControlaInimigo>().TomarDano(Dano);   
			    Instantiate(SangueZumbi, transform.position, objetoDeColisao.transform.rotation);                             
            break;
            case Tags.Chefe:
                objetoDeColisao.GetComponent<ControlaChefe>().TomarDano(Dano);
			    Instantiate(SangueZumbi, transform.position, objetoDeColisao.transform.rotation);                                            
            break;
        }

        Destroy(gameObject);
    }
}
