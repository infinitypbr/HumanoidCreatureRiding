using UnityEngine;
using System.Collections;

public class TestDragonBob : MonoBehaviour {
    Animator animator;                     // Animator Controller
    public Animator animatorTarget;      //Animator Controller of the dragon
    public Transform target;					// Target we will match to (saddle)
    public AnimationClip mountClip;
    float startTime= 0.0f;			// start time for the match
    float endTime = 1.0f;			// end time for the match

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
    bool shouldLog = false;
    void Update()
    {
        if (Input.GetKeyDown("w"))
            animator.SetFloat("locomotion", 2.0f);
        if (Input.GetKeyUp("w"))
            animator.SetFloat("locomotion", 1.0f);
        if(shouldLog)
            Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name + ": " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime + " | " + animatorTarget.GetCurrentAnimatorClipInfo(0)[0].clip.name + ": " +animatorTarget.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    public void MountDragon()
    {
        StartCoroutine(doMountDragon());
        StartCoroutine(doCameraMovement());
    }

    IEnumerator doMountDragon()
    {

        //shouldLog = true;
        animator.MatchTarget(target.position, target.rotation /** Quaternion.Euler(90,0,0) */, AvatarTarget.Root, new MatchTargetWeightMask(Vector3.one, 1.0f), animator.GetCurrentAnimatorStateInfo(0).normalizedTime, endTime);
        yield return new WaitForSeconds((1 - animator.GetCurrentAnimatorStateInfo(0).normalizedTime) * animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        animator.Play("Mount", 0, 0);
        animatorTarget.Play("Mount", 0, 0);
        yield return null;
        transform.position = target.position;
        transform.rotation = target.rotation;
        //animator.MatchTarget(target.position, target.rotation /** Quaternion.Euler(90,0,0) */, AvatarTarget.Root, new MatchTargetWeightMask(Vector3.one, 1.0f), startTime, endTime);
        yield return new WaitForSeconds(mountClip.length);
        Debug.Log("Moet loggen");
        
        
        animator.Play("Idle",0,0);
        animatorTarget.Play("Idle",0,0);
    }

    IEnumerator doCameraMovement()
    {
        Camera ourCam = GetComponentInChildren<Camera>();
        Transform ourCamTransform = ourCam.transform;
        ourCamTransform.parent = animatorTarget.transform;
        yield return null;
        /*Vector3 startPos = ourCamTransform.transform.localPosition;
        Vector3 endPos = new Vector3(0.681f, 4.101f, -1.952f);
        Quaternion startRot = ourCamTransform.transform.localRotation;
        Quaternion endRot = Quaternion.Euler(30, -15.755f, -6.084f);
        float t = 0;
        while(t < 1)
        {
            ourCamTransform.localPosition = Vector3.Lerp(startPos, endPos, t);
            ourCamTransform.localRotation = Quaternion.Slerp(startRot, endRot, t);
            t += Time.deltaTime / 2;
            yield return null;
        }
        ourCamTransform.localPosition = endPos;
        ourCamTransform.localRotation = endRot;*/
    }
}
