using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    public Button Again;
    public Button Infor;
    public Button Connect;


    private void Awake()
    {
        Instance = this;
        // Get Button and Slider components
        Again = GameObject.Find("Again").GetComponent<Button>();

        Infor = GameObject.Find("Infor").GetComponent<Button>();

        Connect = GameObject.Find("Connect").GetComponent<Button>();
        Start();
    }

    private void Start()
    {
        Again.onClick.AddListener(AgainFunc);
        Infor.onClick.AddListener(InforOnclick);
        Connect.onClick.AddListener(ConnectOnclick);
    }
    public void AgainFunc()
    {
        SceneManager.LoadScene("AnimalWorld");

    }
    public void InforOnclick()
    {
        SceneManager.LoadScene("Informationplayer");

    }

    public void ConnectOnclick()
    {
        SceneManager.LoadScene("Friend");
    }
}
