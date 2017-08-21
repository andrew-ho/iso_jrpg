using UnityEngine;
using System.Collections;
using System;

public class SplineFollow {

	/// <summary>
	/// spline reference
	/// </summary>
	private Spline m_Spline;
	/// <summary>
	/// current spline Line this object is at
	/// </summary>
	public int m_Index;
	/// <summary>
	/// current seek of spline
	/// </summary>
	public float m_SeekPos;

	/// <summary>
	/// 
	/// </summary>
	public bool m_GoingForwards = true;

	/// <summary>
	/// 
	/// </summary>
	public bool m_Reverse = false;

	private delegate void finishDel();

	private finishDel finishIndex;
	private finishDel finishCycle;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="a_Spline"></param>
	public SplineFollow(Spline a_Spline) {
		m_Spline = a_Spline;

		//m_MoveToPos = m_Spline.getSplinePoint(0, 0);
		m_SeekPos = 0.0f;
		m_Index = 0;

		finishIndex = finishedIndex;
		finishCycle = finishedSpline;
	}
	
	/// <summary>
	/// sets current segment of spline
	/// </summary>
	/// <param name="a_Index"></param>
	public void setIndex(int a_Index) {
		m_Index = Mathf.Clamp(a_Index, 0, m_Spline.splineCount);
	}

	/// <summary>
	/// updates spline value
	/// </summary>
	/// <param name="a_Speed">speed to travel at</param>
	public void update(float a_Speed = 1.0f) {
		float increment = Time.deltaTime / m_Spline.getSpeedOfNode(m_Index) * a_Speed;

		if (m_GoingForwards) {
			m_SeekPos += increment;
			while (m_SeekPos >= 1) {
				m_SeekPos -= 1;
				finishIndex();
			}
			
		}
		else {//going backwards
			if (m_Reverse) {
				Debug.Log("Backwards: " + m_Index.ToString() + " seek: " + m_SeekPos.ToString() + " dir: " + m_GoingForwards.ToString());
			}
			m_SeekPos -= increment;
			while (m_SeekPos < 0) {
				m_SeekPos += 1;				
				finishIndex();

			}
		
		}

		
	}

	/// <summary>
	/// returns the position of seekPos
	/// </summary>
	public Vector3 getPos() {
		if (m_Reverse) {
			Debug.Log("POS: " + m_Index.ToString() + " seek: " + m_SeekPos.ToString() + " dir: " + m_GoingForwards.ToString());
		}
		return m_Spline.getSplinePoint(m_Index, m_SeekPos);
	}

	/// <summary>
	/// sets whether the spline will follow it self back 
	/// </summary>
	/// <param name="a_Reverse"></param>
	public void setReverse(bool a_Reverse) {
		m_Reverse = a_Reverse;
	}

	public void setDirection(bool a_TravelForwards) {
		m_GoingForwards = a_TravelForwards;
	}

	public void reverseDirection() {
		m_GoingForwards = !m_GoingForwards;
	}

	public void finishedIndex() {
		if (m_GoingForwards) {
			m_Index++;
			if (m_Index >= m_Spline.splineCount - 1) {
				finishCycle();
			}
		}
		else {//going backwards
			m_Index--;
			if (m_Index < 0) {
				finishCycle();
			}
		}

	}

	public void finishedSpline() {
		
		if (m_Reverse) {
			Debug.Log("amount " + m_Spline.splineCount);
			Debug.Log("das das1 " + m_Index.ToString());

			if (m_GoingForwards) {
				m_Index = m_Spline.splineCount-1;
				m_SeekPos = 1.0f;
			}
			else {
				m_Index = 0;
				m_SeekPos = 0;
			}

			reverseDirection();
			Debug.Log("das das2 " + m_Index.ToString() + " seek: " + m_SeekPos.ToString() + " dir: " + m_GoingForwards.ToString());
			return;
		}

		if (m_GoingForwards) {
			m_Index -= m_Spline.splineCount - 1;
			m_SeekPos = 0;
		}
		else {//going backwards
			m_Index += m_Spline.splineCount - 1;
			m_SeekPos = 1;
		}
	}


}
