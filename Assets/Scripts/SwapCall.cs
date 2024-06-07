using System;
using Tiles;
using UnityEngine;

public class SwapCall : MonoBehaviour
{
    public static SwapCall Instace;
    public GameObject Object1;
    public GameObject Object2;
    void Start()
    {
        // HoanDoiDoiTuong();
    }

    // Phương thức hoán đổi đối tượng dựa trên trạng thái hiện tại
    public void HoanDoiDoiTuong()
    {
        try
        {
            if (Object1 == null)
            {
                Object1 = Blocks.Selected.gameObject;
            }
            else
            {
                if (Object2 == null)
                {
                    Object2 = Blocks.Selected.gameObject;
                    //if (Vector2Int.Distance(Object1.GetComponent<Blocks>().seque, Object2.GetComponent<Blocks>().seque) == 1)
                    //{
                    //    Debug.Log("Swapping tiles: " + Object1.GetComponent<Blocks>().seque + " with " + Object2.GetComponent<Blocks>().seque);
                    //    GridManager.Instance.SwapTiles(Object1.GetComponent<Blocks>().seque, Object2.GetComponent<Blocks>().seque);
                    //    Object1 = Object2;
                    //    Object2 = null;
                    //}
                }
                else
                {
                    //if (Vector2Int.Distance(Object1.GetComponent<Blocks>().seque, Object2.GetComponent<Blocks>().seque) == 1)
                    //{
                    //    Debug.Log("Swapping tiles: " + Object1.GetComponent<Blocks>().seque + " with " + Object2.GetComponent<Blocks>().seque);
                    //    GridManager.Instance.SwapTiles(Object1.GetComponent<Blocks>().seque, Object2.GetComponent<Blocks>().seque);
                    //    Object1 = Object2;
                    //    Object2 = null;
                    //}
                    //else
                    //{

                    Object1 = Object2;
                    Object2 = null;
                    //}
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    // Ví dụ phương thức để thay đổi đối tượng trong thời gian chạy
    void Update()
    {
        //  HoanDoiDoiTuong();
        //// Ví dụ: Hoán đổi đối tượng khi nhấn chuột trái
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // HoanDoiDoiTuong();
        }
    }
}
