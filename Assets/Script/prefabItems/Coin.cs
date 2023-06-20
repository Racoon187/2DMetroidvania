using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    static public int MyCoin;

    public Transform target;  //Ŀ��(���)
    public float Radius; //��Ҽ��뾶
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    // Update is called once per frame
    void Update()
    {
        GetCoin();
    }

    private void GetCoin() //���ʰȡ
    {
        if (Mathf.Abs(transform.position.x - target.position.x) < Radius)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("GetCoin");
            MyCoin += 1;
            Destroy(gameObject);
        }
    }
}
