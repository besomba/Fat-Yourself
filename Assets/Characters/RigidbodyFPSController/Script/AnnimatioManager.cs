using UnityEngine;
using System.Collections;

public class AnnimatioManager : MonoBehaviour {
	
	
	public Animator animator;
	private bool isLeftFire = false;
	private float numberFrameLeft = 0;
	private bool isRightFire = false;
	private float numberFrameRight = 0;
	
	// Use this for initialization
	void Start () {
		//animator =  GetComponent<Animator>();
	}
	
	public void leftFire() {
		animator.SetBool("leftArm", true);
		numberFrameLeft = Time.frameCount;
	}
	
	public void RightFire() {
		numberFrameRight = Time.frameCount;
		animator.SetBool("rightArm", true);	
	}
	
	public void leftStopFire() {
		if (numberFrameLeft + 50 <= Time.frameCount)	
		animator.SetBool("leftArm", false);
	}
	
	public void RightStopFire() {
		if(numberFrameRight + 50 <= Time.frameCount)
		animator.SetBool("rightArm", false);	
	}
	
	public void jump(){
		animator.SetBool("jump", true);	
	}
	
	public void run(){
		animator.SetBool("run", true);	
	}
	
	public void stopJump(){
		animator.SetBool("jump", false);	
	}
	
	public void stopRun(){
		animator.SetBool("run", false);	
	}
	
	public void DualFire() {
		animator.SetBool("leftArm", true);
		animator.SetBool("rightArm", true);
	}
	
	public void StopDualFire() {
		animator.SetBool("leftArm", false);
		animator.SetBool("rightArm", false);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Time.frameCount);
	}
}
