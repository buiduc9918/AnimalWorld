using UnityEngine;

namespace Tiles
{

    public class Tile : MonoBehaviour
    {
        public static Tile Instance { get; private set; }
        public int number;
    }
}
