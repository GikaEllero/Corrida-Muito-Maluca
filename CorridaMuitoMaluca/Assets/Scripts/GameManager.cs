using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static Cards cards;
    private static TextUpdate text;
    public static List<(int idx, GameObject card)> playerCards = new List<(int, GameObject)>();
    public static List<(int idx, GameObject card)> mapCards = new List<(int, GameObject)>();
    public static List<int> freeIdxCards = new List<int>(); 
    public static List<Npc> npcs = new List<Npc>(); 
    public static int atualPlayer = 0;
    private static bool firstCard = true;

    public static GameObject AtualMapa(){
        return mapCards[atualPlayer].card;
    }

    public static void FinalizarTurno(){
        text.Clear();
        if(playerCards.Count < 3){
            foreach (var item in freeIdxCards.ToList())
            {
                freeIdxCards.Remove(item);
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
        Background.AtualizaBackground();
    }

    public static void Avanca(){
        bool certo = false;

        while(!certo){
            atualPlayer++;
            if(npcs.Where(w => w.idx == atualPlayer).Count() <= 1){
                certo = true;
            }
        }
        
    }

    public static void SelecionarCarta(){
        var cardIdx = GetCardSelected();
        text.Clear();
        if(cardIdx != null){
            if(firstCard){
                freeIdxCards.Add((int)cardIdx);
                var card = playerCards.Where(w => w.idx == cardIdx).FirstOrDefault();
                Destroy(card.card);
                playerCards.Remove(card);
                firstCard = false;
                Avanca();
                Background.AtualizaBackground();
            }
            else{
                var card = playerCards.Where(w => w.idx == cardIdx).FirstOrDefault();
                if(card.card.name.Contains(mapCards[atualPlayer].card.name)){
                    freeIdxCards.Add((int)cardIdx);
                    Destroy(card.card);
                    playerCards.Remove(card);
                    Avanca();
                    Background.AtualizaBackground();
                }
                else{
                    text.CardError();
                }
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
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if(mapCards.Count == 0){
            Map.IniciaMapa();
        }
        
        cards = GameObject.Find("Cards").GetComponent<Cards>();
        text = GameObject.Find("Text").GetComponent<TextUpdate>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
