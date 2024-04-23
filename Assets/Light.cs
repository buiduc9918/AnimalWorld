using UnityEngine;

public class Light : MonoBehaviour
{
    public GameObject prefabs;

    Vector3 pos1, pos2;
    float time;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; // Sử dụng toán tử += để tính toán thời gian đúng cách

        if (time >= 0.04f && time <= 0.06f && pos1 == Vector3.zero)
        {
            pos1 = transform.position;
        }

        if (time >= 0.09f && time <= 0.11 && pos2 == Vector3.zero)
        {
            pos2 = transform.position;

        }
        Vector3 afterdirection = pos1 - pos2;
        if (afterdirection != Vector3.zero)
        {
            GameObject light = Instantiate(prefabs, afterdirection, Quaternion.identity);
            Destroy(light, 0.02f);
        }
    }

}
