using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RectTransform GasBarContainer;
    public RectTransform GasBar;
    // Start is called before the first frame update
    GameManager GM;


    void Start()
    {
        GM = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGarBar();
    }

    private void UpdateGarBar()
    {
        GasBarContainer.sizeDelta = new Vector2(2 * GM.GasCapacity + 20, GasBarContainer.sizeDelta.y);
        GasBar.sizeDelta = new Vector2(2 * GM.GasLevel, GasBar.sizeDelta.y);
    }
}
