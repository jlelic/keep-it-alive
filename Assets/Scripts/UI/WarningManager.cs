using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningManager : MonoBehaviour
{
    public Text[] textComponents;
    

    HashSet<WarningMessageType> displayedMessages = new HashSet<WarningMessageType>();

    void Start()
    {
        addWarning(WarningMessageType.ENERGY_LOW);
    }

    public void addWarning(WarningMessageType type)
    {
        displayedMessages.Add(type);
        updateWarnings();
    }

    public void removeWarning(WarningMessageType type)
    {
        displayedMessages.Remove(type);
        updateWarnings();
    }

    void updateWarnings()
    {
        clearWarnings();

        int i = 0;
        foreach (WarningMessageType type in displayedMessages)
        {
            if (i < textComponents.Length)
            {
                switch(type)
                {
                    case WarningMessageType.ENERGY_LOW:
                        textComponents[i].text = "Your energy is running low";
                        textComponents[i].color = Color.yellow;
                        break;
                    case WarningMessageType.GAS_LOW:
                        textComponents[i].text = "You're running out of gas";
                        textComponents[i].color = Color.green;
                        break;
                    case WarningMessageType.GAS_LEAK:
                        textComponents[i].text = "You're losing gas, find a way to seal the leak";
                        textComponents[i].color = Color.green;
                        break;
                    case WarningMessageType.OVERHEAT:
                        textComponents[i].text = "Engine overheating! Cool it quick";
                        textComponents[i].color = Color.red;
                        break;
                    case WarningMessageType.WATER_INSUFFICIENT:
                        textComponents[i].text = "You don't have enough water to clean your window";
                        textComponents[i].color = Color.blue;
                        break;
                    // TODO ETC
                }
            }
            i++;
        }
    }

    void clearWarnings()
    {
        foreach (Text textComponent in textComponents)
        {
            textComponent.text = "";
        }
    }
}
