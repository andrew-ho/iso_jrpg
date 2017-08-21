using UnityEngine;
using System.Collections;

public class TravelAlongSpline : MonoBehaviour {

	/// <summary>
	/// spline to travel along
	/// </summary>
	public Spline m_Spline;

	/// <summary>
	/// speed at which object will travel
	/// </summary>
	public float m_Speed;

	public bool m_TravelForwards = true;
	public bool m_ReverseSpline = false;

	/// <summary>
	/// next position that we will seek to
	/// </summary>
	private Vector3 m_MoveToPos;

	/// <summary>
	/// contains code to follow the spline
	/// so this can get the position
	/// </summary>
	private SplineFollow m_SplineFollow;

	// Use this for initialization
	void Start () {
		m_SplineFollow = new SplineFollow(m_Spline);
		m_SplineFollow.setDirection(m_TravelForwards);
		m_SplineFollow.setReverse(m_ReverseSpline);
		transform.position = m_SplineFollow.getPos();
		m_MoveToPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		//if close to end point, get the next point thats far enough away
		while (Vector3.Distance(transform.position, m_MoveToPos) < 0.15f) {

			m_SplineFollow.update(m_Speed);

			m_MoveToPos = m_SplineFollow.getPos();


		}

		//move to point
		float step = m_Speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, m_MoveToPos, step);
		transform.LookAt(m_MoveToPos);
	}
}
