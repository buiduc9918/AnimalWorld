using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    public Button Button;
    // Start is called before the first frame update
    void Start()
    {
        Button = GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void call()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
