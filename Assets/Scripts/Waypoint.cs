using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField, Range(0.1f, 1.0f)]
    private float _radius = 0.1f;

    private void OnDrawGizmos()
    {
        for(int i = 0; i < this.transform.childCount; ++i)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(GetWaypoint(i), _radius);

            int next = GetNextIndex(i);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(next));
        }
    }

    public Vector3 GetWaypoint(int index)
    {
        return this.transform.GetChild(index).position;
    }

    public int GetNextIndex(int index)
    {
        return (index + 1) % this.transform.childCount;
    }

}
