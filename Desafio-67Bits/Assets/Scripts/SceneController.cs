using System;
using TMPro;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [Header("Gameplay")]
    public int sacrificePoints;
    public int maxStack;
    public int maxSpawn;
    public TMP_Text textMaxSpawn;
    public TMP_Text textPoints;
    public TMP_Text textMaxStack;
    public GameObject gameplayPanel;
    public FixedJoystick fixedJoystick;
    public GameObject fixedJoystickGameObject;

    [Header("Store")]
    public GameObject storePanel;
    public int clubPrice;
    public int stackPrice;
    public int spawnPrice;
    public TMP_Text textClubPrice;
    public TMP_Text textBuyUnavailable;
    public TMP_Text textStackPrice;
    public TMP_Text textSpawnPrice;
    void Start()
    {
        sacrificePoints = 0;
        clubPrice = 15;
        maxSpawn = 5;

        textPoints.text = $"{sacrificePoints}";
        textMaxStack.text = $"x {1}";
        textMaxSpawn.text = $"x {maxSpawn}";

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

    public void UpdateUI()
    {
        maxStack = GameObject.Find("StackController").GetComponent<StackController>().maxStackSlot;
        stackPrice = GetFibonacciCost(maxStack);
        maxSpawn = GameObject.Find("SkullEnemySpawner").GetComponent<SkullEnemySpawner>().quantity;
        spawnPrice = GetFibonacciCost(maxSpawn);

        textPoints.text = $"x {sacrificePoints}";
        textMaxStack.text = $"x {maxStack}";
        textStackPrice.text = $"x {stackPrice}";
        textMaxSpawn.text = $"x {maxSpawn}";
        textSpawnPrice.text = $"x {spawnPrice}";
        textClubPrice.text = $"x {clubPrice}";
    }

    public void OpenStore()
    {
        fixedJoystickGameObject.SetActive(false);
        storePanel.SetActive(true);
        UpdateUI();

        // Zera o joystick
        fixedJoystick.OnPointerUp(new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current));
    }


    public void CloseStore()
    {
        fixedJoystickGameObject.SetActive(true);
        storePanel.SetActive(false);
    }

    public void BuyClub()
    {
        int cost = 15;
        if (sacrificePoints >= cost && !GameObject.Find("Player").GetComponent<PlayerMove>().clubIsActive)
        {
            GameObject.Find("Player").GetComponent<PlayerMove>().clubIsActive = true;
            sacrificePoints -= cost;
            textBuyUnavailable.text = "Unavailable";
            textBuyUnavailable.color = Color.red;
            UpdateUI();
        }
    }

    public void BuySlot()
    {
        int cost = GetFibonacciCost(maxStack);
        if (sacrificePoints >= cost)
        {
            GameObject.Find("StackController").GetComponent<StackController>().maxStackSlot++;
            maxStack++;
            sacrificePoints -= cost;
            stackPrice = GetFibonacciCost(maxStack);
            UpdateUI();
        }
    }

    public void BuySpawn()
    {
        int cost = GetFibonacciCost(maxSpawn);
        if (sacrificePoints >= cost)
        {
            GameObject.Find("SkullEnemySpawner").GetComponent<SkullEnemySpawner>().quantity++;
            maxSpawn++;
            sacrificePoints -= cost;
            spawnPrice = GetFibonacciCost(maxSpawn);
            UpdateUI();
        }
    }

    public int GetFibonacciCost(int currentSlots)
    {
        currentSlots += 2;
        if (currentSlots == 1) return 1;

        int a = 1;
        int b = 1;
        int result = 1;

        for (int i = 3; i <= currentSlots; i++)
        {
            result = a + b;
            a = b;
            b = result;
        }

        return result;
    }
}
