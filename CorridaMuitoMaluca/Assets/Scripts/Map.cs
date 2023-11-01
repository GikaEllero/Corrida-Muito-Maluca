using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static GameObject FlorestaPrefab = Resources.Load<GameObject>("Prefab/CardFloresta");
    public static GameObject CidadePrefab = Resources.Load<GameObject>("Prefab/CardCidade");
    public static GameObject DesertoPrefab = Resources.Load<GameObject>("Prefab/CardDeserto");
    public static GameObject NevadoPrefab = Resources.Load<GameObject>("Prefab/CardNevado");
    public static GameObject VulcaoPrefab = Resources.Load<GameObject>("Prefab/CardVulcao");

    public static void IniciaMapa(){

        for (int i = 0; i < 32; i++)
        {
            float num = Random.value;
            if(num < 0.2){
                GameManager.mapCards.Add(FlorestaPrefab);
            }
            else if(num < 0.4){
                GameManager.mapCards.Add(CidadePrefab);
            }
            else if(num <0.6){
                GameManager.mapCards.Add(DesertoPrefab);
            }
            else if(num < 0.8){
                GameManager.mapCards.Add(NevadoPrefab);
            }
            else if(num <= 1){
                GameManager.mapCards.Add(VulcaoPrefab);
            }   
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

        var image = Instantiate(prefab, new Vector3(x[posX], y[posY], 0f), Quaternion.identity);
        image.transform.localScale -= new Vector3(1, 1, 1);
    }

    void Awake()
    {
        if(GameManager.mapCards.Count > 0)
            MostraMapa();
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
