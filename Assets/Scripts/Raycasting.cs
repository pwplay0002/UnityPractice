using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
    private Ray _ray;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    private void OnGUI()//화면에 그래픽이나 gui를 간단히 그릴수있다.
    {
        GUI.Box(new Rect(0, 0, Screen.width * 0.5f, 40), "Hello!!");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_ray.origin, _ray.direction * 500.0f);
    }
}
