using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject FlorestaPrefab;
    public GameObject CidadePrefab;
    public GameObject DesertoPrefab;
    public GameObject NevadoPrefab;
    public GameObject VulcaoPrefab;

    public void IniciaMapa(){
        int posX = 0;
        int posY = 0;
        for (int i = 0; i < 10; i++)
        {
            GeraCartas(posX, posY);
            posX += 1;
        }

        posX = 9;
        posY = 1;
        for (int i = 0; i < 2; i++)
        {
            GeraCartas(posX, posY);
            posY += 1;
        }

        posX = 8;
        posY = 2;
        for (int i = 0; i < 9; i++)
        {
            GeraCartas(posX, posY);
            posX -= 1;
        }

        posX = 0;
        posY = 3;
        for (int i = 0; i < 2; i++)
        {
            GeraCartas(posX, posY);
            posY += 1;
        }

        posX = 1;
        posY = 4;
        for (int i = 0; i < 9; i++)
        {
            GeraCartas(posX, posY);
            posX += 1;
        }  
    }

    public void GeraCartas(int posX, int posY){
        float[] x = {-6.3f, -4.9f, -3.5f, -2.1f, -0.7f, 0.7f, 2.1f, 3.5f, 4.9f, 6.3f};
        float[] y = {2f, 0.5f, -1f, -2.5f, -4f};

        float num = Random.value;
        Vector3 pos = new Vector3(x[posX], y[posY], 0f);
        if(num < 0.2){
            Instantiate(FlorestaPrefab, pos, Quaternion.identity);
            GameManager.mapCards.Add(FlorestaPrefab);
        }
        else if(num < 0.4){
            Instantiate(CidadePrefab, pos, Quaternion.identity);
            GameManager.mapCards.Add(CidadePrefab);
        }
        else if(num <0.6){
            Instantiate(DesertoPrefab, pos, Quaternion.identity);
            GameManager.mapCards.Add(DesertoPrefab);
        }
        else if(num < 0.8){
            Instantiate(NevadoPrefab, pos, Quaternion.identity);
            GameManager.mapCards.Add(NevadoPrefab);
        }
        else if(num <= 1){
            Instantiate(VulcaoPrefab, pos, Quaternion.identity);
            GameManager.mapCards.Add(VulcaoPrefab);
        }   
    }

    public void MostraMapa(){
        var mapCards = GameManager.mapCards;
        int card = 0;
        int posX = 0;
        int posY = 0;
        for (int i = 0; i < 10; i++)
        {
            PosicionaCarta(mapCards[card], posX, posY);
            card += 1;
            posX += 1;
        }

        posX = 9;
        posY = 1;
        for (int i = 0; i < 2; i++)
        {
            PosicionaCarta(mapCards[card], posX, posY);
            card += 1;
            posY += 1;
        }

        posX = 8;
        posY = 2;
        for (int i = 0; i < 9; i++)
        {
            PosicionaCarta(mapCards[card], posX, posY);
            card += 1;
            posX -= 1;
        }

        posX = 0;
        posY = 3;
        for (int i = 0; i < 2; i++)
        {
            PosicionaCarta(mapCards[card], posX, posY);
            card += 1;
            posY += 1;
        }

        posX = 1;
        posY = 4;
        for (int i = 0; i < 9; i++)
        {
            PosicionaCarta(mapCards[card], posX, posY);
            card += 1;
            posX += 1;
        }  
    }

    public void PosicionaCarta(GameObject prefab, int posX, int posY){
        float[] x = {-6.3f, -4.9f, -3.5f, -2.1f, -0.7f, 0.7f, 2.1f, 3.5f, 4.9f, 6.3f};
        float[] y = {2f, 0.5f, -1f, -2.5f, -4f};

        Instantiate(prefab, new Vector3(x[posX], y[posY], 0f), Quaternion.identity);
    }

    void Awake()
    {
        if(GameManager.mapCards.Count == 0){
            IniciaMapa();
        }
        else{
            MostraMapa();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
