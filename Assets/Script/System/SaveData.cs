using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting.Dependencies.NCalc;

public class SaveData : MonoBehaviour
{
    public Animator _animator;
    private Transform playerTransform;

    [Header("存储相关")]
    public Inventory myInventory;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    //玩家位置
    //状态数据
    //NPC数据
    //物品数据

    public void SaveGame()
    {
        Debug.Log(Application.persistentDataPath);

        if(!Directory.Exists(Application.persistentDataPath + "/game_SaveData"))  //没有文件夹的话: 创建
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");
        }

        BinaryFormatter formatter = new BinaryFormatter(); //二进制转化

        FileStream file = File.Create(Application.persistentDataPath + "/game_SaveData/inventory.txt");  //存储文件

        var json = JsonUtility.ToJson(myInventory);

        formatter.Serialize(file, json);
        file.Close();
    }

    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();

        if(File.Exists(Application.persistentDataPath + "/game_SaveData/inventory.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_SaveData/inventory.txt", FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), myInventory);
            file.Close();
        }
    }











    ////////动画相关
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("PlayerCheck",true);
            Debug.Log("Check");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("PlayerCheck", false);
            Debug.Log("exit");
        }
    }
}

