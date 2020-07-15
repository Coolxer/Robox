using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Manager : MonoBehaviour
{
    public Button restartBtn;

    public Button showHideBtn;

    public Button exitBtn;

    public GameObject leftPanel;
    public GameObject rightPanel;
    
    private bool visibleSides = true;

    // Start is called before the first frame update
    void Start()
    {
        restartBtn.onClick.AddListener(restart);
        showHideBtn.onClick.AddListener(showHide);
        exitBtn.onClick.AddListener(exit);
    }

    private void restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }

    private void showHide() 
    {
        visibleSides = !visibleSides;

        leftPanel.SetActive(visibleSides);
        rightPanel.SetActive(visibleSides);

        string text = showHideBtn.GetComponentInChildren<Text>().text;
        showHideBtn.GetComponentInChildren<Text>().text = visibleSides ? "HIDE" : "SHOW";
    }

    private void exit()
    {
        Application.Quit();
    }
}
