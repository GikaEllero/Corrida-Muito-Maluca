using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skill : MonoBehaviour
{
    private bool selected = false;
    public bool used = false;
    public GameObject prefab;

    public Skill(GameObject prefab){
        this.prefab = prefab;
    }

    public bool GetSelected(){
        return selected;
    }

    public void OnMouseDown(){
        if(SceneManager.GetActiveScene().name == "SelecionaHabilidades"){
            if(selected){
                transform.position += Vector3.down * 1f;
                selected = false;
                foreach (var item in Habilidades.skillsPrefab)
                {
                    if(gameObject.name.Contains(item.prefab.name))
                        GameManager.skills.Remove(item);
                }
            }
            else{
                if(GameManager.skills.Count <= 3){
                    transform.position += Vector3.up * 1f;
                    selected = true;
                    foreach (var item in Habilidades.skillsPrefab)
                    {
                        if(gameObject.name.Contains(item.prefab.name))
                            GameManager.skills.Add(item);
                    }
                }
            }
        }
        if(SceneManager.GetActiveScene().name == "Skills"){
            if(selected){
                transform.position += Vector3.down * 1f;
                foreach (var item in GameManager.skills)
                {
                    if(gameObject.name.Contains(item.prefab.name))
                        item.selected = false;
                }
                selected = false;
            }
            else{
                transform.position += Vector3.up * 1f;
                foreach (var item in GameManager.skills)
                {
                    if(gameObject.name.Contains(item.prefab.name))
                        item.selected = true;
                }
                selected = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Skills"){
            selected = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
