using UnityEngine;

public class Tile : MonoBehaviour
{
    public static Tile selected;
    public static Tile selected1;
    private SpriteRenderer Renderer;
    public Vector2 Position;

    private void Start()
    {
        selected = this;
        selected1 = this;
        Renderer = GetComponent<SpriteRenderer>();
    }

    public void Select()
    {
        Renderer.color = Color.grey;
    }

    public void Unselect()
    {
        Renderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            if (selected != null)
            {
                if (selected == this)
                    return;
                selected.Unselect();

                if (AreAdjacent(selected1.Position, Position))
                {
                    Vector2Int x = new Vector2Int(Mathf.RoundToInt(Position.x), Mathf.RoundToInt(Position.y));
                    Vector2Int y = new Vector2Int(Mathf.RoundToInt(selected1.Position.x), Mathf.RoundToInt(selected1.Position.y));
                    GridManager.Instance.SwapTiles(x, y);
                    selected1 = selected;
                    selected = this;
                }
                else saveclick();

            }
            else saveclick();
        }
    }
    public void saveclick()
    {
        selected1 = selected;
        selected = this;
        Select();

    }
    bool AreAdjacent(Vector2 a, Vector2 b)
    {
        // Calculate the absolute difference in coordinates
        float dx = Mathf.Abs(a.x - b.x);
        float dy = Mathf.Abs(a.y - b.y);

        // Check if the absolute difference in one coordinate is 1 and the other is 0
        return (dx == 1 && dy == 0) || (dx == 0 && dy == 1);
    }

}
