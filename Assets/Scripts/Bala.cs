using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

    public float Velocidade = 20;
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
        if(objetoDeColisao.tag == Tags.Inimigo)
        {
            objetoDeColisao.GetComponent<ControlaInimigo>().MorteZumbi();
			GameObject particula = Instantiate(SangueZumbi, transform.position, objetoDeColisao.transform.rotation);
			Destroy(particula, 1f);
        }
        else if(objetoDeColisao.tag == Tags.Chefe)
        {
            objetoDeColisao.GetComponent<ControlaBoss>().TomarDano(10);
        }

        Destroy(gameObject);
    }
}
