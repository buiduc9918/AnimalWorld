using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button start;
    public Button setup;
    public Button champion;
    public Button shop;
    public string setupSence;
    public string playgame;
    public string shopSence;
    public string championSence;

    void Start()
    {
        start = GetComponent<Button>();
        start.onClick.AddListener(Playgame);
        setup = GetComponent<Button>();
        setup.onClick.AddListener(SetupFunc);
        champion = GetComponent<Button>();
        champion.onClick.AddListener(xChampion);
        shop = GetComponent<Button>();
        shop.onClick.AddListener(xShop);

    }

    // Update is called once per frame
    public void Playgame()
    {
        SceneManager.LoadScene(playgame);

    }
    public void SetupFunc(){
        SceneManager.LoadScene(setupSence);
    }
    public void xChampion(){
        SceneManager.LoadScene(championSence);
        
    }
     public void xShop(){
        SceneManager.LoadScene(shopSence);
        
    }
}
