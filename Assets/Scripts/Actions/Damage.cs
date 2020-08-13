using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField, Range(1, 100)]
    private float _hp = 20.0f;

    public bool Death { get; private set; }//3개의 변수를 만드는 코드

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Hitted(float power)//power :이걸로 hp를 깎음
    {

    }
}
