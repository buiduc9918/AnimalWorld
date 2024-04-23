using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController2 : MonoBehaviour
{
    public static UIController2 Instance { get; private set; }
    public Button Again;
    public Button Infor;
    public Button Share;
    public Button NextLevel;



    private void Awake()
    {
        Instance = this;
        // Get Button and Slider components
        Again = GameObject.Find("Again").GetComponent<Button>();

        Infor = GameObject.Find("Infor").GetComponent<Button>();

        Share = GameObject.Find("Share").GetComponent<Button>();
        NextLevel = GameObject.Find("NextLevel").GetComponent<Button>();

        Start();
    }

    private void Start()
    {
        Again.onClick.AddListener(AgainFunc);
        Infor.onClick.AddListener(InforOnclick);
        Share.onClick.AddListener(ShareOnclick);
        NextLevel.onClick.AddListener(NextLevelOnclick);

    }
    public void AgainFunc()
    {
        SceneManager.LoadScene("AnimalWorld");

    }
    public void InforOnclick()
    {
        SceneManager.LoadScene("Informationplayer");

    }

    public void ShareOnclick()
    {
        SceneManager.LoadScene("Friend");
    }
    public void NextLevelOnclick()
    {
        SceneManager.LoadScene("ManagermentScene");
    }
}
