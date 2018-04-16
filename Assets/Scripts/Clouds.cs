using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour 
{
	private Move [] mMovables;

	private const float OFF_SCREEN_POSISION = 3.2f;

	void Awake() 
	{
		mMovables = GetComponentsInChildren<Move>();	
	}
	
	void Update() 
	{
		for( int count = 0; count < mMovables.Length; count++ )
		{
			if( mMovables[count].transform.position.x > OFF_SCREEN_POSISION )
			{
				Vector3 position = mMovables[count].transform.position;
				position.x = -OFF_SCREEN_POSISION;
				mMovables[count].transform.position = position;
			}
		}
	}
}
