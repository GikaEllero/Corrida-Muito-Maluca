using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static GameObject FlorestaPrefab;
    public static GameObject CidadePrefab;
    public static GameObject DesertoPrefab;
    public static GameObject NevadoPrefab;
    public static GameObject VulcaoPrefab;
    public static GameObject PlayerPrefab;
    public static GameObject DickPrefab;
    public static GameObject TrapPrefab;
    public static GameObject FinishPrefab;

    private List<(float x, float y)> posicoesMapa = new List<(float, float)>()
            {(-5.8f, 1.7f), (-4.5f, 1.7f), (-3.2f, 1.7f), (-1.9f, 1.7f), (-0.6f, 1.7f), (0.7f, 1.7f), (2f, 1.7f), (3.3f, 1.7f), (4.6f, 1.7f), (5.9f, 1.7f),
             (5.9f, 0.3f),
             (5.9f, -1.1f), (4.6f, -1.1f), (3.3f, -1.1f), (2f, -1.1f), (0.7f, -1.1f), (-0.6f, -1.1f), (-1.9f, -1.1f), (-3.2f, -1.1f), (-4.5f, -1.1f), (-5.8f, -1.1f),
             (-5.8f, -2.5f),
             (-5.8f, -3.9f), (-4.5f, -3.9f), (-3.2f, -3.9f), (-1.9f, -3.9f), (-0.6f, -3.9f), (0.7f, -3.9f), (2f, -3.9f), (3.3f, -3.9f), (4.6f, -3.9f), (5.9f, -3.9f)};


    public static void IniciaMapa(){
        FlorestaPrefab = Resources.Load<GameObject>("Cards/CardFloresta");
        CidadePrefab = Resources.Load<GameObject>("Cards/CardCidade");
        DesertoPrefab = Resources.Load<GameObject>("Cards/CardDeserto");
        NevadoPrefab = Resources.Load<GameObject>("Cards/CardNevado");
        VulcaoPrefab = Resources.Load<GameObject>("Cards/CardVulcao");
        PlayerPrefab = Resources.Load<GameObject>("Players/Delorean");
        
        GameManager.mapCards.Add((0, FlorestaPrefab));
        GameManager.mapCards.Add((1, CidadePrefab));
        GameManager.mapCards.Add((2, DesertoPrefab));
        GameManager.mapCards.Add((3, NevadoPrefab));
        GameManager.mapCards.Add((4, VulcaoPrefab));

        for (int i = 5; i < 31; i++)
        {
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

    public void MostraMapa(){
        var mapCards = GameManager.mapCards;
        
        for (int i = 0; i < 31; i++)
        {
            PosicionaCarta(mapCards[i].card, mapCards[i].idx);
        }

        PosicionaCarta(FinishPrefab, 31);
        PosicionaPlayer(PlayerPrefab);
        PosicionaNpcs();
        PosicionaDick();
        PosicionaArmadilhas();
    }

    public void PosicionaArmadilhas(){
        foreach (var item in GameManager.traps)
        {
            Vector3 pos = new Vector3(posicoesMapa[item].x, posicoesMapa[item].y + 0.25f, 0f);
            Instantiate(TrapPrefab, pos, Quaternion.identity);
        }
    }

    public void PosicionaDick(){
        if(GameManager.dickAlive){
            Vector3 pos = new Vector3(posicoesMapa[GameManager.dickPosition].x, posicoesMapa[GameManager.dickPosition].y, 0f);
            Instantiate(DickPrefab, pos, Quaternion.identity);
        }
    }

    public void PosicionaCarta(GameObject prefab, int idx){
        var image = Instantiate(prefab, new Vector3(posicoesMapa[idx].x, posicoesMapa[idx].y, 0f), Quaternion.identity);
        image.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
    }

    public void PosicionaPlayer(GameObject prefab){
        var player = GameManager.atualPlayer;

        if(player < 10){
            Vector3 pos = new Vector3(posicoesMapa[player].x, posicoesMapa[player].y + 1.2f, 0f);
            Instantiate(prefab, pos, Quaternion.identity);
        }

        if(player == 10){
            Vector3 pos = new Vector3(6.55f, 0.87f, 0f);
            var image = Instantiate(prefab, pos, Quaternion.identity);
            image.transform.Rotate(0f, 0f, -90f);
        }

        if(player == 11){
            Vector3 pos = new Vector3(6.55f, -0.53f, 0f);
            var image = Instantiate(prefab, pos, Quaternion.identity);
            image.transform.Rotate(0f, 0f, -90f);
        }

        if(player > 11 && player < 21){
            Vector3 pos = new Vector3(posicoesMapa[player].x, posicoesMapa[player].y + 1.2f, 0f);
            var image = Instantiate(prefab, pos, Quaternion.identity);
            image.transform.Rotate(0f, 180f, 0f);
        }

        if(player == 21){
            Vector3 pos = new Vector3(-6.45f, -1.93f, 0f);
            var image = Instantiate(prefab, pos, Quaternion.identity);
            image.transform.Rotate(0f, 180f, -90f);
        }

        if(player == 22){
            Vector3 pos = new Vector3(-6.45f, -3.35f, 0f);
            var image = Instantiate(prefab, pos, Quaternion.identity);
            image.transform.Rotate(0f, 180f, -90f);
        }

        if(player > 22){
            Vector3 pos = new Vector3(posicoesMapa[player].x, posicoesMapa[player].y + 1.2f, 0f);
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }

    public void PosicionaNpcs(){
        List<int> idx = new List<int>();
        foreach (var item in GameManager.npcs)
        {
            if(item.idx < 9){
                if(idx.Contains(item.idx) || GameManager.atualPlayer == item.idx){
                    Vector3 pos = new Vector3(posicoesMapa[item.idx].x, posicoesMapa[item.idx].y - 0.8f, 0f);
                    Instantiate(item.prefab, pos, Quaternion.identity);
                }
                else{
                    Vector3 pos = new Vector3(posicoesMapa[item.idx].x, posicoesMapa[item.idx].y + 1.2f, 0f);
                    Instantiate(item.prefab, pos, Quaternion.identity);
                } 
            }

            if(item.idx == 9){
                if(idx.Contains(item.idx) || GameManager.atualPlayer == item.idx){
                    Vector3 pos = new Vector3(6.55f, 2.3f, 0f);
                    var image = Instantiate(item.prefab, pos, Quaternion.identity);
                    image.transform.Rotate(0f, 0f, -90f);
                }
                else{
                    Vector3 pos = new Vector3(posicoesMapa[item.idx].x, posicoesMapa[item.idx].y + 1.2f, 0f);
                    Instantiate(item.prefab, pos, Quaternion.identity);
                } 
            }

            if(item.idx == 10){
                if(idx.Contains(item.idx) || GameManager.atualPlayer == item.idx){
                    Vector3 pos = new Vector3(6.55f, 0.9f, 0f);
                    var image = Instantiate(item.prefab, pos, Quaternion.identity);
                    image.transform.Rotate(0f, 0f, -90f);
                }
                else{
                    Vector3 pos = new Vector3(5.27f, 0.9f, 0f);
                    var image = Instantiate(item.prefab, pos, Quaternion.identity);
                    image.transform.Rotate(180f, 0f, 90f);
                } 
                
            }

            if(item.idx == 11){
                if(idx.Contains(item.idx) || GameManager.atualPlayer == item.idx){
                    Vector3 pos = new Vector3(posicoesMapa[item.idx].x, posicoesMapa[item.idx].y - 0.8f, 0f);
                    var image = Instantiate(item.prefab, pos, Quaternion.identity);
                    image.transform.Rotate(0f, 180f, 0f);
                }
                else{
                    Vector3 pos = new Vector3(6.55f, -0.5f, 0f);
                    var image = Instantiate(item.prefab, pos, Quaternion.identity);
                    image.transform.Rotate(0f, 0f, -90f);
                    
                } 
                
            }

            if(item.idx > 11 && item.idx < 20){
                if(idx.Contains(item.idx) || GameManager.atualPlayer == item.idx){
                    Vector3 pos = new Vector3(posicoesMapa[item.idx].x, posicoesMapa[item.idx].y + 1.2f, 0f);
                    var image = Instantiate(item.prefab, pos, Quaternion.identity);
                    image.transform.Rotate(0f, 180f, 0f);
                }
                else{
                    Vector3 pos = new Vector3(posicoesMapa[item.idx].x, posicoesMapa[item.idx].y - 0.8f, 0f);
                    var image = Instantiate(item.prefab, pos, Quaternion.identity);
                    image.transform.Rotate(0f, 180f, 0f);
                } 
            }

            if(item.idx == 20){
                if(idx.Contains(item.idx) || GameManager.atualPlayer == item.idx){
                    Vector3 pos = new Vector3(-6.45f, -0.5f, 0f);
                    var image = Instantiate(item.prefab, pos, Quaternion.identity);
                    image.transform.Rotate(0f, 180f, -90f);
                }
                else{
                    Vector3 pos = new Vector3(posicoesMapa[item.idx].x, posicoesMapa[item.idx].y + 1.2f, 0f);
                    var image = Instantiate(item.prefab, pos, Quaternion.identity);
                    image.transform.Rotate(0f, 180f, 0f);
                } 
            }

            if(item.idx == 21){
                if(idx.Contains(item.idx) || GameManager.atualPlayer == item.idx){
                    Vector3 pos = new Vector3(-5.15f, -1.95f, 0f);
                    var image = Instantiate(item.prefab, pos, Quaternion.identity);
                    image.transform.Rotate(0f, 0f, -90f);
                }
                else{
                    Vector3 pos = new Vector3(-6.45f, -1.95f, 0f);
                    var image = Instantiate(item.prefab, pos, Quaternion.identity);
                    image.transform.Rotate(0f, 180f, -90f);
                } 
            }

            if(item.idx == 22){
                if(idx.Contains(item.idx) || GameManager.atualPlayer == item.idx){
                    Vector3 pos = new Vector3(-6.45f, -1.95f, 0f);
                    var image = Instantiate(item.prefab, pos, Quaternion.identity);
                    image.transform.Rotate(0f, 180f, -90f);
                }
                else{
                    Vector3 pos = new Vector3(posicoesMapa[item.idx].x, posicoesMapa[item.idx].y - 0.8f, 0f);
                    Instantiate(item.prefab, pos, Quaternion.identity);
                } 
            }

            if(item.idx > 22){
                if(idx.Contains(item.idx) || GameManager.atualPlayer == item.idx){
                    Vector3 pos = new Vector3(posicoesMapa[item.idx].x, posicoesMapa[item.idx].y - 0.8f, 0f);
                    Instantiate(item.prefab, pos, Quaternion.identity);
                }
                else{
                    Vector3 pos = new Vector3(posicoesMapa[item.idx].x, posicoesMapa[item.idx].y + 1.2f, 0f);
                    Instantiate(item.prefab, pos, Quaternion.identity);
                } 
            }
            idx.Add(item.idx);
        }
    }

    void Awake()
    {
        FlorestaPrefab = Resources.Load<GameObject>("Cards/CardFloresta");
        CidadePrefab = Resources.Load<GameObject>("Cards/CardCidade");
        DesertoPrefab = Resources.Load<GameObject>("Cards/CardDeserto");
        NevadoPrefab = Resources.Load<GameObject>("Cards/CardNevado");
        VulcaoPrefab = Resources.Load<GameObject>("Cards/CardVulcao");
        PlayerPrefab = Resources.Load<GameObject>("Players/Delorean");
        DickPrefab = Resources.Load<GameObject>("Players/PersonagemDickVigarista");
        TrapPrefab = Resources.Load<GameObject>("Cards/Armadilhas");
        FinishPrefab = Resources.Load<GameObject>("Cards/Finish");

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
