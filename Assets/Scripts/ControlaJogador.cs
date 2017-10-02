﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovimentoJogador))]
public class ControlaJogador : MonoBehaviour
{
    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    private AnimacaoPersonagem animacaoPersonagem;
    private MovimentoJogador movimentoJogador;
    [HideInInspector]
    public Status status;
    private void Start()
    {
        movimentoJogador = GetComponent<MovimentoJogador>();
        animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
        status = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {

        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaoPersonagem.AnimacaoMovimento (direcao);
    }

    void FixedUpdate()
    {
        movimentoJogador.Movimentacao(direcao, status.Velocidade);

        movimentoJogador.RotacaoJogador(MascaraChao);
    }

    public void TomarDano (int dano)
    {
        status.Vida -= dano;
        ControlaJogo.instancia.AtualizaSliderVidaJogador();
        if(status.Vida <= 0)
        {
            ControlaJogo.instancia.GameOver();
        }
    }
}
