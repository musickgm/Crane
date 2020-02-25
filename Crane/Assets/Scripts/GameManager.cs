using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles game logic (start, finish, collectables, communicates with UI, and handles sfx
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Public Variables
    public int gameTime = 60;
    public Text scoreText;                          //Text displaying the score
    public Text timeText;
    public CanvasGroup popupGroup;                  //Displays the initial and final instructions
    public Text popupText;
    public CraneController controller;             //The player controller
    public AudioClip collectAudio;

    #endregion
    #region Private Variables
    private int startingScore = 0;                 //Score to start with
    private float score;
    private IEnumerator popupCoroutine;
    private IEnumerator gameCoroutine;

    #endregion


    /// <summary>
    /// Start is called before the first frame update. Set all the UI and coroutines
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        score = startingScore;
        UpdateScore();
        timeText.text = gameTime.ToString();

        popupCoroutine = InstructionFade(0, 3.5f, 2);
        StartCoroutine(popupCoroutine);
        gameCoroutine = GameTime();
        StartCoroutine(gameCoroutine);
    }



    /// <summary>
    /// Update is called once per frame. Look for quitting or restarting input. Update the score
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }



    /// <summary>
    /// Every frame update the score
    /// </summary>
    private void UpdateScore()
    {
        scoreText.text = "$ " + score.ToString();
    }

    /// <summary>
    /// Finish game logic - apply message, stop player controller, stop changing the score.
    /// </summary>
    private void FinishGame()
    {
        popupText.text = "FAIRY EARNINGS = $" + score.ToString() + "\n PRESS 'R' TO RESTART";
        popupCoroutine = InstructionFade(1, 0, 2);
        StartCoroutine(popupCoroutine);
        controller.SetGameOver();
    }


    public void PlayAudio(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip, 0.7f);
    }

    public void CollectItem(float value)
    {
        score += value;
        PlayAudio(collectAudio);
        UpdateScore();
    }


    /// <summary>
    /// Fade the popup canvas
    /// </summary>
    /// <param name="finalAlpha"></param>Either 1 or 0 (on or off)
    /// <param name="initialWait"></param> How long before fading?
    /// <param name="timeToFade"></param> How long does it take to fade?
    /// <returns></returns>
    private IEnumerator InstructionFade(float finalAlpha, float initialWait, float timeToFade)
    {
        yield return new WaitForSeconds(initialWait);
        float startingAlpha = popupGroup.alpha;
        float t = 0;
        while(popupGroup.alpha != finalAlpha)
        {
            t += Time.deltaTime;
            popupGroup.alpha = Mathf.Lerp(startingAlpha, finalAlpha, t / timeToFade);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator GameTime()
    {
        //Wait the amount of time equal to the instructions
        yield return new WaitForSeconds(3.5f);

        while(gameTime > 0)
        {
            yield return new WaitForSeconds(1);
            gameTime--;
            timeText.text = gameTime.ToString() + " s";
        }
        FinishGame();
    }

}
