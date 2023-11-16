using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Traps : MonoBehaviour
{
    private GameObject TunelPrefab;
    private GameObject TroncoPrefab;
    private GameObject SabotagemPrefab;
    private GameObject TrocaPrefab;
    private GameObject RochaPrefab;
    private GameObject PlanoPrefab;
    private GameObject OleoPrefab;
    private int idxTrap;

    public void Acao(int pos, bool isNpc){
        if(idxTrap == 0){
            Debug.Log("Tunel");

            var npcsOrdenados = GameManager.npcs.OrderBy(o => o.idx).ToList();
            if(npcsOrdenados[0].idx > GameManager.atualPlayer){
                GameManager.AvancaPlayer(true);
                GameManager.AvancaNpc(npcsOrdenados[0].idx);
            }
            else if(npcsOrdenados[1].idx > GameManager.atualPlayer){
                GameManager.AvancaPlayer(true);
                GameManager.AvancaNpc(npcsOrdenados[0].idx);
            }
            else{
                GameManager.AvancaNpc(npcsOrdenados[0].idx);
                GameManager.AvancaNpc(npcsOrdenados[1].idx);
            }
        }
        else if(idxTrap == 1){
            Debug.Log("Tronco");

            if(isNpc){
                GameManager.RecuaNpc(pos);
                GameManager.RecuaNpc(pos);
            }
            else{
                GameManager.RecuaPlayer();
                GameManager.RecuaPlayer();
            }
        }
        else if(idxTrap == 2){
            Debug.Log("Sabotagem");

            if(!isNpc){
                GameManager.DesativaSkill();
            }
        }
        else if(idxTrap == 3){
            Debug.Log("Troca");

            var npcsOrdenados = GameManager.npcs.OrderByDescending(o => o.idx).ToList();
            int npc;
            if(isNpc)
                npc = GameManager.npcs.IndexOf(npcsOrdenados.Where(w => w.idx < GameManager.npcs[pos].idx).FirstOrDefault());
            else{
                npc = npcsOrdenados.Where(w => w.idx < pos).FirstOrDefault().idx;
            }

            if(isNpc){
                if(GameManager.atualPlayer > GameManager.npcs[npc].idx && 
                    GameManager.atualPlayer < GameManager.npcs[pos].idx){
                    int aux = GameManager.npcs[pos].idx;
                    GameManager.npcs[pos].idx = GameManager.atualPlayer;
                    GameManager.atualPlayer = aux;
                }
                else{
                    int aux = GameManager.npcs[npc].idx;
                    GameManager.npcs[npc].idx = GameManager.npcs[pos].idx;
                    GameManager.npcs[pos].idx = aux;
                }
            }
            else{
                int aux = GameManager.npcs[npc].idx;
                GameManager.npcs[npc].idx = GameManager.atualPlayer;
                GameManager.atualPlayer = aux;
            }
        }
        else if(idxTrap == 4){
            Debug.Log("Rocha");

            if(isNpc){
                var idxs = GameManager.npcs.Select(s => s.idx).ToList();
                idxs.Add(GameManager.atualPlayer);
                int livre = GameManager.mapCards.OrderByDescending(o => o.idx).Where(w => w.idx < GameManager.npcs[pos].idx && !idxs.Contains(w.idx)).FirstOrDefault().idx;
                GameManager.npcs[pos].idx = livre;
            }
            else{
                var idxs = GameManager.npcs.Select(s => s.idx).ToList();
                int livre = GameManager.mapCards.OrderByDescending(o => o.idx).Where(w => w.idx < pos && !idxs.Contains(w.idx)).FirstOrDefault().idx;
                GameManager.atualPlayer = livre;
            }
        }
        else if(idxTrap == 5){
            Debug.Log("Plano");

            if(!isNpc){
                GameManager.AtivaSkill();
            }
        }
        else if(idxTrap == 6){
            Debug.Log("Óleo");

            if(isNpc){
                GameManager.AvancaPlayer(true);
                for (int i = 0; i < GameManager.npcs.Count; i++)
                {
                    if(i != pos)
                        GameManager.AvancaNpc(i);
                }
            }
            else{
                for (int i = 0; i < GameManager.npcs.Count; i++)
                {
                    GameManager.AvancaNpc(i);
                }
            }
        }
    }

    public void GeraTrap(int pos, bool isNpc){
        System.Random random = new System.Random();
        int num = random.Next(0, 7);
        idxTrap = num;
        Acao(pos, isNpc);
    }

    public void MostraTrap(GameObject prefab){
        Vector3 pos = new Vector3(0f, -4.5f, 0f);
        Instantiate(prefab, pos, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        TunelPrefab = Resources.Load<GameObject>("Armadilhas/TunelDesenhado");
        TroncoPrefab = Resources.Load<GameObject>("Armadilhas/Tronco");
        SabotagemPrefab = Resources.Load<GameObject>("Armadilhas/Sabotagem");
        TrocaPrefab = Resources.Load<GameObject>("Armadilhas/TrocaTroca");
        RochaPrefab = Resources.Load<GameObject>("Armadilhas/Rocha");
        PlanoPrefab = Resources.Load<GameObject>("Armadilhas/PlanoFracassado");
        OleoPrefab = Resources.Load<GameObject>("Armadilhas/Oleo");
        
        if(SceneManager.GetActiveScene().name == "Armadilha"){
            switch (idxTrap)
            {
                case 0:
                    MostraTrap(TunelPrefab);
                    break;

                case 1:
                    MostraTrap(TroncoPrefab);
                    break;

                case 2:
                    MostraTrap(SabotagemPrefab);
                    break;

                case 3:
                    MostraTrap(TrocaPrefab);
                    break;

                case 4:
                    MostraTrap(RochaPrefab);
                    break;

                case 5:
                    MostraTrap(PlanoPrefab);
                    break;

                case 6:
                    MostraTrap(OleoPrefab);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
