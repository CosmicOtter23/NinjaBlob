using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private float moveVelocity;
	public float jumpHeight;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;

	private bool doubleJumped;

	private Animator anim;

	public Transform firePoint;
	public GameObject ninjaStar;

	public float shotDelay;
	public float shotDelayCounter;

	public float knockback;
	public float knockbackLength;
	public float knockbackCount;
	public bool knockFromRight;

	Rigidbody2D rigid2D;

	public bool onLadder;
	public float climbSpeed;
	private float climbVelocity;
	private float gravityStore;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();

		rigid2D = GetComponent<Rigidbody2D>();

		gravityStore = rigid2D.gravityScale;
	}
		
	void FixedUpdate() {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

	}
	
	// Update is called once per frame
	void Update () {

		if (grounded)
			doubleJumped = false;

		anim.SetBool ("Grounded", grounded);

#if  UNITY_STANDALONE || UNITY_WEBPLAYER

		if(Input.GetButtonDown("Jump") && grounded)
		{
			//rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpHeight);
			Jump();
		}

		if (Input.GetButtonDown("Jump") && !doubleJumped && !grounded) 
		{
			//rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpHeight);
			Jump();
			doubleJumped = true;
		}

		//moveVelocity = 0f;

		//moveVelocity = moveSpeed * Input.GetAxisRaw ("Horizontal");

		Move (Input.GetAxisRaw ("Horizontal"));

#endif

		if (knockbackCount <= 0) {
			rigid2D.velocity = new Vector2 (moveVelocity, rigid2D.velocity.y);
		} else {
			if (knockFromRight)
				rigid2D.velocity = new Vector2 (-knockback, knockback);
			if(!knockFromRight)
				rigid2D.velocity = new Vector2 (knockback, knockback);
			knockbackCount -= Time.deltaTime;
		}



		anim.SetFloat("Speed", Mathf.Abs(rigid2D.velocity.x));

		if (rigid2D.velocity.x > 0)
			transform.localScale = new Vector3 (1f, 1f, 1f);
		else if (rigid2D.velocity.x < 0)
			transform.localScale = new Vector3 (-1f, 1f, 1f);

#if  UNITY_STANDALONE || UNITY_WEBPLAYER

		if (Input.GetButtonDown ("Fire1")) 
		{
			//Instantiate (ninjaStar, firePoint.position, firePoint.rotation);
			FireStar();
			shotDelayCounter = shotDelay;
		}

		if(Input.GetButton("Fire1"))
		{
			shotDelayCounter -= Time.deltaTime;

			if(shotDelayCounter <= 0)
			{
				shotDelayCounter = shotDelay;
				//Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
				FireStar();
			}
		}

		//if (anim.GetBool ("Sword"))
		//	anim.SetBool ("Sword", false);

		//if (Input.GetButtonDown("Fire2")) 
		//{
		//	anim.SetBool("Sword", true);
		//}

#endif

		if (onLadder) {
			rigid2D.gravityScale = 0f;

			climbVelocity = climbSpeed * Input.GetAxisRaw ("Vertical");

			rigid2D.velocity = new Vector2 (rigid2D.velocity.x, climbVelocity);
		}

		if (!onLadder) {
			rigid2D.gravityScale = gravityStore;
		}

	}

	public void Move(float moveInput)
	{
		moveVelocity = moveSpeed * moveInput;
	}

	public void FireStar(){
		Instantiate (ninjaStar, firePoint.position, firePoint.rotation);
	}

	public void Jump()
	{
		//rigid2D.velocity = new Vector2 (rigid2D.velocity.x, jumpHeight);

		if(grounded)
		{
			//rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpHeight);
			//Jump();
			rigid2D.velocity = new Vector2 (rigid2D.velocity.x, jumpHeight);
		}

		if ( !doubleJumped && !grounded) 
		{
			//rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpHeight);
			//Jump();
			rigid2D.velocity = new Vector2 (rigid2D.velocity.x, jumpHeight);
			doubleJumped = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.transform.tag == "MovingPlatform") {
			transform.parent = other.transform;
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.transform.tag == "MovingPlatform") {
			transform.parent = null;
		}
	}
}
