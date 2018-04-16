using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour 
{
	[SerializeField] private float mVerticalDistanceBetweenPlatforms;
	[SerializeField] private float mStartingPlatformDestoryTime;
	[SerializeField] private float mPlatformDestoryTimeRamp;
	[SerializeField] private float mStartingPlatformLocation;
	[SerializeField] private float mPlatformLocationRamp;
	[SerializeField] private int mPointsPerPlatform;
	[SerializeField] private int mStartingLives;

	public float VerticalDistanceBetweenPlatforms { get { return mVerticalDistanceBetweenPlatforms; } set { mVerticalDistanceBetweenPlatforms = value; } }
	public float StartingPlatformDestoryTime { get { return mStartingPlatformDestoryTime; } set { mStartingPlatformDestoryTime = value; } }
	public float PlatformDestoryTimeRamp { get { return mPlatformDestoryTimeRamp; } set { mPlatformDestoryTimeRamp = value; } }
	public float StartingPlatformLocation { get { return mStartingPlatformLocation; } set { mStartingPlatformLocation = value; } }
	public float PlatformLocationRamp { get { return mPlatformLocationRamp; } set { mPlatformLocationRamp = value; } }
	public int PointsPerPlatform { get { return mPointsPerPlatform; } set { mPointsPerPlatform = value; } }
	public int StartingLives { get { return mStartingLives; } set { mStartingLives = value; } }
}
