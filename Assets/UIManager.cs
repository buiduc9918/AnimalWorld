using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Set up Buttons
    public static UIManager Instance { get; private set; }
    public Button pause;
    public Button exit;
    public bool PauseStatus;
    public Sprite[] SpriteButton;
    public Image ButtonImage;
    public Image ButtonImageExit;

    private void Awake()
    {
        Instance = this;
        // Get Button and Slider components
        pause = GameObject.Find("PauseButton").GetComponent<Button>();
        exit = GameObject.Find("ExitButton").GetComponent<Button>();
        ButtonImage = pause.GetComponent<Image>();
        ButtonImageExit = exit.GetComponent<Image>();
        PauseStatus = false;
        Start();
    }

    private void Start()
    {
        pause.onClick.AddListener(PauseOnclick);
        exit.onClick.AddListener(Exit);
    }
    public void PauseOnclick()
    {
        PauseStatus = !PauseStatus;
        Time.timeScale = PauseStatus ? 0f : 1f;
        ButtonImage.sprite = PauseStatus ? SpriteButton[0] : SpriteButton[1];
    }

    public void Exit()
    {
        SceneManager.LoadScene("AnimalWorld");
    }
}
