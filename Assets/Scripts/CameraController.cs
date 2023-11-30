using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 height;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + height.y, transform.position.z);
    }
}
