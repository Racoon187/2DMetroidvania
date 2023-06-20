using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("���")]
    public TextMeshProUGUI _text; //�ı���

    [Header("�ı��ļ�")]
    public TextAsset _textFile; //�ı��ļ�TXT
    public int index; //���


    List<string> textList = new List<string>();
    void Awake()
    {
        GetTextFromFile(_textFile);
    }

    private void OnEnable()
    {
        _text.text = textList[index];
        index++;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _text.text = textList[index];
            index++;
        }
        if(Input.GetKeyDown(KeyCode.Space) && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear(); //�������
        index = 0;

        var lineData = file.text.Split('\n'); //�����и�

        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }
}
