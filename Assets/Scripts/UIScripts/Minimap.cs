using UnityEngine;

public class Minimap : MonoBehaviour
{

    public Transform player;

    // Start is called before the first frame update
    void LateUpdate()
    {
        Vector3 newMinimapPosition = player.position;
        newMinimapPosition.y = transform.position.y;
        transform.position = newMinimapPosition;
    }
}
