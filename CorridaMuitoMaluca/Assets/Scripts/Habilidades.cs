using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Habilidades : MonoBehaviour
{
    public List<GameObject> skills;
    public static List<Skill> skillsPrefab = new List<Skill>();
    public static Cards cards = new Cards();

    public static void UsaHabilidade(Skill skill){
        Debug.Log(skill.prefab.name);
        if(!skill.used){
            if(skill.prefab.name.Contains("Speed")){
                GameManager.AvancaPlayer(false);
                GameManager.AvancaPlayer(false);
                skill.used = true;
            }
            else if(skill.prefab.name.Contains("Rodas")){
                Debug.Log("Entrou roda");
                GameManager.AvancaPlayer(true);
                GameManager.AvancaPlayer(true);
                GameManager.AvancaPlayer(true);
                skill.used = true;
            }
            else if(skill.prefab.name.Contains("Cassino")){
                var card = cards.TiraCarta();

                for (int i = 0; i < GameManager.mapCards.Count; i++)
                {
                    if(card.Contains(GameManager.mapCards[i].card.name) && GameManager.mapCards[i].idx > GameManager.atualPlayer){
                        if(GameManager.traps.Count > 0){
                            if(!GameManager.traps.Where(w => w == i).Any()){
                                GameManager.atualPlayer = i;
                                break;
                            }
                        }
                        else{
                            GameManager.atualPlayer = i;
                            break;
                        }
                    }
                }
                skill.used = true;
            }
            else if(skill.prefab.name.Contains("Familia")){
                var npcsOrdenados = GameManager.npcs.OrderByDescending(o => o.idx).ToList();
                if(GameManager.atualPlayer < npcsOrdenados[0].idx){
                    GameManager.AvancaPlayer(true);
                    GameManager.AvancaPlayer(true);
                    GameManager.AvancaPlayer(true);
                    GameManager.AvancaPlayer(true);

                    for (int i = 0; i < GameManager.npcs.Count; i++)
                    {
                        GameManager.AvancaNpc(i);
                    }
                }
                skill.used = true;
            }
            else if(skill.prefab.name.Contains("Mapa")){
                for (int i = GameManager.atualPlayer + 1; i < GameManager.atualPlayer + 5; i++)
                {
                    if(GameManager.mapCards[i].card.name.Contains("Floresta")){
                        GameManager.atualPlayer = i;
                        GameManager.CaiuTrap(GameManager.atualPlayer, false);
                        break;
                    }
                }
                skill.used = true;
            }
            else if(skill.prefab.name.Contains("Katiau")){
                var npcs = GameManager.npcs.Select(s => s.idx).ToList();
                for (int i = GameManager.atualPlayer + 1; i < GameManager.mapCards.Count; i++)
                {
                    if(!npcs.Contains(i)){
                        GameManager.atualPlayer = i;
                        GameManager.CaiuTrap(GameManager.atualPlayer, false);
                        break;
                    }
                }
                skill.used = true;
            }
            else if(skill.prefab.name.Contains("Atrasado")){
                var npcsOrdenados = GameManager.npcs.OrderBy(o => o.idx).ToList();
                if(GameManager.atualPlayer > npcsOrdenados[0].idx){
                    GameManager.AvancaPlayer(true);
                    GameManager.AvancaPlayer(true);
                    GameManager.AvancaPlayer(true);
                    GameManager.AvancaPlayer(true);
                }
                skill.used = true;
            }
            else if(skill.prefab.name.Contains("Senhora")){
                var npcsOrdenados = GameManager.npcs.OrderByDescending(o => o.idx).ToList();
                GameManager.RecuaNpc(GameManager.npcs.IndexOf(npcsOrdenados[0]));
                GameManager.CaiuTrap(GameManager.npcs.IndexOf(npcsOrdenados[0]), true);
                skill.used = true;
            }
            else if(skill.prefab.name.Contains("Confia")){
                var npcsOrdenados = GameManager.npcs.OrderByDescending(o => o.idx).ToList();
                GameManager.AvancaNpc(GameManager.npcs.IndexOf(npcsOrdenados[0]));
                GameManager.CaiuTrap(GameManager.npcs.IndexOf(npcsOrdenados[0]), true);
                skill.used = true;
            }
            else if(skill.prefab.name.Contains("Explorador")){
                if(GameManager.traps.Where(w => w == GameManager.atualPlayer + 1).Any()){
                    GameManager.traps.Remove(GameManager.atualPlayer + 1);
                    GameManager.traps.Add(GameManager.atualPlayer - 1);

                    for (int i = 0; i < GameManager.npcs.Count; i++)
                    {
                        GameManager.CaiuTrap(i, true);
                    }
                }
                skill.used = true;
            }
        } 
    }

    public void MostraHabilidades(){
        float x = -6.7f;
        foreach (var item in GameManager.skills)
        {
            Vector3 pos = new Vector3(x, -3f, 0f);
            var skill = Instantiate(item.prefab, pos, Quaternion.identity);
            skill.transform.localScale = new Vector3(2f, 2f, 2f);
            x += 4.5f;
        }
    }

    public void MostraTodasHabilidades(){
        foreach (var item in skills)
        {
            skillsPrefab.Add(new Skill(item));
            Instantiate(item);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "SelecionaHabilidades"){
            MostraTodasHabilidades();
        }
        if(SceneManager.GetActiveScene().name == "Skills"){
            MostraHabilidades();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
