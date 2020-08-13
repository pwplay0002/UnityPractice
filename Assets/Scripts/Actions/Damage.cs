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
        _animator.SetTrigger("Hit");
    }
}
