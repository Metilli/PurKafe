using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanelScript : MonoBehaviour
{
    public List<GameObject> Buttons;
    private GameObject selectedObject;
    public GameObject menuPanel;

    // Start is called before the first frame update
    void Start()
    {
        SetupMenuPanelButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupMenuPanelButtons()
    {
        var buttonHeight = menuPanel.GetComponent<RectTransform>().rect.height / Buttons.Count;
        var buttonWidth = menuPanel.GetComponent<RectTransform>().rect.width;
        for (int i = 0; i < Buttons.Count; i++)
        {
            RectTransform rect = Buttons[i].GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(buttonWidth, buttonHeight);
            rect.anchoredPosition = new Vector2(0, -((i * buttonHeight) + buttonHeight / 2));
        }
    }

    public void LoadReportsScene()
    {
        SceneManager.LoadScene("ReportDetail", LoadSceneMode.Single);
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    public void LoadAddProductScene()
    {
        SceneManager.LoadScene("AddProductScene", LoadSceneMode.Single);
    }
    public void LoadAddDeskScene()
    {
        SceneManager.LoadScene("AddDeskScene", LoadSceneMode.Single);
    }
}
