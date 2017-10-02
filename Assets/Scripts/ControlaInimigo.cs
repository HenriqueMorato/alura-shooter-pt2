using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovimentoPersonagem))]
[RequireComponent(typeof(Status))]
public class ControlaInimigo : MonoBehaviour {

    private GameObject Jogador;
    private MovimentoPersonagem movimentoPersonagem;
    private AnimacaoPersonagem animacaoPersonagem;
    private Status status;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float tempoDaProximaMovimentacao = 4;
    private float contadorVagar;

	// Use this for initialization
	void Start () {
        Jogador = GameObject.FindWithTag("Jogador");
        status = GetComponent<Status>();
        movimentoPersonagem = GetComponent<MovimentoPersonagem>();
        animacaoPersonagem = GetComponent<AnimacaoPersonagem>();

        AleatoriaTipoZumbi();
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);


        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        movimentoPersonagem.Rotacao(novaRotacao);

        if(distancia > status.DistanciaDeVisao)
        {
            Vagar();
        }
        else if (distancia > 2.5)
        {
            
            direcao = Jogador.transform.position - transform.position;            

            movimentoPersonagem.Movimentacao(direcao.normalized, status.Velocidade);

            animacaoPersonagem.Atacar(false);
        }
        else
        {
            animacaoPersonagem.Atacar(true);
        }
    }

    void Vagar ()
    {
        
        if(Time.time > contadorVagar)
        {
            posicaoAleatoria = transform.position + (Random.insideUnitSphere * 10);
            posicaoAleatoria.y = 0f;
            contadorVagar = Time.time + tempoDaProximaMovimentacao;
        }

	    animacaoPersonagem.AnimacaoMovimento(direcao);

        if(Vector3.Distance(transform.position, posicaoAleatoria) >= 0.05f)
		{
		    direcao = posicaoAleatoria - transform.position;
            movimentoPersonagem.Movimentacao(direcao.normalized, status.Velocidade);            
		}
    }

    void DanoJogador ()
    {
        Jogador.GetComponent<ControlaJogador>().TomarDano(Random.Range(25,40));
    }

    public void MorteZumbi()
    {
        movimentoPersonagem.CairPeloChao();
        animacaoPersonagem.Morte();
        Destroy(gameObject, 1f);
        movimentoPersonagem.enabled = false;
        this.enabled = false;
    }

    void AleatoriaTipoZumbi ()
    {
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }
}
