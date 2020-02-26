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
    public int gameTime = 60;                       //How long the game should last
    public Text scoreText;                          //Text displaying the score
    public Text timeText;                           //Text displaying remaining time
    public CanvasGroup popupGroup;                  //Displays the initial and final instructions
    public Text popupText;                          //Pop up text for instructions
    public CraneController controller;             //The player controller
    public AudioClip collectAudio;                  //Audio played when we collect (on pillow)
    [HideInInspector]
    public bool initialWait = true;                 //Bool handling initial game logic
    [HideInInspector]
    public bool gameOver = false;                   //Bool handling end game logic

    #endregion
    #region Private Variables
    private float score = 0;
    private readonly float initialWaitTime = 3.5f;
    private IEnumerator popupCoroutine;
    private IEnumerator gameCoroutine;

    #endregion


    /// <summary>
    /// Start is called before the first frame update. Set all the UI and coroutines
    /// </summary>
    void Start()
    {
        initialWait = true;
        //Hide the cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        //Set initial score/time
        UpdateScore();
        timeText.text = gameTime.ToString();
        //Start initial coroutines
        popupCoroutine = InstructionFade(0, initialWaitTime, 2);
        StartCoroutine(popupCoroutine);
        gameCoroutine = GameTime(initialWaitTime);
        StartCoroutine(gameCoroutine);
    }

    /// <summary>
    /// Update is called once per frame. Look for quitting or restarting input.
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
    /// When an item is collected, update the score text
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
        gameOver = true;
    }


    /// <summary>
    /// Plays audio sent to it
    /// </summary>
    /// <param name="clip"></param>The clip to be played
    public void PlayAudio(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip, 0.7f);
    }

    /// <summary>
    /// Handles collecting items
    /// </summary>
    /// <param name="value"></param>The value of the collected item
    public void CollectItem(float value)
    {
        if(gameOver)
        {
            return;
        }
        score += value;
        PlayAudio(collectAudio);
        UpdateScore();
    }

    /// <summary>
    /// Fade the popup canvas
    /// </summary>
    /// <param name="finalAlpha"></param>Either 1 or 0 (on or off)
    /// <param name="waitTime"></param> How long before fading?
    /// <param name="timeToFade"></param> How long does it take to fade?
    /// <returns></returns>
    private IEnumerator InstructionFade(float finalAlpha, float waitTime, float timeToFade)
    {
        yield return new WaitForSeconds(waitTime);
        float startingAlpha = popupGroup.alpha;
        float t = 0;
        while(popupGroup.alpha != finalAlpha)
        {
            t += Time.deltaTime;
            popupGroup.alpha = Mathf.Lerp(startingAlpha, finalAlpha, t / timeToFade);
            yield return new WaitForEndOfFrame();
        }
        initialWait = false;
        yield return null;
    }

    /// <summary>
    /// Handles the game time (how much time is left in the game)
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameTime(float waitTime)
    {
        //Wait the amount of time equal to the instructions before starting
        yield return new WaitForSeconds(waitTime);

        //Every second, reduce the amount of time left (and update time text)
        while(gameTime > 0)
        {
            yield return new WaitForSeconds(1);
            gameTime--;
            timeText.text = gameTime.ToString() + " s";
        }
        //When no time is left, end the game
        FinishGame();
        yield return null;
    }

}
