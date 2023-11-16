using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public GameObject FlorestaPrefab;
    public GameObject CidadePrefab;
    public GameObject DesertoPrefab;
    public GameObject NevadoPrefab;
    public GameObject VulcaoPrefab;

    public void GeraCarta(float x, float y, int idx){
        float num = UnityEngine.Random.value;
        Vector3 pos = new Vector3(x, y, 0f);
        if(num < 0.2){
            var card = Instantiate(FlorestaPrefab, pos, Quaternion.identity);
            DontDestroyOnLoad(card);
            GameManager.playerCards.Add((idx, card));
        }
        else if(num < 0.4){
            var card = Instantiate(CidadePrefab, pos, Quaternion.identity);
            DontDestroyOnLoad(card);
            GameManager.playerCards.Add((idx, card));
        }
        else if(num < 0.6){
            var card = Instantiate(DesertoPrefab, pos, Quaternion.identity);
            DontDestroyOnLoad(card);
            GameManager.playerCards.Add((idx, card));
        }
        else if(num < 0.8){
            var card = Instantiate(NevadoPrefab, pos, Quaternion.identity);
            DontDestroyOnLoad(card);
            GameManager.playerCards.Add((idx, card));
        }
        else if(num <= 1){
            var card = Instantiate(VulcaoPrefab, pos, Quaternion.identity);
            DontDestroyOnLoad(card);
            GameManager.playerCards.Add((idx, card));
        }
    }

    public string TiraCarta(){
        float num = UnityEngine.Random.value;
        if(num < 0.2){
            return FlorestaPrefab.name;
        }
        else if(num < 0.4){
            return CidadePrefab.name;
        }
        else if(num < 0.6){
            return DesertoPrefab.name;
        }
        else if(num < 0.8){
            return NevadoPrefab.name;
        }
        else if(num <= 1){
            return VulcaoPrefab.name;
        }
        return "";
    }

    public void PosicionaCarta(GameObject prefab, Vector3 pos){
        Instantiate(prefab, pos, Quaternion.identity);
    }

    void Awake()
    {
        
    }

    void Start(){
        if(GameManager.playerCards.Count == 0 && GameManager.atualPlayer == 0){
            GeraCarta(-3.8f, -4.7f, 1);
            GeraCarta(-0.5f, -4.7f, 2);
            GeraCarta(2.8f, -4.7f, 3);
        }
        else{
            foreach (var item in GameManager.playerCards)
            {
                item.card.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
