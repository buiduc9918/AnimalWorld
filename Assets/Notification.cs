using UnityEngine;

public class Notification : MonoBehaviour
{
    private bool _test;
    public bool Test
    {
        get { return _test; }
        set { _test = value; }
    }
    private GameObject[,] Grid;
    private int GridDimension { get; set; }
    public static Notification Instance { get; private set; }
    // public GridManager gridManager;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        // gridManager = GameObject.Find("Grid").GetComponent<GridManager>();

        Grid = GridManager.Instance.Grid;
        GridDimension = GridManager.Instance.GridDimension;
        chuyen();
        InvokeRepeating(nameof(CheckCanMatches), 6f, 0.02f);
    }
    void chuyen()
    {
        Debug.Log(GridDimension.ToString());

        for (int row = 0; row < GridDimension; row++)
        {
            for (int column = 0; column < GridDimension; column++)
            {
                Debug.Log(Grid[column, row].GetComponent<SpriteRenderer>().sprite.ToString());
            }
        }
    }
    void CheckCanMatches()
    {

        int x = 0;
        for (int row = 0; row < GridDimension; row++)
        {
            for (int column = 0; column < GridDimension; column++)
            {
                if (FindForTile(column, row)) x++;
            }
        }
        Test = x > 0 ? true : false;
        Debug.Log(Test.ToString());
    }
    bool FindForTile(int column, int row)
    {
        int maths = 0;
        SpriteRenderer main = Grid[column, row].GetComponent<SpriteRenderer>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue; // Bỏ qua ô hiện tại
                int neighborColumn = column + i;
                int neighborRow = row + j;

                if (IsValidPosition(neighborColumn, neighborRow))
                {
                    SpriteRenderer neighbor = Grid[neighborColumn, neighborRow].GetComponent<SpriteRenderer>();
                    if (main.sprite == neighbor.sprite) maths++;
                }
            }
        }

        return maths > 2;
    }

    bool IsValidPosition(int column, int row)
    {
        return column >= 0 && column < GridDimension && row >= 0 && row < GridDimension;
    }


}
/*
bool FindForTile(int column, int row)
    {
        int maths = 0;

        SpriteRenderer main = Grid[column, row].GetComponent<SpriteRenderer>();
        int rightColumn = column + 1;
        int leftColumn = column - 1;
        int upRow = row + 1;
        int downRow = row - 1;
        if (rightColumn < GridDimension)
        {
            SpriteRenderer main1 = Grid[rightColumn, row].GetComponent<SpriteRenderer>();
            if (main.sprite == main1.sprite) maths++;
        }
        if (leftColumn >= 0)
        {
            SpriteRenderer main2 = Grid[leftColumn, row].GetComponent<SpriteRenderer>();
            if (main.sprite == main2.sprite) maths++;
        }
        if (upRow < GridDimension)
        {
            SpriteRenderer main3 = Grid[column, upRow].GetComponent<SpriteRenderer>();
            if (main.sprite == main3.sprite) maths++;
        }
        if (downRow >= 0)
        {
            SpriteRenderer main4 = Grid[column, downRow].GetComponent<SpriteRenderer>();
            if (main.sprite == main4.sprite) maths++;
        }
        if (rightColumn < GridDimension && leftColumn >= 0)
        {
            SpriteRenderer main1 = Grid[rightColumn, row].GetComponent<SpriteRenderer>();
            SpriteRenderer main2 = Grid[leftColumn, row].GetComponent<SpriteRenderer>();
            if (main1.sprite == main2.sprite) maths++;
        }
        if (rightColumn < GridDimension && upRow < GridDimension)
        {
            SpriteRenderer main1 = Grid[rightColumn, row].GetComponent<SpriteRenderer>();
            SpriteRenderer main3 = Grid[column, upRow].GetComponent<SpriteRenderer>();
            if (main1.sprite == main3.sprite) maths++;
        }
        if (rightColumn < GridDimension && downRow >= 0)
        {
            SpriteRenderer main1 = Grid[rightColumn, row].GetComponent<SpriteRenderer>();
            SpriteRenderer main4 = Grid[column, downRow].GetComponent<SpriteRenderer>();
            if (main1.sprite == main4.sprite) maths++;
        }
        if (leftColumn >= 0 && upRow < GridDimension)
        {
            SpriteRenderer main3 = Grid[column, upRow].GetComponent<SpriteRenderer>();
            SpriteRenderer main2 = Grid[leftColumn, row].GetComponent<SpriteRenderer>();
            if (main2.sprite == main3.sprite) maths++;
        }

        if (leftColumn >= 0 && downRow >= 0)
        {
            SpriteRenderer main2 = Grid[leftColumn, row].GetComponent<SpriteRenderer>();
            SpriteRenderer main4 = Grid[column, downRow].GetComponent<SpriteRenderer>();
            if (main2.sprite == main4.sprite) maths++;
        }
        if (downRow >= 0 && upRow < GridDimension)
        {
            SpriteRenderer main3 = Grid[column, upRow].GetComponent<SpriteRenderer>();
            SpriteRenderer main4 = Grid[column, downRow].GetComponent<SpriteRenderer>();

            if (main3.sprite == main4.sprite) maths++;
        }
        return maths > 2;
    }
*/