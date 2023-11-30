using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {

	private Transform cameraTranform;
	private Vector3 lastCameraPosition;
	//[SerializeField] private Vector2 parallaxEffector;
	//private float textureUnitSizeX;

    void Start()
    {
        cameraTranform = Camera.main.transform;
        lastCameraPosition = cameraTranform.position;
		//Sprite sprite = GetComponent<SpriteRenderer>().sprite;
		//Texture2D texture = sprite.texture;
        //textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }
    void LateUpdate () {
		Vector3 deltaMovement = cameraTranform.position - lastCameraPosition;
		//transform.position += new Vector3(deltaMovement.x * parallaxEffector.x, deltaMovement.y * parallaxEffector.y);
		transform.position += deltaMovement;
        lastCameraPosition = cameraTranform.position;
        
		//if (cameraTranform.position.x - transform.position.x >= textureUnitSizeX)
		//{
		//	float offSetPositionX = (cameraTranform.position.x - transform.position.x) % textureUnitSizeX;
		//	transform.position = new Vector3(cameraTranform.position.x + offSetPositionX, transform.position.y);
  //      }
	   
	} 
    
}
