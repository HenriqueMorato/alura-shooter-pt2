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
    [HideInInspector]
    public GeradorZumbis meuGerador;
    public GameObject KitMedico;
    public AudioClip SomMorte;
    
	// Use this for initialization
	void Start () {
        Jogador = GameObject.FindWithTag(Tags.Jogador);
        movimentoPersonagem = GetComponent<MovimentoPersonagem>();
        status = GetComponent<Status>();
        animacaoPersonagem = GetComponent<AnimacaoPersonagem>();

        AleatorizaTipoZumbi();
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);


        movimentoPersonagem.Rotacao(direcao);
	    animacaoPersonagem.AnimacaoMovimento(direcao);        
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
        
        if(Time.timeSinceLevelLoad > contadorVagar)
        {
            posicaoAleatoria = transform.position + (Random.insideUnitSphere * 10);
            posicaoAleatoria.y = 0f;
            contadorVagar = Time.timeSinceLevelLoad + tempoDaProximaMovimentacao;
        }


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
        Destroy(gameObject, 12);
        movimentoPersonagem.enabled = false;
        this.enabled = false;
        meuGerador.DiminuiQtdZumbis();
        ControlaAudio.instancia.PlayOneShot(SomMorte);
        ControlaJogo.instancia.MorteZumbiInterface();

        if(Random.value < 0.1)
        {
            GameObject kitMedico = Instantiate(KitMedico, transform.position, Quaternion.identity) as GameObject;
            Destroy(kitMedico, 5);
        }
    }

    void AleatorizaTipoZumbi ()
    {
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }
}
