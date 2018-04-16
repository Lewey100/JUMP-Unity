using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Environment : MonoBehaviour 
{
	[SerializeField] private float PlatformStart;

	private List<Platform> mPlatforms;
	private PlatformFactory mPlatformFactory;
	private GameSettings mSettings;
	private float mPlatformNextSpawnDistance;
	private float mPlatformLocation;
	private float mDestoryTime;
	private bool mLeftPlatform;

	void Awake()
	{
		mPlatforms = new List<Platform>();
		mPlatformFactory = GetComponentInChildren<PlatformFactory>();
	}

	void Start() 
	{
		mPlatformNextSpawnDistance = PlatformStart;
	}
	
	void Update() 
	{
		Platform platform = mPlatformFactory.GetNextPlatform();
		if( platform != null )
		{
			float location = mLeftPlatform ? -mPlatformLocation : mPlatformLocation;
			platform.transform.position = new Vector3( location, mPlatformNextSpawnDistance, 0.0f );
			platform.gameObject.SetActive( true );
			platform.DestroyTime = mDestoryTime;
			platform.IsAlive = true;
			mPlatforms.Add( platform ); 

			mPlatformNextSpawnDistance += mSettings.VerticalDistanceBetweenPlatforms;
			mLeftPlatform = !mLeftPlatform;

			mPlatformLocation += mSettings.PlatformLocationRamp;
			mDestoryTime -= mSettings.PlatformDestoryTimeRamp;
		}
	}

	public void ApplySettings( GameSettings settings )
	{
		mSettings = settings;
	}

	public void Reset()
	{
		for( int count = 0; count < mPlatforms.Count; count++ )
		{
			mPlatforms[count].gameObject.SetActive( false );
			mPlatforms[count].IsAlive = false;
		}
		mPlatformFactory.ForceRecycle();
		mPlatformNextSpawnDistance = PlatformStart;
		mPlatformLocation = mSettings.StartingPlatformLocation;
		mDestoryTime = mSettings.StartingPlatformDestoryTime;
	}
}
