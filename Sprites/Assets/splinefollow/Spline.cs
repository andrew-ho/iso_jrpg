using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Spline : MonoBehaviour
{

    public class SplineNode
    {
        public Vector3 pos;
        public Vector3 tangent;
        public float distTillNextNode;
    }

    public List<SplineNode> splineNodes = new List<SplineNode>();

    private float timer = 0.0f;
    private int index = 0;

    private float m_UpdateTimer = 0.0f;


    public int splineCount { get { return splineNodes.Count; } }
    public List<SplineNode> nodes { get { return splineNodes; } }

    // Use this for initialization
    void Awake()
    {
        updateNodes();
    }

    // Update is called once per frame
    void OnDrawGizmos()
    {

        for (int i = 0; i < splineNodes.Count - 1; i++)
        {
            DrawCurve(splineNodes[i], splineNodes[i + 1]);
        }

        if (splineNodes.Count == 0)
        {
            updateNodes();
            index = Mathf.Clamp(index, 0, splineNodes.Count - 1);
        }
        timer += Time.deltaTime / splineNodes[index].distTillNextNode;

        if (timer >= 1.0f)
        {
            timer -= 1.0f;
            index++;
            if (index >= splineNodes.Count - 1)
            {
                index = 0;
            }
        }

        Gizmos.DrawSphere(getSplinePoint(splineNodes[index], splineNodes[index + 1], timer), 0.3f);
    }

    void Update()
    {
        m_UpdateTimer += Time.deltaTime;
        if (m_UpdateTimer >= 5)
        {
            updateNodes();
            m_UpdateTimer = 0;
        }
    }

    private void updateNodes()
    {
        splineNodes.Clear();

        //get splines
        for (int i = 0; i < transform.childCount; i++)
        {
            SplineNode n = new SplineNode();
            n.pos = transform.GetChild(i).position;
            n.tangent = transform.GetChild(i).position - transform.GetChild(i).GetChild(0).position;

            if (i != 0)
            {
                splineNodes[i - 1].distTillNextNode = calculateDistance(splineNodes[i - 1], n);
            }

            splineNodes.Add(n);
        }
    }

    public float getSpeedOfNode(int a_Index)
    {
        if (a_Index >= splineNodes.Count)
        {
            Debug.LogWarning("Index out of array");
            return 0;
        }
        return splineNodes[a_Index].distTillNextNode;
    }

    float calculateDistance(SplineNode P1, SplineNode P2)
    {
        float dist = 0;

        Vector3 prevPoint = P1.pos;
        Vector3 currPoint = P1.pos;
        int steps = 20;

        for (int t = 0; t <= steps; t++)
        {

            float s = (float)t / (float)steps;

            currPoint = getSplinePoint(P1, P2, s);

            dist += Vector3.Distance(prevPoint, currPoint);

            prevPoint = currPoint;
        }
        return dist;
    }

    void DrawCurve(SplineNode P1, SplineNode P2)
    {

        Vector3 prevPoint = P1.pos;
        Vector3 currPoint = P1.pos;
        int steps = 20;

        for (int t = 0; t <= steps; t++)
        {

            float s = (float)t / (float)steps;

            currPoint = getSplinePoint(P1, P2, s);

            if (t % 2 == 0)
                Gizmos.DrawLine(prevPoint, currPoint);

            prevPoint = currPoint;
        }

    }

    public Vector3 getSplinePoint(int index, float t)
    {
        return getSplinePoint(splineNodes[index], splineNodes[index + 1], t);
    }

    public Vector3 getSplinePoint(SplineNode P1, SplineNode P2, float t)
    {
        float s = t;
        float h1 = 2 * Mathf.Pow(s, 3) - 3 * s * s + 1;
        float h2 = -2 * Mathf.Pow(s, 3) + 3 * s * s;
        float h3 = Mathf.Pow(s, 3) - 2 * s * s + s;
        float h4 = Mathf.Pow(s, 3) - s * s;

        return h1 * P1.pos + h2 * P2.pos + h3 * P1.tangent + h4 * P2.tangent;
    }

}
