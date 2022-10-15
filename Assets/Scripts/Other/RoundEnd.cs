using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundEnd : MonoBehaviour
{
    [SerializeField] RectTransform roundEndText;
    [SerializeField] RectTransform roundEndPrompt;
    [SerializeField] float animationTime = .5f;
    AudioSource winSfx;
    private float shiftAmountPrompt = 2000f;
    private float shiftAmountText = 2000f;

    [SerializeField] private bool textDisplayed = false;

    private void Awake()
    {
        winSfx = GetComponent<AudioSource>();

        foreach (FishHP hp in FindObjectsOfType<FishHP>())
        {
            hp.die.AddListener(DisplayText);
        }
    }

    private void Start()
    {
        roundEndText.localPosition += Vector3.up * shiftAmountText;
        roundEndPrompt.localPosition += Vector3.right * shiftAmountPrompt;
        textDisplayed = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && textDisplayed)
        {
            int randomScene = Random.Range(1, SceneManager.sceneCountInBuildSettings - 1);
            SceneManager.LoadScene(randomScene);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void DisplayText()
    {
        if (textDisplayed)
            return;

        winSfx.Play();
        FindObjectOfType<Soundtrack>().Mute();
        textDisplayed = true;
        StartCoroutine(DisplayTextCoroutine());
    }

    IEnumerator DisplayTextCoroutine()
    {
        roundEndText.LeanMoveLocalY(roundEndText.localPosition.y - shiftAmountText, animationTime).setEaseInBack();
        yield return new WaitForSeconds(animationTime);
        roundEndPrompt.LeanMoveLocalX(0f, animationTime).setEaseInBack();
    }
}
