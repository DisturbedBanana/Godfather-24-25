using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public KeyCode[] validSequenceKeys;
    private KeyCode _chosenSequenceKey;
    private ScaleOverTime _scaleManager;
    private void Start()
    {
        _scaleManager = GetComponent<ScaleOverTime>();
    }

    public void StartQTE(int QTEduration)
    {
        _chosenSequenceKey = validSequenceKeys[Random.Range(0, validSequenceKeys.Length)];
        Debug.Log(_chosenSequenceKey.ToString());
        _scaleManager.StartScaling(QTEduration);

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
                else
                {
                    _scaleManager.StopScaling(false);
                }
            }
        }
    }
}
