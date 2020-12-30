using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PersonMove : MonoBehaviour
{
	public float speed = 150;
	public float addForce = 7;
	public bool lookAtCursor;
	public KeyCode leftButton = KeyCode.LeftArrow;
	public KeyCode rightButton = KeyCode.RightArrow;
	public KeyCode upButton = KeyCode.UpArrow;
	public KeyCode downButton = KeyCode.DownArrow;
	public KeyCode addForceButton = KeyCode.Space;
	public bool isFacingRight = true;
	private Vector3 direction;
	private float horizontal;
	private Rigidbody2D body;
	private float rotationY;
	private bool jump;
    private Animator anim;

	void Start () 
	{
		body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
		body.fixedAngle = true;
	}
	void OnCollisionStay2D(Collision2D coll) 
	{
		if(coll.transform.tag == "Ground")
		{
			body.drag = 10;
			jump = true;
		}
	}
	void OnCollisionExit2D(Collision2D coll) 
	{
		if(coll.transform.tag == "Ground")
		{
			body.drag = 0;
			jump = false;
		}
	}
	void FixedUpdate()
	{
		body.AddForce(direction * body.mass * speed);

		if(Mathf.Abs(body.velocity.x) > speed/100f)
		{
			body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed/100f, body.velocity.y);
		}
        if(Input.GetKey(addForceButton) && jump)
		{
			body.velocity = new Vector2(0, addForce);
		}	
	}
	void Flip()
	{
        isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;					
	}
	void Update () 
	{
		if(lookAtCursor)
		{
			Vector3 lookPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
			lookPos = lookPos - transform.position;
			float angle  = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		if(Input.GetKey(leftButton)) horizontal = -1;
		else if(Input.GetKey(rightButton)) horizontal = 1; 
        else horizontal = 0;
        if(horizontal != 0) anim.SetBool("right", true);
        else anim.SetBool("right", false);
        direction = new Vector2(horizontal, 0); 
		if(horizontal > 0 && !isFacingRight) Flip(); 
        else if(horizontal < 0 && isFacingRight) Flip();
	}
}