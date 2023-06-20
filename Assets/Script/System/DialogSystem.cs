using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("组件")]
    public TextMeshProUGUI _text; //文本框

    [Header("文本文件")]
    public TextAsset _textFile; //文本文件TXT
    public int index; //编号


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
        textList.Clear(); //首先清空
        index = 0;

        var lineData = file.text.Split('\n'); //按行切割

        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }
}
