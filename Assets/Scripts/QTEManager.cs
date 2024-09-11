using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public int[] durations;
    public KeyCode[] validSequenceKeys;
    private KeyCode _chosenSequenceKey;
    [SerializeField] private int _timerBeforeQTEStart;
    private ScaleOverTime _scaleManager;
    private void Start()
    {
        _scaleManager = GetComponent<ScaleOverTime>();
    }

    public void StartQTE(int QTEduration)
    {
        _chosenSequenceKey = validSequenceKeys[Random.Range(0, validSequenceKeys.Length)];
        Debug.Log(_chosenSequenceKey.ToString());
        _scaleManager.StartScaling(QTEduration, _chosenSequenceKey.ToString());

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
        }
        
    }

    private IEnumerator StartQTETimer(int duration)
    {
        yield return new WaitForSeconds(_timerBeforeQTEStart);
        StartQTE(duration);
    }
}
