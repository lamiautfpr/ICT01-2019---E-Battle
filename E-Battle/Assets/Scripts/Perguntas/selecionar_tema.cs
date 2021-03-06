﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;


public class selecionar_tema : MonoBehaviour
{
    // Start is called before the first frame update

    public Text nome_tema;
    public Button pesquisarTemas;
    public GameObject pesquisar;
    private int cod_tema = -1;
    public AudioSource audioSeleTema;
    public AudioSource audioTemaVazio;
    float posX;
    float posY;

    public void set_tema (int cod){
        cod_tema = cod;
    }

    public int get_tema(){
        return cod_tema;
    }

    void Start()
    {
        nome_tema.text ="Nenhum";
        posX = pesquisar.transform.position.x;
        posY = pesquisar.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mostrarPainelPesquisa(){
        pesquisar.transform.position = new Vector3(0, 0, 0);
        GameObject.Find("mostrar_temas/tabela").GetComponent<tabelaDosTemas>().inicializarTabela();
        audioSeleTema = GameObject.Find("selecionar_tema").GetComponent<AudioSource>();
        audioSeleTema.Play();
    }

    public void tirarPainelPesquisa(){
        Text tema_vazio = GameObject.Find("tema_selecionado/tema_nao_selecionado").GetComponent<Text>();

        pesquisar.transform.position = new Vector3(posX, posY, 0);
        GameObject.Find("mostrar_temas/tabela").GetComponent<tabelaDosTemas>().inicializarTabela();
        audioTemaVazio.Play();
        if (cod_tema < 0 && !String.Equals(tema_vazio.text, "")){
            tema_vazio.text = "Selecione um tema para inserir a pergunta!";
            
        }
        else{
            tema_vazio.text = "";
            audioTemaVazio.Play();
        }
    }
}
