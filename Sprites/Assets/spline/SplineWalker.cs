using UnityEngine;
using System.Collections;

public enum SplineWalkerMode {
	Once,Loop,PingPong
}

public class SplineWalker : MonoBehaviour {

	public BezierSpline spline;

	public float speed;

	private float progress;

	public bool lookForward;

	public SplineWalkerMode mode;

	private bool goingForward = true;

	// Update is called once per frame
	void Update () {
		if(speed == 0) {
			return;
		}

		float progressAmount = Time.deltaTime / (spline.GetVelocity(progress).magnitude / speed);

		if (goingForward) {
			progress += progressAmount;
			if (progress > 1f) {
				if (mode == SplineWalkerMode.Once) {
					progress = 1.0f;
				}else if (mode == SplineWalkerMode.Loop) {
					progress -= 1.0f;
				}
				else {
					progress = 2f - progress;
					goingForward = false;
				}
			}
		}
		else {
			progress -= progressAmount;
			if(progress < 0f) {
				progress = -progress;
				goingForward = true;
			}
		}

		Vector3 position = spline.GetPoint(progress);
		transform.localPosition = position;
		if (lookForward) {
			if (goingForward) {
				transform.LookAt(position + spline.GetDirection(progress));
			}
			else {
				transform.LookAt(position - spline.GetDirection(progress));
			}
		}

	}
}
