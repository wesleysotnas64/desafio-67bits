using TMPro;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public int sacrificePoints;
    public TMP_Text textPoints;
    public int maxStack;
    public TMP_Text textMaxStack;
    public GameObject gameplayPanel;
    void Start()
    {
        sacrificePoints = 0;

        textPoints.text = $"{sacrificePoints}";
        UpdateMaxStack();
        textMaxStack.text = $"Stack: {3}";

        InitialSetUp();

    }

    private void InitialSetUp()
    {
        GameObject.Find("Camera").GetComponent<CameraMove>().offset = new(0, 1.3f, -4);
        GameObject.Find("Player").transform.forward *= -1;
        GameObject.Find("CoinStartSpawn").GetComponent<CoinStartSpawn>().enable = false;
        gameplayPanel.SetActive(false);
        GameObject.Find("ButtonPlay").SetActive(true);
    }

    public void InitGame()
    {
        GameObject.Find("Camera").GetComponent<CameraMove>().offset = new(0, 10, -13);
        GameObject.Find("Player").transform.forward = Vector3.forward;
        StartCoroutine(
            GameObject.Find("CoinStartSpawn").GetComponent<CoinStartSpawn>().TranslateEnable()
        );
        gameplayPanel.SetActive(true);
        GameObject.Find("ButtonPlay").SetActive(false);
    }

    public void Point()
    {
        sacrificePoints++;
        textPoints.text = $"{sacrificePoints}";
    }

    public void UpdateMaxStack()
    {
        maxStack = GameObject.Find("StackController").GetComponent<StackController>().maxStack;
        textMaxStack.text = $"Stack: {maxStack}";
    }
}
