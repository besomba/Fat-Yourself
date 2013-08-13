using UnityEngine;
using System.Collections;

// Source : http://wiki.unity3d.com/index.php?title=RigidbodyFPSWalker

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

 
public class RigidbodyFPSController : MonoBehaviour {
 
	public float speed = 5.0f;
	public float sprintSpeed = 20.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	public int numberOfWallJump = 2;
	public int currentNumberOfWallJump = 0;
	private bool grounded = false;
	public Transform PlayerBase;
  
	void Awake () {
	    rigidbody.freezeRotation = true;
	    rigidbody.useGravity = false;
	}
 
	public void Move(bool isSprint, Vector3 targetVelocity) {
	 	if (grounded) {
			targetVelocity = transform.TransformDirection(targetVelocity);
			if (isSprint) {
				targetVelocity *= sprintSpeed;
			}
			else
		        targetVelocity *= speed;
	        // Apply a force that attempts to reach our target velocity
	        Vector3 velocity = rigidbody.velocity;
	        Vector3 velocityChange = (targetVelocity - velocity);
	        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
	        velocityChange.y = 0;
	        rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
		}
	}
	
	public void jump() {
		 if (grounded) {
				if (canJump) {
				Vector3 velocity = rigidbody.velocity;
	            rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				currentNumberOfWallJump++;
	        }
		}
		
	}
	
	void FixedUpdate () {
		/*Debug.DrawRay(PlayerBase.position, PlayerBase.TransformDirection(Vector3.down), Color.red);
	    if (grounded) {
	        // Calculate how fast we should be moving
	        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	        targetVelocity = transform.TransformDirection(targetVelocity);
			if (Input.GetButton("Sprint")) {
				targetVelocity *= sprintSpeed;
			}
			else
		        targetVelocity *= speed;
	        // Apply a force that attempts to reach our target velocity
	        Vector3 velocity = rigidbody.velocity;
	        Vector3 velocityChange = (targetVelocity - velocity);
	        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
	        velocityChange.y = 0;
	        rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
 
	        // Jump
	        if (canJump && Input.GetButton("Jump")) {
	            rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				currentNumberOfWallJump++;
	        }
	    }*/
 
	    // We apply gravity manually for more tuning control
	    rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
 
	    grounded = onTheGround();
	}
 
	public bool onTheGround() {
		RaycastHit hit;
		if (Physics.Raycast(PlayerBase.position, PlayerBase.TransformDirection(Vector3.down), out hit)) {
			Debug.DrawRay(PlayerBase.position, PlayerBase.TransformDirection(Vector3.down), Color.red);
			if (Vector3.Distance(PlayerBase.position, hit.point) < 1) {
				grounded = true;
				return true;
			}
		}
		return false;
	}
	
	void OnCollisionStay (Collision collisionInfo) {
		RaycastHit hit;
		if (Physics.Raycast(PlayerBase.position, PlayerBase.TransformDirection(Vector3.down), out hit)) {
			Debug.DrawRay(PlayerBase.position, PlayerBase.TransformDirection(Vector3.down), Color.red);
			if (Vector3.Distance(PlayerBase.position, hit.point) < 0.2)
				grounded = true;
		}
	}
 
	float CalculateJumpVerticalSpeed () {
	    // From the jump height and gravity we deduce the upwards speed 
	    // for the character to reach at the apex.
	    return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}

