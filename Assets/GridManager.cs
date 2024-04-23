using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public Slider slider;

    public List<Sprite> Sprites = new List<Sprite>();
    public GameObject TilePrefab;
    public int GridDimension = 8;
    public float Distance = 1.0f;
    public GameObject[,] Grid { get; private set; }
    public bool IsShifting;
    public string worldGameOver;
    public int StartingMoves = 9;
    public GameObject GameOverMenu;
    public TextMeshProUGUI MovesText;
    public TextMeshProUGUI ScoreText;
    private int _numMoves;
    public int ScoreSave = 25;
    private int Save;
    private int _step;
    public GameObject effect;
    public GameObject star;
    public Image mage1;
    public Image mage2;
    public Image mage3;
    private int _a, _b, _c, Number = 3;
    public TextMeshProUGUI aValue;
    public TextMeshProUGUI bValue;
    public TextMeshProUGUI cValue;
    public TextMeshProUGUI LevelText;
    public GameObject Cat;
    public GameObject Bunny;
    public GameObject Pig;


    public static GridManager Instance { get; private set; }
    private int _score;
    public int NumMoves
    {
        get { return _numMoves; }
        set
        {
            _numMoves = value;
            MovesText.text = _numMoves.ToString();
        }
    }
    public int step
    {
        get { return _step; }
        set
        {
            _step = value;
            LevelText.text = _step.ToString() + "/" + "5";
        }
    }


    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            ScoreText.text = _score.ToString() + "/" + Save.ToString();
        }
    }

    public int a
    {
        get
        {
            return _a;
        }

        set
        {
            _a = value;
            aValue.text = _a.ToString() + "/" + Number.ToString();
        }
    }
    public int c
    {
        get
        {
            return _c;
        }

        set
        {
            _c = value;
            cValue.text = _c.ToString() + "/" + Number.ToString();
        }
    }
    public int b
    {
        get
        {
            return _b;
        }

        set
        {
            _b = value;
            bValue.text = _b.ToString() + "/" + Number.ToString();
        }
    }
    void Awake()
    {
        Instance = this;
        Score = 0;
        NumMoves = StartingMoves;
        mage1 = Cat.GetComponent<Image>();
        mage2 = Bunny.GetComponent<Image>();
        mage3 = Pig.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Save = ScoreSave;
        Grid = new GameObject[GridDimension, GridDimension];
        GameOverMenu.SetActive(false);
        InitGrid();
        slider.minValue = 0;
        slider.maxValue = 10;
        slider.onValueChanged.AddListener(delegate { Slider(); });
    }

    private float timeElapsed = 0f;

    private void Update()
    {
        Slider();
        timeElapsed += Time.deltaTime;

        if (Notification.Instance.Test == false && timeElapsed > 5f)
        {
            for (int row = 0; row < GridDimension; row++)
            {
                for (int column = 0; column < GridDimension; column++)
                {
                    Destroy(Grid[column, row]);
                }
            }
            InitGrid();
        }
        if (Score > Save || Score - Save >= -1)
        {
            NumMoves = +8;
            Save += 20;
            step++;
            timeElapsed = 0;
            Nextstep();
        }
        if (Input.GetMouseButton(0))
        {
            timeElapsed = 0;
        }
        if (timeElapsed >= 10) GameOver();
    }
    public void Slider()
    {
        if (step == 1) slider.value = 2;
        if (step == 2) slider.value = 4;
        if (step == 3) slider.value = 6;
        if (step == 4) slider.value = 8;
        if (step == 5) slider.value = 10;

    }
    void InitGrid()
    {
        Vector3 positionOffset = transform.position - new Vector3(GridDimension * Distance / 2.0f, GridDimension * Distance / 2.0f, 0);

        for (int row = 0; row < GridDimension; row++)
        {
            for (int column = 0; column < GridDimension; column++)
            {
                GameObject newTile = Instantiate(TilePrefab);
                List<Sprite> possibleSprites = new List<Sprite>(Sprites);
                //Choose what sprite to use for this cell
                Sprite left1 = GetSpriteAt(column - 1, row);
                Sprite left2 = GetSpriteAt(column - 2, row);
                if (left2 != null && left1 == left2)
                {
                    possibleSprites.Remove(left1);
                }
                Sprite down1 = GetSpriteAt(column, row - 1);
                Sprite down2 = GetSpriteAt(column, row - 2);
                if (down2 != null && down1 == down2)
                {
                    possibleSprites.Remove(down1);
                }
                SpriteRenderer renderer = newTile.GetComponent<SpriteRenderer>();
                renderer.sprite = possibleSprites[Random.Range(0, possibleSprites.Count)];
                Tile tile = newTile.AddComponent<Tile>();
                tile.Position = new Vector2Int(column, row);
                newTile.transform.parent = transform;
                newTile.transform.position = new Vector3(column * Distance, row * Distance, 0) + positionOffset;
                Grid[column, row] = newTile;
            }
        }
    }

    Sprite GetSpriteAt(int column, int row)
    {
        if (column < 0 || column >= GridDimension
         || row < 0 || row >= GridDimension)
            return null;
        GameObject tile = Grid[column, row];
        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
        return renderer.sprite;
    }

    SpriteRenderer GetSpriteRendererAt(int column, int row)
    {
        if (column < 0 || column >= GridDimension
         || row < 0 || row >= GridDimension)
            return null;
        GameObject tile = Grid[column, row];
        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
        return renderer;
    }


    public void SwapTiles(Vector2Int tile1Position, Vector2Int tile2Position)
    {
        GameObject tile1 = Grid[tile1Position.x, tile1Position.y];
        SpriteRenderer renderer1 = tile1.GetComponent<SpriteRenderer>();

        GameObject tile2 = Grid[tile2Position.x, tile2Position.y];
        SpriteRenderer renderer2 = tile2.GetComponent<SpriteRenderer>();


        GameObject spawn = Instantiate(effect, tile1.transform.position, tile1.transform.rotation);
        StartCoroutine(MoveTo(spawn.transform, star.transform.position));
        Destroy(spawn, 4f);

        Sprite temp = renderer1.sprite;
        renderer1.sprite = renderer2.sprite;
        renderer2.sprite = temp;

        bool changesOccurs = CheckMatches();
        if (!changesOccurs)
        {
            temp = renderer1.sprite;
            renderer1.sprite = renderer2.sprite;
            renderer2.sprite = temp;
        }
        else
        {
            NumMoves--;
            do
            {
                FillHoles();

            } while (CheckMatches());
            if (NumMoves <= 0)
            {
                NumMoves = 0;
                GameOver();
            }
        }
    }
    bool CheckMatches()
    {
        List<SpriteRenderer> matchedSprites = new List<SpriteRenderer>();
        for (int row = 0; row < GridDimension; row++)
        {
            for (int column = 0; column < GridDimension; column++)
            {
                SpriteRenderer current = GetSpriteRendererAt(column, row);

                List<SpriteRenderer> horizontalMatches = FindColumnMatchForTile(column, row, current.sprite);
                List<SpriteRenderer> verticalMatches = FindRowMatchForTile(column, row, current.sprite);

                if (horizontalMatches.Count >= 2)
                {
                    matchedSprites.AddRange(horizontalMatches);
                    matchedSprites.Add(current);
                }

                if (verticalMatches.Count >= 2)
                {
                    matchedSprites.AddRange(verticalMatches);
                    matchedSprites.Add(current);
                }
            }
        }


        if (matchedSprites.Count > 0 && matchedSprites[1].sprite == mage1.sprite)
        {
            a += matchedSprites.Count;

        }
        if (matchedSprites.Count > 0 && matchedSprites[1].sprite == mage2.sprite)
        {
            b += matchedSprites.Count;

        }
        if (matchedSprites.Count > 0 && matchedSprites[1].sprite == mage3.sprite)
        {
            c += matchedSprites.Count;

        }
        foreach (SpriteRenderer renderer in matchedSprites)
        {
            renderer.sprite = null; // Consider handling matched sprites differently
        }

        Score += matchedSprites.Count;
        return matchedSprites.Count > 0;
    }


    List<SpriteRenderer> FindColumnMatchForTile(int col, int row, Sprite sprite)
    {
        List<SpriteRenderer> result = new List<SpriteRenderer>();
        for (int i = col + 1; i < GridDimension; i++)
        {
            SpriteRenderer nextColumn = GetSpriteRendererAt(i, row);
            if (nextColumn.sprite != sprite)
            {
                break;
            }
            result.Add(nextColumn);
        }
        return result;
    }

    List<SpriteRenderer> FindRowMatchForTile(int col, int row, Sprite sprite)
    {
        List<SpriteRenderer> result = new List<SpriteRenderer>();
        for (int i = row + 1; i < GridDimension; i++)
        {
            SpriteRenderer nextRow = GetSpriteRendererAt(col, i);
            if (nextRow.sprite != sprite)
            {
                break;
            }
            result.Add(nextRow);
        }
        return result;
    }

    void FillHoles()
    {
        for (int column = 0; column < GridDimension; column++)
            for (int row = 0; row < GridDimension; row++)
            {
                while (GetSpriteRendererAt(column, row).sprite == null)
                {
                    SpriteRenderer current = GetSpriteRendererAt(column, row);
                    SpriteRenderer next = current;
                    for (int filler = row; filler < GridDimension - 1; filler++)
                    {
                        next = GetSpriteRendererAt(column, filler + 1);
                        current.sprite = next.sprite;
                        current = next;
                    }
                    next.sprite = Sprites[Random.Range(0, Sprites.Count)];
                }
            }
    }

    void GameOver()
    {
        Debug.Log("GAME OVER");
        PlayerPrefs.SetInt("score", Score);
        GameOverMenu.SetActive(true);

        SceneManager.LoadScene(worldGameOver);
    }

    void Nextstep()
    {

        if (step > 5)
        {
            Debug.Log("WIN GAME");
            PlayerPrefs.SetInt("score", Score);
            GameOverMenu.SetActive(true);
            SceneManager.LoadScene("LevelComplete");
        }
        else
        {
            for (int row = 0; row < GridDimension; row++)
                for (int column = 0; column < GridDimension; column++)
                {
                    Destroy(Grid[column, row]);
                }
            InitGrid();
        }
    }
    private IEnumerator MoveTo(Transform subject, Vector3 position)
    {
        while (Vector3.Distance(subject.position, position) > 0.1f)
        {
            subject.position = Vector3.Lerp(subject.position, position, 6 * Time.deltaTime);

            yield return null;
        }

        subject.position = position;
    }
}
