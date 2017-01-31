#pragma strict

var animator	: Animator;						// Animator Controller
var target		: Transform;					// Target we will match to (saddle)
var startTime	: float			= 0.0;			// start time for the match
var endTime		: float			= 1.0;			// end time for the match
var animatorTarget: Animator;

function Start () {
    animator		= GetComponent(Animator);		// Assign
}

function Update () {
	if (Input.GetKeyDown("w"))
		animator.SetFloat("locomotion", 2.0);
	if (Input.GetKeyUp("w"))
		animator.SetFloat("locomotion", 1.0);
}

function MountDragon(){
    doMountDragon();
	
}

function doMountDragon()
{
    animator.MatchTarget(target.position, target.rotation /** Quaternion.Euler(90,0,0) */, AvatarTarget.Root, MatchTargetWeightMask(Vector3.one, 1.0), startTime, endTime);
    yield WaitForSeconds(3);
}