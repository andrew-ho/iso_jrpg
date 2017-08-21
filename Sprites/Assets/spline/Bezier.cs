using UnityEngine;
using System.Collections;

public class Bezier {

	public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t) {
		//return Vector3.Lerp(Vector3.Lerp(p0,p1, t), Vector3.Lerp(p1,p2, t), t);
		t = Mathf.Clamp01(t);
		float oneMinusT = 1.0f - t;
		Vector3 result = (oneMinusT * oneMinusT * p0) + (2f * oneMinusT * t * p1) + (t * t * p2);
		return result;
	}

	public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
		t = Mathf.Clamp01(t);
		float oneMinusT = 1.0f - t;
		return		oneMinusT * oneMinusT * oneMinusT * p0 +
					3 * oneMinusT * oneMinusT * t * p1 +
					3 * oneMinusT * t * t * p2 +
					t * t * t * p3;
	}

	public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t) {
		return 2f * (1 - t) * (p1 - p0) + 2f * t * (p2 - p1);
	}

	public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
		t = Mathf.Clamp01(t);
		float oneMinusT = 1.0f - t;
		return		3 * oneMinusT * oneMinusT * (p1 - p0) +
					6 * oneMinusT * t * (p2-p1) +
					3 * t * t * (p3-p2);
	}
}
