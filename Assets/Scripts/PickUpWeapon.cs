using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField]
    private Weapon _weapon = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<Attacking>().Equip(_weapon);
            Destroy(this.gameObject);
        }
    }
}
