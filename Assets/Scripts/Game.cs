using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	public delegate void GameEvent( int lives, int score );
	public static event GameEvent OnGameRules;
	public static event GameEvent OnNewGame;
	public static event GameEvent OnRestart;
	public static event GameEvent OnGameOver;
	public static event GameEvent OnLostLife;
	public static event GameEvent OnScoreChange;

	private enum State { Starting, Playing, Gameover, GameRules }

	[SerializeField] private GameSettings GameSettingsPrefab;

	private GameSettings mSettings;
	private Environment mEnvironment;
	private Player mPlayer;
	private int mScore;
	private int mLives;
	private State mState;  

	void Start() 
	{
		mSettings = Instantiate( GameSettingsPrefab ) as GameSettings;
		mEnvironment = GetComponentInChildren<Environment>();
		mEnvironment.ApplySettings( mSettings );
		mEnvironment.Reset();
		mPlayer = GetComponentInChildren<Player>();
		mPlayer.Enabled = false;
		mLives = mSettings.StartingLives;
		mScore = 0;
		mState = State.Starting;

		StartingPlatform.OnPlayerStart += HandleOnPlayerStart;
		StartingPlatform.OnPlayerFell += HandleOnPlayerFell;
		Platform.OnPlayerJoinedPlatform += HandleOnPlayerJoinedPlatform;
	}
	
	void Update()
	{
		switch( mState )
		{
		case State.Starting:
			UpdateStarting();
			break;
		case State.Playing:
			UpdatePlaying();
			break;
		case State.Gameover:
			UpdateGameover();
			break;
		case State.GameRules:
			UpdateGameRules();
			break;
		}
	}

	void HandleOnPlayerStart()
	{
	}

	void HandleOnPlayerFell()
	{
		if( mState == State.Playing )
		{
			mEnvironment.Reset();
			mLives--;

			if( OnLostLife != null )
			{
				OnLostLife( mLives, mScore );
			}
		}
	}

	void HandleOnPlayerJoinedPlatform()
	{
		if( mState == State.Playing )
		{
			mScore += mSettings.PointsPerPlatform;
			
			if( OnScoreChange != null )
			{
				OnScoreChange( mLives, mScore );
			}
		}
	}

	private void UpdateStarting()
	{
		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			ChangeState( State.Playing );
		}
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			ChangeState (State.GameRules);
		}
	}

	private void UpdatePlaying()
	{
		if( mLives <= 0 )
		{
			ChangeState( State.Gameover );
		}


	}

	private void UpdateGameover()
	{
		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			ChangeState( State.Starting );
		}
	}

	private void UpdateGameRules()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			ChangeState (State.Starting);
		}
	}


	private void ChangeState( State s )
	{
		if( mState != s )
		{
			switch( s )
			{
			case State.Starting:
				mLives = mSettings.StartingLives;
				mScore = 0;
				if( OnRestart != null )
				{
					OnRestart( mLives, mScore );
				}
				break;
			case State.Playing:
				mPlayer.Enabled = true;
				if( OnNewGame != null )
				{
					OnNewGame( mLives, mScore );
				}
				break;
			case State.Gameover:
				mPlayer.Enabled = false;
				if( OnGameOver != null )
				{
					OnGameOver( mLives, mScore );
				}
				break;
			case State.GameRules:
				if (OnGameRules != null)
				{
					OnGameRules( 0, 0 );
				}
				break;
			}

			mState = s;
		}
	}
}
