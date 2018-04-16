using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	[SerializeField] private Transform CamRoot;
	[SerializeField] private float JumpForce = 250.0f;
	[SerializeField] private float MaxSpeed = 2.0f;
	[SerializeField] private float CameraOffset = 2.0f;
	[SerializeField] private Transform GroundCheck;
	[SerializeField] private LayerMask GroundLayer;

	private Animator mAnim;
	private float mGroundRadius = 0.2f;
	private bool mFacingRight;
	private bool mGrounded;

	public bool Enabled { get; set; }

	void Start() 
	{
		mAnim = GetComponent<Animator>();
		mFacingRight = false;
		mGrounded = false;
	}

	void Update()
	{
		if( Enabled && mGrounded && Input.GetKeyDown( KeyCode.Space ) )
		{
			mAnim.SetBool( "Grounded", false );
			rigidbody2D.AddForce( new Vector2( JumpForce * ( mFacingRight ? -0.5f : 0.5f ), JumpForce ) );
		}

		if( CamRoot != null )
		{
			Vector3 camPos = CamRoot.position;
			camPos.y = transform.position.y + CameraOffset;
			CamRoot.position = camPos;
		}
	}
	
	void FixedUpdate() 
	{
		mGrounded = Physics2D.OverlapCircle( GroundCheck.position, mGroundRadius, GroundLayer );
		mAnim.SetBool( "Grounded", mGrounded );

		mAnim.SetFloat( "VSpeed", rigidbody2D.velocity.y ); 

		if( Enabled )
		{
			float move = Input.GetAxis( "Horizontal" );
			mAnim.SetFloat( "Speed", Mathf.Abs( move ) );
			rigidbody2D.velocity = new Vector2( move * MaxSpeed, rigidbody2D.velocity.y );
			if( move > 0.0f && mFacingRight ) 
			{
				Flip();
			}
			else if( move < 0.0f && !mFacingRight )
			{
				Flip();
			}
		}
	}

	void Flip()
	{
		mFacingRight = !mFacingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1.0f;
		transform.localScale = scale;
	}
}
