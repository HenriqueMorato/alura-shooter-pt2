using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovimentoJogador))]
public class ControlaJogador : MonoBehaviour
{
    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    private AnimacaoPersonagem animacaoJogador;
    private MovimentoJogador movimentoJogador;
    [HideInInspector]
    public Status status;
    public AudioClip SomDano;

    private void Start()
    {
        movimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        status = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {

        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaoJogador.AnimarMovimento (direcao);
    }

    void FixedUpdate()
    {
        movimentoJogador.Movimentar(direcao, status.Velocidade);

        movimentoJogador.RotacaoJogador(MascaraChao);
    }

    public void TomarDano (int dano)
    {
        status.Vida -= dano;
        ControlaAudio.instancia.PlayOneShot(SomDano);
        ControlaInterface.instancia.AtualizaSliderVidaJogador();
        if(status.Vida <= 0)
        {
            ControlaInterface.instancia.GameOver();
        }
    }

    public void RecuperarVida (int vida)
    {
        status.Vida += vida;
        if(status.Vida > status.VidaInicial)
        {
            status.Vida = status.VidaInicial;
        }
        ControlaInterface.instancia.AtualizaSliderVidaJogador();        
    }
}
