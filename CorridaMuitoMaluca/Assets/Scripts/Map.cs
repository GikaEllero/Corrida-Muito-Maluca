using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static GameObject FlorestaPrefab;
    public static GameObject CidadePrefab;
    public static GameObject DesertoPrefab;
    public static GameObject NevadoPrefab;
    public static GameObject VulcaoPrefab;
    public static GameObject PlayerPrefab;
    private List<(float x, float y)> posicoesMapa = new List<(float, float)>()
            {(-6.3f, 2f), (-4.9f, 2f), (-3.5f, 2f), (-2.1f, 2f), (-0.7f, 2f), (0.7f, 2f), (2.1f, 2f), (3.5f, 2f), (4.9f, 2f), (6.3f, 2f),
             (6.3f, 0.5f),
             (6.3f, -1f), (4.9f, -1f), (3.5f, -1f), (2.1f, -1f), (0.7f, -1f), (-0.7f, -1f), (-2.1f, -1f), (-3.5f, -1f), (-4.9f, -1f), (-6.3f, -1f),
             (-6.3f, -2.5f),
             (-6.3f, -4f), (-4.9f, -4f), (-3.5f, -4f), (-2.1f, -4f), (-0.7f, -4f), (0.7f, -4f), (2.1f, -4f), (3.5f, -4f), (4.9f, -4f), (6.3f, -4f)};


    public static void IniciaMapa(){
        FlorestaPrefab = Resources.Load<GameObject>("Cards/CardFloresta");
        CidadePrefab = Resources.Load<GameObject>("Cards/CardCidade");
        DesertoPrefab = Resources.Load<GameObject>("Cards/CardDeserto");
        NevadoPrefab = Resources.Load<GameObject>("Cards/CardNevado");
        VulcaoPrefab = Resources.Load<GameObject>("Cards/CardVulcao");
        PlayerPrefab = Resources.Load<GameObject>("Players/delorean");

        for (int i = 0; i < 32; i++)
        {
            if(i == 0){
                GameManager.mapCards.Add((i, FlorestaPrefab));
            }
            else if(i == 1){
                GameManager.mapCards.Add((i, CidadePrefab));
            }
            else if(i == 2){
                GameManager.mapCards.Add((i, DesertoPrefab));
            }
            else if(i == 3){
                GameManager.mapCards.Add((i, NevadoPrefab));
            }
            else if(i == 4){
                GameManager.mapCards.Add((i, VulcaoPrefab));
            }
            else{
                float num = Random.value;
                if(num < 0.2){
                    GameManager.mapCards.Add((i, FlorestaPrefab));
                }
                else if(num < 0.4){
                    GameManager.mapCards.Add((i, CidadePrefab));
                }
                else if(num <0.6){
                    GameManager.mapCards.Add((i, DesertoPrefab));
                }
                else if(num < 0.8){
                    GameManager.mapCards.Add((i, NevadoPrefab));
                }
                else if(num <= 1){
                    GameManager.mapCards.Add((i, VulcaoPrefab));
                } 
            }
        }
    }

    public void MostraMapa(){
        var mapCards = GameManager.mapCards;
        
        for (int i = 0; i < 32; i++)
        {
            PosicionaCarta(mapCards[i].card, mapCards[i].idx);
        }

        PosicionaPlayer(PlayerPrefab);
        PosicionaNpcs();
    }

    public void PosicionaCarta(GameObject prefab, int idx){
        var image = Instantiate(prefab, new Vector3(posicoesMapa[idx].x, posicoesMapa[idx].y, 0f), Quaternion.identity);
        image.transform.localScale -= new Vector3(1, 1, 1);
    }

    public void PosicionaPlayer(GameObject prefab){
        var player = GameManager.atualPlayer;

        if(player < 10){
            Vector3 pos = new Vector3(posicoesMapa[player].x, posicoesMapa[player].y + 1.45f, 0f);
            Instantiate(prefab, pos, Quaternion.identity);
        }

        if(player == 10){
            Vector3 pos = new Vector3(7.15f, 1.1f, 0f);
            var image = Instantiate(prefab, pos, Quaternion.identity);
            image.transform.Rotate(0f, 0f, -90f);
        }

        if(player > 10 && player < 21){
            Vector3 pos = new Vector3(posicoesMapa[player].x, posicoesMapa[player].y + 1.45f, 0f);
            var image = Instantiate(prefab, pos, Quaternion.identity);
            image.transform.Rotate(0f, 180f, 0f);
        }

        if(player == 21){
            Vector3 pos = new Vector3(-7.15f, -1.83f, 0f);
            var image = Instantiate(prefab, pos, Quaternion.identity);
            image.transform.Rotate(0f, 180f, -90f);
        }

        if(player > 21){
            Vector3 pos = new Vector3(posicoesMapa[player].x, posicoesMapa[player].y + 1.45f, 0f);
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }

    public void PosicionaNpcs(){
        foreach (var item in GameManager.npcs)
        {
            Debug.Log(item.idx);
            if(item.idx < 10){
                Vector3 pos = new Vector3(posicoesMapa[item.idx].x, posicoesMapa[item.idx].y + 1.45f, 0f);
                Instantiate(item.prefab, pos, Quaternion.identity);
            }

            if(item.idx == 10){
                Vector3 pos = new Vector3(7.15f, 1.1f, 0f);
                var image = Instantiate(item.prefab, pos, Quaternion.identity);
                image.transform.Rotate(0f, 0f, -90f);
            }

            if(item.idx > 10 && item.idx < 21){
                Vector3 pos = new Vector3(posicoesMapa[item.idx].x, posicoesMapa[item.idx].y + 1.45f, 0f);
                var image = Instantiate(item.prefab, pos, Quaternion.identity);
                image.transform.Rotate(0f, 180f, 0f);
            }

            if(item.idx == 21){
                Vector3 pos = new Vector3(-7.15f, -1.83f, 0f);
                var image = Instantiate(item.prefab, pos, Quaternion.identity);
                image.transform.Rotate(0f, 180f, -90f);
            }

            if(item.idx > 21){
                Vector3 pos = new Vector3(posicoesMapa[item.idx].x, posicoesMapa[item.idx].y + 1.45f, 0f);
                Instantiate(item.prefab, pos, Quaternion.identity);
            }
        }
    }

    void Awake()
    {
        FlorestaPrefab = Resources.Load<GameObject>("Prefab/CardFloresta");
        CidadePrefab = Resources.Load<GameObject>("Prefab/CardCidade");
        DesertoPrefab = Resources.Load<GameObject>("Prefab/CardDeserto");
        NevadoPrefab = Resources.Load<GameObject>("Prefab/CardNevado");
        VulcaoPrefab = Resources.Load<GameObject>("Prefab/CardVulcao");
        PlayerPrefab = Resources.Load<GameObject>("Players/delorean");

        if(GameManager.mapCards.Count > 0)
            MostraMapa();
        else{
            IniciaMapa();
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
