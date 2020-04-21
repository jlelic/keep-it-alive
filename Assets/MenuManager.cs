using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject title;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject credits;
    [SerializeField] RectTransform tutorial;
    [SerializeField] RectTransform bars;

    Vector3 initialBarsPosition;

    void Start()
    {
        initialBarsPosition = bars.position;
        bars.position -= new Vector3(bars.sizeDelta.x, 0, 0);
    }

    public void PlayTutorial()
    {
        EnterGameMode(true);
    }
    public void PlayWithoutTutorial()
    {
        EnterGameMode(false);
    }
    public void PlayEndless()
    {
        GameManager.Instance.EndlessMode = true;
        EnterGameMode(false);
    }

    void EnterGameMode(bool tutorialMode)
    {
        GameManager.Instance.InMenu = false;
        iTween.MoveTo(bars.gameObject, initialBarsPosition, 2);
        iTween.MoveBy(title, new Vector3(0, Screen.height), 6);
        iTween.MoveBy(buttons, new Vector3(0, -Screen.height), 3);
        iTween.MoveBy(credits, new Vector3(0, -Screen.height/2), 5);
        iTween.MoveBy(tutorial.gameObject, new Vector3(Screen.width/2, 0), 1);
        if (tutorialMode)
        {
            StartCoroutine(ReturnTutorialBox());
            GetComponent<TutorialManager>().StartTutorial();
        }
        foreach(var button in buttons.GetComponentsInChildren<Button>()){
            button.enabled = false;
        }
        if(!tutorialMode)
        {
            GameManager.Instance.StartTheGame();
        }
    }

    IEnumerator ReturnTutorialBox()
    {
        yield return new WaitForSeconds(2.1f);
        tutorial.anchorMin = new Vector2(1, 1);
        tutorial.anchorMax = new Vector2(1, 1);
        tutorial.pivot = new Vector2(1, 1);
        iTween.ValueTo(tutorial.gameObject, iTween.Hash(
            "from", tutorial.anchoredPosition,
            "to", new Vector2(5, 4),
            "time", 1,
            "onupdate", (Action<Vector2>)((val) => { tutorial.anchoredPosition = val; })
            ));
    }
}
