using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static Cards cards;
    private static Map map;

    public static List<(int idx, GameObject card)> playerCards = new List<(int, GameObject)>();
    public static List<GameObject> mapCards = new List<GameObject>();
    public static List<int> freeIdx = new List<int>(); 
    private static int atualPlayer = 0;
    private static bool firstCard = true;
    

    public static void FinalizarTurno(){
        if(playerCards.Count < 3){
            foreach (var item in freeIdx.ToList())
            {
                freeIdx.Remove(item);
                switch (item)
                {
                    case 1:
                        cards.GeraCarta(-3.8f, -4.7f, 1);
                        break;
                    case 2:
                        cards.GeraCarta(-0.5f, -4.7f, 2);
                        break;
                    case 3:
                        cards.GeraCarta(2.8f, -4.7f, 3);
                        break;
                }
            }
        }

        firstCard = true;
    }

    public static void SelecionarCarta(){
        var cardIdx = GetCardSelected();
        if(cardIdx != null){
            if(firstCard){
                freeIdx.Add((int)cardIdx);
                var card = playerCards.Where(w => w.idx == cardIdx).FirstOrDefault();
                Destroy(card.card);
                playerCards.Remove(card);
                firstCard = false;
                atualPlayer++;
            }
            else{
                var card = playerCards.Where(w => w.idx == cardIdx).FirstOrDefault();
                /*if(card.card.name == mapCards[atualPlayer].name){
                    freeIdx.Add((int)cardIdx);
                    Destroy(card.card);
                    playerCards.Remove(card);
                    atualPlayer++;
                }*/
            }
        }
    }

    public static int? GetCardSelected(){
        foreach (var item in playerCards)
        {
            if(item.card.GetComponent<Card>().GetSelected())
                return item.idx;
        }
        return null;
    }

    void Awake(){
        if(mapCards.Count == 0){
            Map.IniciaMapa();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cards = GameObject.Find("Cards").GetComponent<Cards>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
