using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;

public class TopPanelScript : MonoBehaviour
{
    public Text date;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        date.text = System.DateTime.Now.ToString();
    }
}
