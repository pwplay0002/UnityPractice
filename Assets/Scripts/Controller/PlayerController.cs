using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Moving _moving = null;
    private Attacking _attacking = null;

    private void Awake()
    {
        _moving = this.GetComponent<Moving>();
        _attacking = this.GetComponent<Attacking>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Attacking() == true)
            return;

        if (Moving() == true)
            return;
    }

    private bool Attacking()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());//배열 자료형 만들때 []붙여버림
        foreach(RaycastHit hit in hits)//range based for loop 와 유사함
        {
            Damage damage = hit.transform.GetComponent<Damage>();
            if (damage == null)
                continue;

            if (Input.GetMouseButton(0))
                _attacking.Begin(damage);

            return true;
        }
        return false;
    }

    private bool Moving()
    {
        if (Input.GetMouseButton(0))//0 왼쪽 1오른쪽버튼
        {
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit, 500))//값을 hit에다가 넣는것.//500안쪽으로 안잡히면 체크 x
            {
                _moving.Begin(hit.point);
                return true;
            }
        }

        return false;
    }

    private Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);//메인카메라가 바라보고있는 방향으로 레이져를 쏘는 값
    }
}
