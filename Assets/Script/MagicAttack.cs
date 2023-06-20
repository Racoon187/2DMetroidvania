using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    [SerializeField]private GameObject projectile;
    [SerializeField]private Transform FirePoint;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(PlayerStatus.MP > 0)
            {
                PlayerStatus.MP--;
                Instantiate(projectile, FirePoint.position, FirePoint.rotation);
            }
        }
    }
}