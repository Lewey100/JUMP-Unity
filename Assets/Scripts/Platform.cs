using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour 
{
	public delegate void PlayerTrigger();
	
	public static event PlayerTrigger OnPlayerJoinedPlatform;
	public static event PlayerTrigger OnPlayerLeftPlatform;

	public bool IsAlive { get; set; }

	private Shake mShake;
	private float mTimeSinceActive = 0.0f;
	private bool mHasBeenActivated = false;

	public float DestroyTime { set; get; }

	void OnEnable()
	{
		mHasBeenActivated = false;
		mTimeSinceActive = 0.0f;
		mShake = GetComponentInChildren<Shake>();
		if( mShake != null )
		{
			mShake.enabled = false;
		}
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		if( other as CircleCollider2D )
		{
			Player player = other.GetComponent<Player>();
			if( player != null )
			{
				if( OnPlayerJoinedPlatform != null )
				{
					OnPlayerJoinedPlatform();
				}
								
				mHasBeenActivated = true;
				if( mShake != null )
				{
					mShake.enabled = true;
				}
			}
		}
	}

	void OnTriggerExit2D( Collider2D other )
	{
		if( other as CircleCollider2D )
		{
			Player player = other.GetComponent<Player>();
			if( player != null )
			{
				if( OnPlayerLeftPlatform != null )
				{
					OnPlayerLeftPlatform();
				}
			}
		}
	}

	void Update()
	{
		if( mHasBeenActivated )
		{
			mTimeSinceActive += Time.deltaTime;
			if( mTimeSinceActive > DestroyTime )
			{
				gameObject.SetActive( false );
				IsAlive = false;
			}
		}
	}
}
