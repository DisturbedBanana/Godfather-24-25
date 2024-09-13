using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public float[] durations;
    public KeyCode[] validSequenceKeys;
    private KeyCode _chosenSequenceKey;
    [SerializeField] private int _timerBeforeQTEStart;
    private ScaleOverTime _scaleManager;
    private Animation _animation;
    public bool shouldHitterStayUp = true;

    public ParticleSystem[] winEffects;
    public ParticleSystem[] loseEffects;
    public SpriteRenderer[] impactFrameSprites;

    public SpriteRenderer background;
    public Sprite normalBackGround;
    public Sprite winBackGround;
    public Sprite loseBackGround;

    public string[] incomingAnims;
    public string[] outgoingAnims;
    public GameObject[] hittersList;
    public GameObject chosenHitter;
    public Animator hitterAnimator;

    [SerializeField] public GameObject _animatedBall;
    private Animator _ballAnimator => _animatedBall.GetComponent<Animator>();

    private void Start()
    {
        _animatedBall.SetActive(false);
        _scaleManager = GetComponent<ScaleOverTime>();
        _animation = _animatedBall.GetComponent<Animation>();

        foreach (ParticleSystem effect in winEffects)
            effect.Stop();
    }

    public void StartQTE(float QTEduration)
    {
        _chosenSequenceKey = validSequenceKeys[Random.Range(0, validSequenceKeys.Length)];
        Debug.Log(_chosenSequenceKey.ToString());
        _scaleManager.StartScaling(QTEduration, _chosenSequenceKey.ToString());
        _animatedBall.SetActive(true);
        _ballAnimator.Play(GetRandomAnimString(incomingAnims));
        _ballAnimator.speed = 1/QTEduration;

        if (!shouldHitterStayUp)
        {
            chosenHitter = hittersList[Random.Range(0, hittersList.Length)];
            hitterAnimator = chosenHitter.GetComponent<Animator>();
            chosenHitter.SetActive(true);
        }
    }

    private void OnGUI()
    {
        if (_scaleManager.isScaling)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                if (e.keyCode == _chosenSequenceKey)
                {
                    _scaleManager.StopScaling(true);
                }
                else if(e.keyCode != _chosenSequenceKey && validSequenceKeys.Contains(e.keyCode))
                {
                    _scaleManager.StopScaling(false);
                }
            }
        }
    }

    private void Update()
    {
        if (!_scaleManager.isScaling)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                StartCoroutine(StartQTETimer(durations[0]));
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                StartCoroutine(StartQTETimer(durations[1]));
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                StartCoroutine(StartQTETimer(durations[2]));
            }
            else if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                StartCoroutine(StartQTETimer(durations[2]));
                StartCoroutine(ChangeCorrectKeyMidQTE(durations[2]));
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                _scaleManager.SwitchQTEVisibility();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                _scaleManager.isLastBall = true;
            }
        }
        
    }

    private IEnumerator StartQTETimer(float duration)
    {
        yield return new WaitForSeconds(_timerBeforeQTEStart);
        StartQTE(duration);
    }

    private string GetRandomAnimString(string[] list)
    {
        string[] anims = new string[] { "anim1", "anim2"/*, "anim3"*/ };
        return list[Random.Range(0, list.Length)];
    }

    public void PlayOutgoingAnim(bool isLastBall)
    {
        _animatedBall.SetActive(true);
        if (!isLastBall)
        {
            _ballAnimator.Play("animOut");
            return;
        }

        _ballAnimator.Play("animHomeRun");
    }

    public void ChangeBackground(bool hasWon, bool shouldGoBackToNormal = true)
    {
        if (hasWon)
            background.sprite = winBackGround;
        else
            background.sprite = loseBackGround;

        if(shouldGoBackToNormal)
            StartCoroutine(ChangeBackgroundBackToNormal());
    }

    private IEnumerator ChangeBackgroundBackToNormal()
    {
        yield return new WaitForSeconds(2.0f);
        background.sprite = normalBackGround;
    }

    public void PlayWinParticles()
    {
        foreach (ParticleSystem effect in winEffects)
        {
            effect.Play();
        }
    }

    public void PlayLoseParticles()
    {
        foreach (ParticleSystem effect in loseEffects)
        {
            effect.Play();
        }
    }

    public IEnumerator ChangeCorrectKeyMidQTE(float QTEDuration)
    {
        yield return new WaitForSeconds(QTEDuration/2 + _timerBeforeQTEStart);
        KeyCode oldKey = _chosenSequenceKey;
        KeyCode newKey = validSequenceKeys[Random.Range(0, validSequenceKeys.Length)];

        while (newKey == oldKey)
        {
            newKey = validSequenceKeys[Random.Range(0, validSequenceKeys.Length)];
        }

        _chosenSequenceKey = newKey;
        _scaleManager._qteText.text = newKey.ToString();
    }
}
