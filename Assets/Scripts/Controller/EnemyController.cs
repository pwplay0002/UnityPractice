using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Moving _moving = null;
    private Attacking _attacking = null;

    [SerializeField, Range(1.0f, 10.0f)]
    private float _searchRange = 5.0f;

    private GameObject _player = null;

    [SerializeField]
    private Waypoint _waypoint = null;
    private int _currentWaypointIndex = 0;

    [SerializeField]
    private float _arrivedTime = 0.0f;

    [SerializeField]
    private float _dwellTime = 2.0f;

    void Awake()
    {
        _moving = this.GetComponent<Moving>();
        _attacking = this.GetComponent<Attacking>();

        _player = GameObject.Find("Player");
    }

    private Vector3 _initPosition;
    void Start()
    {
        _initPosition = this.transform.position;
        _dwellTime = Random.Range(1.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInRange() == true)
            _attacking.Begin(_player.GetComponentInChildren<Damage>());
        else
        {
            Vector3 next = _initPosition;
            if(IsArrivedWaypoint() == true)
            {
                _arrivedTime = 0.0f;
                _currentWaypointIndex = _waypoint.GetNextIndex(_currentWaypointIndex);
            }

            next = GetCurrentWaypoint();

            if (_arrivedTime > _dwellTime)
            {
                _moving.Begin(next);
            }
            else
                _arrivedTime += Time.deltaTime;

        }
    }

    private bool IsInRange()
    {
        Vector2 targetPoint = new Vector2(_player.transform.GetChild(0).position.x, _player.transform.GetChild(0).position.z);
        Vector2 point = new Vector2(this.transform.position.x, this.transform.position.z);

        return Vector2.Distance(targetPoint, point) < _searchRange;
    }

    private Vector3 GetCurrentWaypoint()
    {
        return _waypoint.GetWaypoint(_currentWaypointIndex);
    }

    private bool IsArrivedWaypoint()
    {
        Vector2 point = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 waypoint = new Vector2(GetCurrentWaypoint().x, GetCurrentWaypoint().z);

        return Vector2.Distance(point, waypoint) < 0.25f;

    }
}
