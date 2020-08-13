using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//Nevigation mesh

public class Moving : MonoBehaviour, IAction
{
    //[SerializeField]//직렬화
    //private Vector3 _destination;
    //
    //[SerializeField, Range(0, 100)]
    //private float speed = 0.0f;

    private NavMeshAgent _agent = null;//Navmesh agent component
    //멤버변수 만들때는 언더바 붙여서 많이 쓴다.
    private Animator _animator = null;
    private ActionManager _actionManager = null;

    //private Vector3 _destination;
    //private Vector3 _thisPosition;
    //도구 -> 옵션 -> C# -> 고급 -> 프로시저단위 줄 생성
    void Awake()
    {
        _agent = this.GetComponent<NavMeshAgent>();
        _animator = this.GetComponent<Animator>();
        _actionManager = this.GetComponent<ActionManager>();
    }

    void Start()
    {

    }

    void Update()
    {//destination : 목적지
        //if (Input.GetMouseButton(0))//0 왼쪽 1오른쪽버튼
        //{
        //    Ray ray = GetMouseRay();
        //    Debug.DrawRay(ray.origin, ray.direction);//화면에 표시
        //
        //    RaycastHit hit;
        //    if(Physics.Raycast(ray, out hit, 500))//값을 hit에다가 넣는것.//500안쪽으로 안잡히면 체크 x
        //    {
        //        _agent.destination = hit.point;//point : 현재 충돌된 위치
        //        _destination = hit.point;
        //    }
        //}

        //_thisPosition = this.transform.position;
        //_thisPosition.y = 0.0f;//땅에 붙인다
        //Debug.DrawLine(_thisPosition, _destination);
    }

    private void FixedUpdate()//정밀한 업데이트할 때 사용한다. 설정값으로 얼마에 한번씩 업데이트 할지 정할 수 있음. (물리, 정밀한것 처리할 때)
    {//velocity : 속도 
        Vector3 velocity = _agent.velocity;
        Vector3 local = this.transform.InverseTransformDirection(velocity);//역행렬, 해당(world, view, proj)중에 하나 빼기 위해서//velocity를 넣으면 방향성을 빼서 속력이 남는다.

        float speed = local.z / _agent.speed;
        _animator.SetFloat("Speed", speed);
    }

    //private Ray GetMouseRay()
    //{
    //    return Camera.main.ScreenPointToRay(Input.mousePosition);//메인카메라가 바라보고있는 방향으로 레이져를 쏘는 값
    //}

    public void Begin(object obj)
    {
        Debug.Assert((obj is Vector3), "Input type : Vector3");//obj == Vector3

        _actionManager.StartAction(this);
        MoveTo((Vector3)obj);
    }

    public void End()
    {
        _agent.isStopped = true;
    }

    public void MoveTo(Vector3 destination)
    {
        _agent.isStopped = false;
        _agent.destination = destination;
    }
}
