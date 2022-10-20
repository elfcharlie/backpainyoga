using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI stretchText;
    public AudioClip countDown;
    public AudioClip changeStretch;
    private AudioSource audioSource;
    private bool isActive;
    private bool isPaused;
    private bool isCountingDown;
    private float timer;
    private bool stretching;
    private int stretchNumber;
    private string[] stretchmoves = {"CHILD'S POSE", "CAT COW", "THREAD THE NEEDLE, R"
    , "THREAD THE NEEDLE, L", "SPHINX", "STANDING FORWARD FOLD", "YOGI  SQUAT"
    , "SEATED TWIST, R", "SEATED TWIST, L", "KNEE HUG", "DYNAMIC SHOULDER BRIDGE"
    , "RECLINING PIGEON, R", "RECLINING PIGEON, L", "FULL BODY STRETCH", "LYING TWIST, R"
    , "LYING TWIST, L", "DEEP RELAXATION"};
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1f;
        isActive = false;
        isPaused = false;
        isCountingDown = false;
        stretchNumber = 0;
        timer = 10;   
        stretching = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive){
            timer -= Time.deltaTime;
        }
        if(timer < 0 && stretching == false)
        {
            timer = 31;
            stretching = true;
            audioSource.PlayOneShot(changeStretch, 1f);
        }
        else if (timer < 0 && stretching == true)
        {
            timer = 10;
            stretching = false;
            stretchNumber += 1;
            audioSource.PlayOneShot(changeStretch, 1f);
        }
        if (isActive && (int)timer == 3 && !isCountingDown)
        {
            isCountingDown = true;
            StartCoroutine(CountDown());
        }
        if (stretchNumber < stretchmoves.Length)
        {
            timerText.SetText(((int)timer).ToString());
            stretchText.SetText(stretchmoves[stretchNumber]);
        }
        else 
        {
            timer = 0;
            isActive = false;
            timerText.SetText(((int)timer).ToString());
            stretchText.SetText("DONE!");
        }
    }

    public void StartTimer()
    {
        isActive = true;
        isCountingDown = false;
    }

    public void Pause()
    {
        if(isActive == true && !isPaused)
        {
            isActive = false;
            isPaused = true;
        }
        else if(isPaused)
        {
            isActive = true;
            isPaused = false;
        }
    }

    public void Reset()
    {
        isPaused = false;
        stretching = false;
        stretchNumber = 0;
        timer = 10;
        isActive = false;
    }

    IEnumerator CountDown()
    {
        for (int i=1; i <= 3; i++)
        {
        audioSource.PlayOneShot(countDown, i/3f);
        yield return new WaitForSeconds(1);
        }
        isCountingDown = false;
    }
}
