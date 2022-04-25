using System.Collections;
using UnityEngine;
using TMPro;


[System.Serializable]
public class TutorialElement
{
    [TextArea]
    public string dialog;
    public GameObject[] tutorialItems;

    public void SetElementActive()
    {
        foreach(GameObject item in tutorialItems)
            item.SetActive(true);
    }

    public void SetElementInactive()
    {
        foreach (GameObject item in tutorialItems)
            item.SetActive(false);
    }
}

public class Tutorial : MonoBehaviour
{
    int currentTutElement = 0;
    
    public TutorialElement[] elements;

    public GameObject tutorialPanel;
    public TMP_Text dialogTxt;

    private void Awake()
    {
        foreach (var element in elements)
            element.SetElementInactive();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
            NextTutorial();
        
        if(Input.GetKeyDown(KeyCode.A))
            PreviousTutorial();
    }


    void Start()
    {
        elements[currentTutElement].SetElementActive();
        StartCoroutine(SetDialog(elements[currentTutElement].dialog));
    }

    public void NextTutorial()
    {
        if (currentTutElement + 1 < elements.Length)
        {
            StopCoroutine(SetDialog(""));
            dialogTxt.text = "";
            elements[currentTutElement].SetElementInactive();

            currentTutElement++;

            elements[currentTutElement].SetElementActive();
            StartCoroutine(SetDialog(elements[currentTutElement].dialog));
        }

        else return;
    }

    public void PreviousTutorial()
    {
        if (currentTutElement - 1 >= 0)
        {
            StopCoroutine(SetDialog(""));
            dialogTxt.text = "";
            elements[currentTutElement].SetElementInactive();

            currentTutElement--;

            elements[currentTutElement].SetElementActive();

            StartCoroutine(SetDialog(elements[currentTutElement].dialog));
        }

        else return;
    }

    public IEnumerator SetDialog(string dialog)
    {
        foreach (char character in dialog)
        {
            dialogTxt.text += character.ToString();
            yield return new WaitForSeconds(0.01f);
        }
    }
}
