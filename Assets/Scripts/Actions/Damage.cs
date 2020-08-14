using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField, Range(1, 100)]
    private float _hp = 20.0f;

    private Animator _animator;
    private List<Material> _materials = new List<Material>();

    public bool Death { get; private set; }//3개의 변수를 만드는 코드

    void Awake()
    {
        Renderer[] renderers = this.GetComponentsInChildren<Renderer>();//자식들이 가지고있는 renderer를 모두 긁어온다.
        foreach (Renderer renderer in renderers)
            _materials.AddRange(renderer.materials);

        _animator = this.GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Hitted(float power)//power :이걸로 hp를 깎음
    {
        foreach (Material material in _materials)
            material.color = Color.red;

        _animator.SetTrigger("Hit");

        Invoke("RestoreMaterial", 0.7f);//MonoBehaviour안에 있는 함수로 함수이름을 넣으면 그 함수를 특정시간에 실행시켜준다.
    }

    private void RestoreMaterial()
    {
        foreach (Material material in _materials)
            material.color = Color.white;//원래대로 되돌리기
    }
}
