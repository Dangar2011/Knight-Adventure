using UnityEngine;

public class SwordRotation : MonoBehaviour
{
    public GameObject player; 
    private Vector3 swordPosition;
    private Vector3 newPosition;
    private void Start()
    {
        swordPosition = new Vector3(2,0,0);
    }

    private void Update()
    {
        
        bool isPlayerFlipped = player.GetComponent<SpriteRenderer>().flipX;

        if (isPlayerFlipped)
        {
            newPosition = player.transform.position - swordPosition;

        }
        else
        {
            newPosition = player.transform.position + swordPosition;

        
        }
        transform.position = newPosition;
    }
}
