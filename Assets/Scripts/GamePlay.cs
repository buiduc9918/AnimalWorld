using UnityEngine;

namespace Grid
{
    public class GamePlay : MonoBehaviour
    {
        private GameObject[,] grid;
        private int tableWidth;
        private int tableHeight;

        public void Start()
        {
            PopulateTable();
        }

        // tạo bảng, điền các object vào chỗ trống
        public void PopulateTable()
        {

        }

        // tìm và xóa các đối tượng đã được ăn
        // trả về true nếu có xóa
        // trả về false nếu không xóa
        public bool Match()
        {
            // thuật toán
            return false;
        }

        public void OnTouch()
        {
            // tùy theo hướng touch mà tính toán đổi chỗ
        }

        // đổi chỗ object từ tọa độ (fromX, fromY) với tọa độ (toX, toY)
        public void Move(int fromX, int fromY, int toX, int toY)
        {
            // thực hiện đổi chỗ 

            // nếu còn ăn được thì tiếp tục ăn và điền vào chỗ trống
            while (Match())
            {
                PopulateTable();
            }
        }
    }
}