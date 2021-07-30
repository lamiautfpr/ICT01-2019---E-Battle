﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popUp_pergunta : MonoBehaviour
{
    // Start is called before the first frame update
    private static int op;
    private static bool moving = false;

    private static bool mostrarTexto = false;

    private static float tempoTela = 3.0f;

    private static int popUpSpeed;
    public static bool tempoAcabou = false;

    public AudioClip audioCerto;
    public AudioClip audioErrado;
    public AudioSource som;


    public static void set_op(int opcao)
    {
        op = opcao;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("painel_Pergunta").GetComponent<apresentarPergunta>().getAlternativasAtuaisSize() >= 2)
        {
            if (Vector3.Distance(GameObject.Find("fundo_feedback_da_resposta").transform.position, GameObject.Find("Camera_Tabuleiro").transform.position + new Vector3(700, -350, 1)) < 1000f)
            {
                popUpSpeed = 50000;  //400 é o tempo padrão
            }
        }
        else
            popUpSpeed = 10000; //1000 é o tempo padrão

        if (moving)
        {
            GameObject.Find("fundo_feedback_da_resposta/Button").GetComponent<Button>().interactable = false;
            GameObject.Find("fundo_feedback_da_resposta").transform.position = Vector3.MoveTowards(GameObject.Find("fundo_feedback_da_resposta").transform.position, GameObject.Find("Camera_Tabuleiro").transform.position + new Vector3(700, -350, 1), popUpSpeed * Time.deltaTime);
            if (Vector3.Distance(GameObject.Find("fundo_feedback_da_resposta").transform.position, GameObject.Find("Camera_Tabuleiro").transform.position + new Vector3(700, -350, 1)) < 0.001f)
            {
                moving = false;

                if (GameObject.Find("painel_Pergunta").GetComponent<apresentarPergunta>().getAlternativasAtuaisSize() >= 2)
                    mostrarTexto = true;
                else
                    mostrarTextoPerguntaAberta();
                GameObject.Find("fundo_feedback_da_resposta/Button").GetComponent<Button>().interactable = true;
            }
        }

        if (mostrarTexto)
            mostrarTextoDoFeedback();

    }

    private void mostrarTextoDoFeedback()
    {
        if (tempoTela > 1)
        {
            tempoTela -= Time.deltaTime;
        }
        else
        {
            mostrarTexto = false;
            tempoTela = 3f;

            if (op == 1)
            {
                GameObject.Find("fundo_feedback_da_resposta/Text").GetComponent<Text>().text = "Parabéns, você acertou!";
                som.clip = audioCerto;
                som.Play();

            }
            else if (op == 0)
            {
                GameObject.Find("fundo_feedback_da_resposta/Text").GetComponent<Text>().text = "Que pena, a resposta está incorreta!";
                som.clip = audioErrado;
                som.Play();

                if (tempoAcabou)
                {
                    GameObject.Find("fundo_feedback_da_resposta/Text").GetComponent<Text>().text = "Que pena, seu tempo acabou!";
                }
            }
        }
    }

    private void mostrarTextoPerguntaAberta()
    {
        if (op == 1)
        {
            GameObject.Find("fundo_feedback_da_resposta/Text").GetComponent<Text>().text = "Parabéns, você acertou!";
        }
        else if (op == 0)
        {
            GameObject.Find("fundo_feedback_da_resposta/Text").GetComponent<Text>().text = "Que pena, a resposta está incorreta!";
            if (tempoAcabou)
            {
                GameObject.Find("fundo_feedback_da_resposta/Text").GetComponent<Text>().text = "Que pena, seu tempo acabou!";
            }
        }
    }

    public static void mostrarPopUp()
    {
        moving = true;
        GameObject.Find("fundo_feedback_da_resposta/Text").GetComponent<Text>().text = "";
        GameObject.Find("painel_Pergunta").GetComponent<apresentarPergunta>().desabilitarAlternativas();
    }

    public void continuar()
    {
        if (op == 1)
        {
            GameObject.Find("rolarDado").GetComponent<mostrarDado>().mover(1);
        }
        else if (op == 0)
        {
            GameObject.FindGameObjectWithTag("Controlador").GetComponent<MvP1>().passarVez();
        }

        tempoAcabou = false;
        GameObject.Find("painel_Pergunta").transform.position = GameObject.Find("Camera_Tabuleiro").transform.position + new Vector3(0, 2000, 0);
        GameObject.Find("painel_Pergunta").GetComponent<apresentarPergunta>().set_pergAtual(GameObject.Find("painel_Pergunta").GetComponent<apresentarPergunta>().get_pergAtual() + 1);
        GameObject.Find("painel_Pergunta").GetComponent<apresentarPergunta>().reiniciarComponentes();
        GameObject.Find("painel_Pergunta").GetComponent<apresentarPergunta>().mostrarPergunta();

    }

}
