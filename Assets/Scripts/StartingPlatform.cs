using UnityEngine;
using System.Collections;

public class StartingPlatform : MonoBehaviour 
{
	public delegate void PlayerTrigger();

	public static event PlayerTrigger OnPlayerStart;
	public static event PlayerTrigger OnPlayerFell;

	private bool mStarted;

	void Start()
	{
		Reset();
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		if( other as CircleCollider2D )
		{
			Player player = other.GetComponent<Player>();
			if( player != null )
			{
				if( !mStarted )
				{
					mStarted = true;

					if( OnPlayerStart != null )
					{
						OnPlayerStart();
					}
				}
				else
				{
					if( OnPlayerFell != null )
					{
						OnPlayerFell();
					}
				}
			}
		}
	}

	public void Reset()
	{
		mStarted = false;
	}
}
