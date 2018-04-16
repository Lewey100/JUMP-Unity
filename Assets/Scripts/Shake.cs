using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour 
{
	[SerializeField] private Vector3 Amount;

	void Update()
	{
		Vector3 position;
		position.x = Random.Range( -Amount.x, Amount.x ); 
		position.y = Random.Range( -Amount.y, Amount.y ); 
		position.z = Random.Range( -Amount.z, Amount.z ); 
		transform.localPosition = position;
	}
}
