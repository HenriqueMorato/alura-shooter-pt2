using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovimentoPersonagem))]
[RequireComponent(typeof(Status))]
public class ControlaInimigo : MonoBehaviour, IMatavel {

    private GameObject Jogador;
    private MovimentoPersonagem movimentoPersonagem;
    private AnimacaoPersonagem animacaoPersonagem;
    private Status status;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float tempoDaProximaMovimentacaoAleatoria = 4;
    private float contadorVagar;
    public float PorcentagemDeGerarKitMedico = 0.1f;

    [HideInInspector]
    public GeradorZumbis meuGerador;
    public GameObject KitMedico;
    public AudioClip SomDaMorte;
    
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


        movimentoPersonagem.Rotacionar(direcao);
	    animacaoPersonagem.AnimarMovimento(direcao);    
        if(distancia > status.DistanciaDeVisao)
        {
            Vagar();
        }
        else if (distancia > 2.5)
        {  
            direcao = Jogador.transform.position - transform.position;            

            movimentoPersonagem.Movimentar(direcao.normalized, status.Velocidade);

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
            posicaoAleatoria = AleatorizarPosicao();
            float tempoAleatorio = Random.Range(-1f, 1f);
            contadorVagar = Time.timeSinceLevelLoad + tempoDaProximaMovimentacaoAleatoria + tempoAleatorio;
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05f;
        if(ficouPertoOSuficiente == false)
		{
		    direcao = posicaoAleatoria - transform.position;
            movimentoPersonagem.Movimentar(direcao.normalized, status.Velocidade);            
		}
    }

    Vector3 AleatorizarPosicao ()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

    void BateNoJogador ()
    {
        int dano = Random.Range(25,30);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    void AleatorizaTipoZumbi ()
    {
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int _dano)
    {
        status.Vida -= _dano;
        if(status.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        movimentoPersonagem.CairPeloChao();
        animacaoPersonagem.Morte();
        Destroy(gameObject, 2);
        this.enabled = false;
        meuGerador.DiminuiQtdZumbis();
        ControlaAudio.instancia.PlayOneShot(SomDaMorte);
        ControlaInterface.instancia.AtualizarZumbisInterface();

        if(Random.value < PorcentagemDeGerarKitMedico)
        {
            Instantiate(KitMedico, transform.position, Quaternion.identity);
        }
    }
}
