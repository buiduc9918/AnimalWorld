using UnityEngine;


namespace UI.Raycaster
{

    public class TouchPhaseExample : MonoBehaviour
    {
        [SerializeField] private Vector2 startPos;
        [SerializeField] private Vector2 direction;

        public void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPos = touch.position;
                        break;

                    case TouchPhase.Moved:
                        direction = touch.position - startPos;
                        break;

                    case TouchPhase.Ended:
                        break;
                }
            }
        }
    }
}
