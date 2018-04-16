using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformFactory : MonoBehaviour 
{
	[SerializeField] private Platform [] PlatformPrefab;
	[SerializeField] private float RecycleWaitTime = 5.0f;
	
	private Platform[,] mPool;
	private List<Platform> mAvailable;
	private WaitForSeconds mRecycleWaitTime;
	
	private int MAX_PLATFORMS = 20;
	
	void Awake()
	{
		mPool = new Platform[MAX_PLATFORMS, PlatformPrefab.Length];
		mAvailable = new List<Platform>( MAX_PLATFORMS );
		
		for (int count = 0; count < MAX_PLATFORMS; count++) 
		{
			for (int type = 0; type < PlatformPrefab.Length; type ++) 
			{
				Platform planet = Instantiate (PlatformPrefab [type]) as Platform;
				mPool [count,type] = planet;

				mAvailable.Add (planet);

				mPool [count, type].gameObject.name = "PoolPlatform" + (count + 1); 
				mPool [count, type].gameObject.SetActive (false);
				mPool [count, type].transform.parent = transform;
			}
		}
		
		mRecycleWaitTime = new WaitForSeconds( RecycleWaitTime );
		StartCoroutine( Recycle() );
	}
	
	public Platform GetNextPlatform()
	{
		Platform planet = FindFreePlatform();
		if( planet != null )
		{
		}
		
		return planet;
	}
	
	public void ForceRecycle()
	{
		DoRecycle();
	}
	
	private IEnumerator Recycle()
	{
		while( true )
		{
			DoRecycle();
			yield return mRecycleWaitTime;
		}
	}
	
	private void DoRecycle()
	{
		for( int count = 0; count < MAX_PLATFORMS; count++ )
		{
			for (int type = 00; type < PlatformPrefab.Length; type ++) 
			{
				if( mPool[count,type] != null && !mPool[count,type].IsAlive && !mAvailable.Contains( mPool[count,type] ) )
				{
					mAvailable.Add( mPool[count,type] );
				}
			}
		}
	}
	
	private Platform FindFreePlatform()
	{
		Platform planet = null;
		
		if( mAvailable != null && mAvailable.Count > 0 )
		{
			planet = mAvailable[0];
			mAvailable.Remove( planet );
		}
		
		return planet;
	}
}
