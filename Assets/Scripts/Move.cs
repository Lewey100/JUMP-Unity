using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour 
{
	[SerializeField] private Vector3 mMovement;

	// Update is called once per frame
	void Update() 
	{
		Vector3 position = transform.position;
		position += mMovement * Time.deltaTime;
		transform.position = position;	
	}
}
