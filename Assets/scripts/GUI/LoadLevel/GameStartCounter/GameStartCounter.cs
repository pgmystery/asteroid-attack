using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameStartCounter : MonoBehaviour
{
    public int startTime=5;

    private int currentCounter;

    [SerializeField]
    private Animator animatorComponent;
    [SerializeField]
    private Text counterText;

    private Action globalCallback;

    public static GameStartCounter gameStartCounter;

    void Awake() {
        if (gameStartCounter == null) {
            gameStartCounter = this;
        }
        else if (gameStartCounter != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        if (animatorComponent == null) {
            Debug.LogError("GameStartCounter: no 'animatorComponent' set!");
        }
        if (counterText == null) {
            Debug.LogError("GameStartCounter: no 'counterText' set!");
        }
        // StartCoroutine(FirstStartCounting(startTime));
    }

    public IEnumerator FirstStartCounting(int time, Action callback) {
        globalCallback = callback;
        yield return new WaitForSeconds(1);
        StartCounting(time);
    }

    void StartCounting(int time) {
        transform.GetChild(0).gameObject.SetActive(true);
        animatorComponent.enabled = true;
        RunAnimation(time);
    }

    void CountingFinished() {
        // animatorComponent.Play("idle");  //TODO: DO I NEED THIS???
        animatorComponent.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        globalCallback();
    }

    void RunAnimation(int time) {
        currentCounter = time;
        if (time != 0) {
            counterText.text = time.ToString();
        }
        else {
            counterText.text = "START!";
        }
        animatorComponent.Play("WaveCountdown", -1, 0);
    }

    void CountingAnimationFinished() {
        currentCounter--;
        if (currentCounter >= 0) {
            StartCounting(currentCounter);
        }
        else {
            CountingFinished();
        }
    }
}

