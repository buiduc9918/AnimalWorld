using UnityEngine;
using UnityEngine.EventSystems;

public class Blocks : MonoBehaviour, IPointerDownHandler
{
    public static Blocks selected;
    public Vector2Int Position;
    public void Select()
    {
        transform.localScale = Vector3.one * 1.3f;
    }

    public void Unselect()
    {
        transform.localScale = Vector3.one * 1.5f;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            if (selected != null)
            {
                if (selected == this)
                    return;

                selected.Unselect();
                Debug.Log(selected.name + this.name);
                Debug.Log(selected.Position.x.ToString() + selected.Position.y.ToString());
                Debug.Log(Position.x.ToString() + Position.y.ToString());


                if (Vector2Int.Distance(selected.Position, Position) == 1)
                {
                    Debug.Log("check 1");
                    GridManager.instance.SwapTiles(selected.Position, Position);
                    selected = this;
                    Select();
                }
                else
                {
                    SaveClick();
                }
            }
            else
            {
                SaveClick();
            }
        }
    }
    private void SaveClick()
    {
        selected = this;
        Select();
    }

}
