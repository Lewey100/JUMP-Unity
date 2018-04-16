using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour 
{
	[SerializeField] private SpriteRenderer [] Lives;
	[SerializeField] private TextMesh Score;
	[SerializeField] private TextMesh Title;
	[SerializeField] private TextMesh SubTitle;
	//[SerializeField] private TextMesh MinorTitle;

	void Start() 
	{
		Lives = GetComponentsInChildren<SpriteRenderer>();
		Score = GetComponentInChildren<TextMesh>();

		Game.OnNewGame += HandleHudChangedEvent;
		Game.OnGameOver += HandleOnGameOver;
		Game.OnRestart += HandleOnRestart;
		Game.OnLostLife += HandleHudChangedEvent;
		Game.OnScoreChange += HandleHudChangedEvent;
		Game.OnGameRules += HandleOnGameRules;

		for( int count = 0; count < Lives.Length; count++ )
		{
			Lives[count].enabled = false;
		}		
	}

	void HandleOnGameRules (int lives, int score)
	{
		if (Title != null && SubTitle != null) 
		{
			Title.text = "Rules...";
			SubTitle.text = "Jump don't fall";
			//MinorTitle.text = "Press Escape to Exit";
		}
	}

	void HandleNewGame( int lives, int score )
	{
		for( int count = 0; count < Lives.Length; count++ )
		{
			Lives[count].enabled = true;
		}

		if( Title != null && SubTitle != null)
		{
			Title.gameObject.SetActive( false );
			SubTitle.gameObject.SetActive( false );
			//MinorTitle.gameObject.SetActive( false );

		}
	}

	void HandleOnRestart( int lives, int score )
	{
		if( Title != null && SubTitle != null )
		{
			Title.gameObject.SetActive( true );
			SubTitle.gameObject.SetActive( true );
			//MinorTitle.gameObject.SetActive( true );

			Title.text = "Jump!";
			SubTitle.text = "Press Space...";
			//MinorTitle.text = "Press Escape to See Game Rules";
		}
	}

	void HandleOnGameOver( int lives, int score )
	{
		if( Title != null && SubTitle != null)
		{
			Title.gameObject.SetActive( true );
			SubTitle.gameObject.SetActive( true );
		//	MinorTitle.gameObject.SetActive(true);
			
			Title.text = "Score: " + score.ToString( "D5" );
			SubTitle.text = "Press Space to continue...";
			//MinorTitle.text = "Press Escape to See Game Rules";
		}
	}

	void HandleHudChangedEvent( int lives, int score )
	{
		if( Lives != null && Score != null )
		{
			for( int count = 0; count < Lives.Length; count++ )
			{
				Lives[count].enabled = ( count < lives );
			}

			Score.text = score.ToString( "D5" );
		}

		if( Title != null && SubTitle != null)
		{
			Title.gameObject.SetActive( false );
			SubTitle.gameObject.SetActive( false );
			//MinorTitle.gameObject.SetActive( false );
		}
	}
}
