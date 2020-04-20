using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningManager : MonoBehaviour
{
    [SerializeField] GameObject warningTextPrefab;

    Dictionary<WarningMessageType, Text> displayedMessages = new Dictionary<WarningMessageType, Text>();
    Dictionary<WarningMessageType, Coroutine> timerCoroutines = new Dictionary<WarningMessageType, Coroutine>();

    void Start()
    {
        displayedMessages.Clear();
    }

    public void addWarning(WarningMessageType type, float time = 0)
    {
        var newTextObject = Instantiate(warningTextPrefab);
        newTextObject.transform.parent = transform;
        var text = newTextObject.GetComponentInChildren<Text>();
        text.transform.position += new Vector3(Random.Range(-Screen.width/4, Screen.width / 4), 0, 0);
        removeWarning(type);
        displayedMessages.Add(type, text);
        SetTextAndColor(type, text);

        Coroutine oldCoroutine = null;
        if(timerCoroutines.TryGetValue(type, out oldCoroutine))
        {
            timerCoroutines.Remove(type);
            StopCoroutine(oldCoroutine);
        }

        if(time>0)
        {
            var coroutine = StartCoroutine(TimeOutWarning(type, time));
            timerCoroutines.Add(type, coroutine);
        }
    }

    public void removeWarning(WarningMessageType type)
    {
        Text text; 
        if (displayedMessages.TryGetValue(type, out text))
        {
            Destroy(text.transform.parent.gameObject);
            displayedMessages.Remove(type);
        }
    }

    IEnumerator TimeOutWarning(WarningMessageType type, float time)
    {
        yield return new WaitForSeconds(time);
        removeWarning(type);
    }

    void SetTextAndColor(WarningMessageType type, Text textComponent)
    {
    //    ENGINE_BROKEN,
    //GAS_LEAK,
    //GAS_LOW,
    //OVERHEATING,
    //POWER_LOW,
    //POWER_INSUFFICIENT,
    //POWER_ZERO,
    //WATER_LOW,
    //WATER_INSUFFICIENT
            switch (type)
            {
            case WarningMessageType.ENGINE_DAMAGED:
                textComponent.text = "ENGINE DAMAGED";
  //              textComponent.color = Color.yellow;
                break;
            case WarningMessageType.ENGINE_CRITICAL:
                textComponent.text = "ENGINE CRITICAL";
//                textComponent.color = Color.yellow;
                break;
            case WarningMessageType.GAS_LOW:
                    textComponent.text = "GAS LOW";
                    break;
                case WarningMessageType.GAS_LEAK:
                    textComponent.text = "You're losing gas, find a way to seal the leak";
                    break;
                case WarningMessageType.OVERHEATING:
                    textComponent.text = "ENGINE OVERHEATING";
                    break;
                case WarningMessageType.POWER_INSUFFICIENT:
                    textComponent.text = "POWER LOW";
                    break;
                case WarningMessageType.WATER_LOW:
                    textComponent.text = "WATER LOW";
                    break;
                case WarningMessageType.WATER_INSUFFICIENT:
                    textComponent.text = "NOT ENOUGH WATER TO CLEAN THE WINDOW";
                    break;
                //                    textComponent.color = Color.blue;
                break;
                // TODO ETC
            }
    }
}
