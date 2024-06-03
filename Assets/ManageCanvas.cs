using UnityEngine;

public class ManageCanvas : MonoBehaviour
{
    public Canvas home;
    public Canvas play;
    public Canvas lost;
    public Canvas win;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SetActiveCanvas(home);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            SetActiveCanvas(play);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SetActiveCanvas(lost);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetActiveCanvas(win);
        }
    }

    private void SetActiveCanvas(Canvas activeCanvas)
    {
        home.gameObject.SetActive(activeCanvas == home);
        play.gameObject.SetActive(activeCanvas == play);
        lost.gameObject.SetActive(activeCanvas == lost);
        win.gameObject.SetActive(activeCanvas == win);
    }
}
