using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public float[] durations;
    public KeyCode[] validSequenceKeys;
    private KeyCode _chosenSequenceKey;
    [SerializeField] private int _timerBeforeQTEStart;
    private ScaleOverTime _scaleManager;
    private Animation _animation;

    [SerializeField] public GameObject _animatedBall;
    private Animator _animator => _animatedBall.GetComponent<Animator>();

    private void Start()
    {
        _animatedBall.SetActive(false);
        _scaleManager = GetComponent<ScaleOverTime>();
        _animation = _animatedBall.GetComponent<Animation>();
    }

    public void StartQTE(float QTEduration)
    {
        _chosenSequenceKey = validSequenceKeys[Random.Range(0, validSequenceKeys.Length)];
        Debug.Log(_chosenSequenceKey.ToString());
        _scaleManager.StartScaling(QTEduration, _chosenSequenceKey.ToString());
        _animatedBall.SetActive(true);
        _animator.Play(GetRandomAnimString());
        _animator.speed = 1/QTEduration;
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

    private string GetRandomAnimString()
    {
        string[] anims = new string[] { "anim1", "anim2"/*, "anim3"*/ };
        return anims[Random.Range(0, anims.Length)];
    }
}
