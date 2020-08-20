using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Damage : MonoBehaviour
{
    [SerializeField, Range(1, 100)]
    private float _hp = 20.0f;

    private bool _bDeath = false;
    public bool Death { get { return _bDeath; } private set { _bDeath = value; } }//3개의 변수를 만드는 코드

    private Animator _animator;
    private List<Material> _materials = new List<Material>();

    private NavMeshAgent _navMeshAgent = null;
    private Collider[] _colliders = null;

    void Awake()
    {
        Renderer[] renderers = this.GetComponentsInChildren<Renderer>();//자식들이 가지고있는 renderer를 모두 긁어온다.
        foreach (Renderer renderer in renderers)
            _materials.AddRange(renderer.materials);

        _animator = this.GetComponent<Animator>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        _colliders = this.GetComponentsInChildren<Collider>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Hitted(float power)//power :이걸로 hp를 깎음
    {
        _hp = Mathf.Max(_hp - power, 0.0f);//둘중에 더 큰게 나옴.

        if (_hp <= 0.0f)
            GoAmerica();
        else
        {
            foreach (Material material in _materials)
                material.color = Color.red;

            _animator.SetTrigger("Hit");

            Invoke("RestoreMaterial", 0.7f);//MonoBehaviour안에 있는 함수로 함수이름을 넣으면 그 함수를 특정시간에 실행시켜준다. 
        }
    }

    private void GoAmerica()
    {
        _bDeath = true;
        _navMeshAgent.enabled = false;
        foreach (Collider collider in _colliders)
            collider.enabled = false;

        _animator.SetTrigger("Death");
    }

    private void RemoveObject()
    {
        Destroy(this.gameObject);
    }

    private void OnAmerica()
    {
        Invoke("RemoveObject", 1.0f);
    }

    private void RestoreMaterial()
    {
        foreach (Material material in _materials)
            material.color = Color.white;//원래대로 되돌리기
    }
}
