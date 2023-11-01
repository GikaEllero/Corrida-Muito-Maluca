using Unity.VisualScripting;
using UnityEngine;

public class Background : MonoBehaviour
{
    private static GameObject atual;
    public static void AtualizaBackground(){
        Destroy(atual);
        var background = Instantiate(GameManager.AtualMapa(), new Vector3(-0.6f, -2f, 0), Quaternion.identity);
        background.transform.localScale = new Vector3(5, 5, 5);
        atual = background;
    }
    // Start is called before the first frame update
    void Start()
    {
        AtualizaBackground();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
