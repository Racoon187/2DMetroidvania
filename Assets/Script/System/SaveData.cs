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

    [Header("�洢���")]
    public Inventory myInventory;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    //���λ��
    //״̬����
    //NPC����
    //��Ʒ����

    public void SaveGame()
    {
        Debug.Log(Application.persistentDataPath);

        if(!Directory.Exists(Application.persistentDataPath + "/game_SaveData"))  //û���ļ��еĻ�: ����
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");
        }

        BinaryFormatter formatter = new BinaryFormatter(); //������ת��

        FileStream file = File.Create(Application.persistentDataPath + "/game_SaveData/inventory.txt");  //�洢�ļ�

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











    ////////�������
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

