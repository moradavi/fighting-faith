using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WritingManager : MonoBehaviour
{
    public TextAsset[] writing;
    public string[] currentSentence;
    public int currentTextAsset;

    public Text dialogueBox;
    public int currentLine;
    public char[] currentLineLetters;
    public int numShowArray;
    public string showSentence;

    public bool startCo;
    public bool compSentence;
    public float addLetterTime;

    public GameObject illustration;
    public GameObject sceneTransition;

    // Start is called before the first frame update
    void Start()
    {
        if (writing != null)
        {
            currentSentence = writing[currentTextAsset].text.Split('\n');
        }

        currentLine = 0;
        showSentence = "";
        startCo = true;
        StartCoroutine(LetterAddition());
    }

    // Update is called once per frame
    void Update()
    {
        NextLine();
    }

    void NextLine()
    {
        if (Input.GetMouseButtonDown(0) && currentLine != currentSentence.Length && !compSentence|| currentLine == 0)
        {
            if (currentLine <= currentSentence.Length - 1 && compSentence)
            {
                StopAllCoroutines();
                compSentence = false;
            } else if (currentLine <= currentSentence.Length - 1 && !compSentence)
            {
                currentLine++;
                showSentence = "";
                numShowArray = 0;
                startCo = true;
                AddLetters();
            }
        } else if (Input.GetMouseButtonDown(0) && currentLine == currentSentence.Length && !compSentence)
        {
            if (currentTextAsset < writing.Length - 1)
            {
                illustration.SetActive(true);
                currentTextAsset++;
                currentSentence = writing[currentTextAsset].text.Split('\n');
                currentLine = 0;
                showSentence = "";
                startCo = true;
                Debug.Log("This should transition");
            }

            if (currentTextAsset == writing.Length - 1 && currentLine == currentSentence.Length)
            {
                sceneTransition.SetActive(true);
                sceneTransition.GetComponent<ManageScenes>().ChangeScene();
            }
        }
    }

    void AddLetters()
    {
        currentLineLetters = currentSentence[currentLine - 1].ToCharArray();

        if (startCo)
        {
            startCo = false;
            StartCoroutine(LetterAddition());
        }
    }

    IEnumerator LetterAddition()
    {
        compSentence = true;
        for (int i = 0; i < currentLineLetters.Length; i++)
        {
            showSentence += currentLineLetters[numShowArray];

            dialogueBox.GetComponent<Text>().text = showSentence;
            Debug.Log("running");
            numShowArray += 1;
            yield return new WaitForSeconds(addLetterTime);
        }
        compSentence = false;
    }
}
