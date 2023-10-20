using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public GameObject FlorestaPrefab;
    public GameObject CidadePrefab;
    public GameObject DesertoPrefab;
    public GameObject NevadoPrefab;
    public GameObject VulcaoPrefab;

    public void SelecionaCarta(float x, float y){
        float num = Random.value;
        Vector3 pos = new Vector3(x, y, 0f);
        if(num < 0.2){
            Instantiate(FlorestaPrefab, pos, Quaternion.identity);
            GameManager.playerCards.Add(FlorestaPrefab);
        }
        else if(num < 0.4){
            Instantiate(CidadePrefab, pos, Quaternion.identity);
            GameManager.playerCards.Add(CidadePrefab);
        }
        else if(num <0.6){
            Instantiate(DesertoPrefab, pos, Quaternion.identity);
            GameManager.playerCards.Add(DesertoPrefab);
        }
        else if(num < 0.8){
            Instantiate(NevadoPrefab, pos, Quaternion.identity);
            GameManager.playerCards.Add(NevadoPrefab);
        }
        else if(num <= 1){
            Instantiate(VulcaoPrefab, pos, Quaternion.identity);
            GameManager.playerCards.Add(VulcaoPrefab);
        }
    }

    public void PosicionaCarta(GameObject prefab, Vector3 pos){
        Instantiate(prefab, pos, Quaternion.identity);
    }

    void Awake()
    {
        if(GameManager.playerCards.Count == 0){
            SelecionaCarta(-3.8f, -4.7f);
            SelecionaCarta(-0.5f, -4.7f);
            SelecionaCarta(2.8f, -4.7f);
        }
        else{
            float x = -3.8f;
            float y = -4.7f;
            Vector3 pos;
            foreach (var item in GameManager.playerCards)
            {
                pos = new Vector3(x, y, 0f);
                PosicionaCarta(item, pos);
                x += 3.3f;
            }
        }
    }

    void Start(){

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
