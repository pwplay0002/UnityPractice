using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallowCamera : MonoBehaviour
{
    [SerializeField, Range(1,20)]
    private float _distance = 6.0f;
    private Transform _camera = null;
    private Transform _player = null;
    void Start()
    {
        _camera = this.transform.GetChild(0);//자식의 transform이 나온다.
        _player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        this.transform.position = _player.position;
        _camera.localPosition = new Vector3(0.0f, _distance - 2.0f, -_distance - 5.0f);
    }
}
