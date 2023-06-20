using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMP : MonoBehaviour
{

    public Slider UIMP;

    // Update is called once per frame
    void Update()
    {
        UIMP.maxValue = PlayerStatus.MAXMP;
        UIMP.value = PlayerStatus.MP;
    }
}
