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
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInRange() == true)
            _attacking.Begin(_player.GetComponentInChildren<Damage>());
        else
            _moving.Begin(_initPosition);
    }

    private bool IsInRange()
    {
        Vector2 targetPoint = new Vector2(_player.transform.GetChild(0).position.x, _player.transform.GetChild(0).position.z);
        Vector2 point = new Vector2(this.transform.position.x, this.transform.position.z);

        return Vector2.Distance(targetPoint, point) < _searchRange;
    }
}
