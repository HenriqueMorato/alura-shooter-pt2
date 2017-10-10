using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ControlaInterface : MonoBehaviour 
{
	public Slider sliderVidaJogador;
	private Status statusJogador;
	public static ControlaInterface instancia;
	public GameObject GameOverPanel;
	public Text TextTempoSobrevivencia;
	private int quantidadeZumbisMortos = 0;
	public Text TextQuantidadeZumbisMortos;
	public Text TextQuantidadeZumbisMortosGameOver;
	public Text TextChefeAparece;

	void Awake ()
	{
		instancia = this;		
	}
	void Start()
	{
		statusJogador = GameObject.FindWithTag(Tags.Jogador).GetComponent<Status>();

		sliderVidaJogador.maxValue = statusJogador.Vida;
		AtualizaSliderVidaJogador ();

		Time.timeScale = 1;
	}

	public void AtualizaSliderVidaJogador ()
	{
		sliderVidaJogador.value = statusJogador.Vida;
	}

	public void GameOver ()
	{
		Time.timeScale = 0;
		int minutos = (int)(Time.timeSinceLevelLoad / 60);
		int segundos = (int)(Time.timeSinceLevelLoad % 60);		
		string textoTempoSobrevivencia = string.Format("Você sobreviveu por {0}min e {1}s", minutos, segundos);
		
		TextTempoSobrevivencia.text = textoTempoSobrevivencia;
		StartCoroutine(MostrarObjeto(GameOverPanel, 1));
		StartCoroutine(IncrementaValorAte(quantidadeZumbisMortos, 2));				
	}

	IEnumerator MostrarObjeto(GameObject obj, float tempo)
	{
		yield return new WaitForSecondsRealtime(1);
		obj.SetActive(true);
	}

	public void AtualizarZumbisInterface()
	{
		quantidadeZumbisMortos++;
		TextQuantidadeZumbisMortos.text = "x " + quantidadeZumbisMortos;
	}

	public void AtualizarZumbisInterface(int score)
	{
		quantidadeZumbisMortos += score;
		TextQuantidadeZumbisMortos.text = "x " + quantidadeZumbisMortos;		
	}

	IEnumerator IncrementaValorAte(int valor, int duracao)
	{
		yield return new WaitForSecondsRealtime(1);
		int min = 0;
		float relogio = 0;
		while(min <= valor)
		{
			relogio += Time.unscaledDeltaTime;		
			float progresso = relogio / duracao;
			min = (int)Mathf.Lerp(0, valor, progresso);
			TextQuantidadeZumbisMortosGameOver.text = min.ToString();
			yield return null;
		}
	}

	public void TextoChefeAparece ()
	{
		StartCoroutine(DesaparecerTexto(2, TextChefeAparece));
	}

	public void TrocarCenaJogo ()
	{
		SceneManager.LoadScene("game");
	}

	IEnumerator DesaparecerTexto (float tempoDeSumico, Text text)
	{
		text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
		text.gameObject.SetActive(true);
		yield return new WaitForSeconds(1);	
		while(text.color.a > 0)
		{
			text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / tempoDeSumico));
			if(text.color.a <= 0)
			{
				text.gameObject.SetActive(false);
			}
			yield return null;
		}
	}
}
