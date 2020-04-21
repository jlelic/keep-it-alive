using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Text tutorialText;

    [SerializeField] GameObject dodgeCars;
    [SerializeField] GameObject collectWrench;
    [SerializeField] GameObject dodgeDirt;
    [SerializeField] GameObject collectWater;
    [SerializeField] GameObject collectAll;
    [SerializeField] RectTransform tutorialBox;


    RoadsManager roadsManager;
    int tutorialLevel = 0;

    bool gasCollected;
    bool icecreamCollected;
    bool phoneCollected;
    bool gasUsed;
    bool icecreamUsed;
    bool phoneUsed;

    bool started;

    private void Start()
    {
        roadsManager = GetComponent<RoadsManager>();
    }

    public void StartTutorial()
    {
        if(started)
        {
            return;
        }
        started = true;
        tutorialText.text = "Use A and D to dodge incoming cars!";
        roadsManager.RoadPrefabs.Clear();
        roadsManager.ForcedSpawn = dodgeCars;
        roadsManager.RoadPrefabs.Add(collectWrench);
    }

    public void OnCarsDodged()
    {
        tutorialLevel = 1;
        tutorialText.text = "Ooops! Use SPACE to steal a wrench and repair your ship!";
    }
    public void OnWrenchStolen()
    {
        if (tutorialLevel != 1)
        {
            return;
        }
        tutorialLevel = 2;
        tutorialText.text = "Smooth! Now use mouse to drag the wrench from the inventory on the ship to repair it!";
    }
    public void OnShipRepaired()
    {
        if(tutorialLevel == 2)
        {
            tutorialLevel = 3;
            tutorialText.text = "The ship looks brand new now. Just be careful to not get it dirty.";
            roadsManager.RoadPrefabs.Clear();
            roadsManager.ForcedSpawn = dodgeDirt;
            roadsManager.RoadPrefabs.Add(collectWater);
        }
    }

    public void OnDirtDodged()
    {
        tutorialLevel = 4;
        tutorialText.text = "Oopsie! Good thing the ship has an automatic wiping device. Press W to activate it!";
    }

    public void OnWindowCleaned()
    {
        if (tutorialLevel == 4)
        {
            tutorialLevel = 5;
            tutorialText.text = "Much better! However this device requires water to function properly. Steal some!";
        }
    }

    public void OnWaterStolen()
    {
        if (tutorialLevel != 5)
        {
            return;
        }
        tutorialLevel = 6;
        tutorialText.text = "Now drag it to the front of the ship to fill it up!";
    }
    public void OnWaterRefilled()
    {
        tutorialLevel = 7;
        UpdateLastmessage();
        roadsManager.RoadPrefabs.Clear();
        roadsManager.RoadPrefabs.Add(collectAll);
    }

    public void OnGasRefilled()
    {
        gasUsed = true;
        UpdateLastmessage();
    }

    public void OnIcecreamUsed()
    {
        icecreamUsed = true;
        UpdateLastmessage();

    }

    public void OnBateryRecharged()
    {
        phoneUsed = true;   
        UpdateLastmessage();
    }
    public void OnGasStolen()
    {
        gasCollected = true;
        UpdateLastmessage();
    }

    public void OnIcecreamStolen()
    {
        icecreamCollected = true;
        UpdateLastmessage();
    }

    public void OnPhoneStolen()
    {
        phoneCollected = true;
        UpdateLastmessage();
    }

    void UpdateLastmessage()
    {
        if(phoneUsed && icecreamUsed && gasUsed)
        {
            CompleteTutorial();
            return;
        }
        var msg = "You're getting the hang of it! Here are your last tasks:\n";
        if (!phoneCollected)
        {
            msg += "- steal a phone\n";
        }
        else if (!phoneUsed)
        {
            msg += "- use phone to recharge ship's battery\n";
        }

        if (!icecreamCollected)
        {
            msg += "- steal icecream\n";
        }
        else if (!icecreamUsed)
        {
            msg += "- use icecream to cool down the engine\n";
        }

        if (!gasCollected)
        {
            msg += "- steal fuel canister\n";
        }
        else if (!gasUsed)
        {
            msg += "- refill fuel\n";
        }
        tutorialText.text = msg;
    }

    void CompleteTutorial()
    {
        if(tutorialLevel != 7)
        {
            return;
        }
        tutorialText.text = "Well done!\nNow if break the ship, go out of fuel or overheat the engine you'll lose.\n\nGood luck, off you go!";
        GameManager.Instance.StartTheGame();
        iTween.ValueTo(tutorialBox.gameObject, iTween.Hash(
            "from", tutorialBox.anchoredPosition,
            "to", tutorialBox.anchoredPosition + tutorialBox.sizeDelta*2,
            "time", 1,
            "delay", 5,
            "onupdate", (System.Action<Vector2>)((val) => { tutorialBox.anchoredPosition = val; })
            ));

    }
}
