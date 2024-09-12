using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    [SerializeField] private GameObject _qteParent;
    [SerializeField] private TextMeshPro _qteText;
    [SerializeField] private QTEManager _qteManager;
    [SerializeField] private GameObject _homeRunText;

    public bool isLastBall = false;

    private Vector3 _marginVector = new Vector3(0.2f, 0.2f, 0.2f);
    public Vector3 targetScale;  // Target scale to reach
    private Vector3 initialTargetScale;
    public float duration = 1.0f; // Time to reach the target scale
    private Vector3 initialScale; // Starting scale of the object
    private float timeElapsed = 0.0f; // Time counter
    public bool isScaling = false; // Scaling flag

    void Start()
    {
        initialScale = transform.localScale;  // Store the object's initial scale
        initialTargetScale = targetScale;
        targetScale -= _marginVector;
        _qteManager = GetComponent<QTEManager>();
        _qteText.text = " ";
    }

    void Update()
    {
        if (isScaling)
        {
            // Increment the time elapsed based on frame time
            timeElapsed += Time.deltaTime;

            // Calculate the interpolation factor (0 to 1)
            float t = timeElapsed / duration;

            // Lerp the scale from initial to target based on the factor
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            // If the scaling is done, stop updating
            if (t >= 1.0f)
            {
                SwitchQTEVisibility();
                isScaling = false;
                Debug.Log("lose, time out");
                DissapearBallAndText();
                _qteManager.ChangeBackground(false);
            }
        }
    }

    public void StopScaling(bool goodKey)
    {
        DissapearBallAndText();
        SwitchQTEVisibility();
        isScaling = false;

        if (goodKey)
        {
            float diff = transform.localScale.x - initialTargetScale.x;
            //Debug.Log($"{transform.localScale} {targetScale}");

            if (diff <= 0.1)
            {
                Debug.Log("win" + diff);
                _qteManager.PlayOutgoingAnim(isLastBall);
                _qteManager.hitterAnimator.Play("FrappeBallHammer");
                _qteManager.shouldHitterStayUp = false;
                StartCoroutine(DissapearHitterCouroutine());
                _qteManager.ChangeBackground(true);
            }
            else
            {
                _qteManager.shouldHitterStayUp = true;
                _qteManager.ChangeBackground(false);
            }
        }
        else
        {
            _qteManager.shouldHitterStayUp = true;
            _qteManager.ChangeBackground(false);
        }


        if (isLastBall)
        {
            _homeRunText.SetActive(true);
            _homeRunText.transform.DOScale(1f, 1.0f).SetEase(Ease.OutBounce);
            _qteManager.shouldHitterStayUp = true;
            _qteManager.ChangeBackground(true, false);
        }
    }

    // Call this method to start the scaling
    public void StartScaling(float givenDuration, string QTELetter)
    {
        SwitchQTEVisibility();
        transform.localScale = initialScale;  // Reset the scale to initial
        initialScale = transform.localScale;  // Refresh initial scale if the scale was changed
        timeElapsed = 0.0f;                   // Reset the time
        duration = givenDuration;             // Set the duration
        _qteText.text = QTELetter;
        isScaling = true;                     // Set the flag to start scaling
    }

    private void SwitchQTEVisibility()
    {
        SpriteRenderer[] renderers = _qteParent.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.enabled = !renderer.enabled;
        }
    }

    public void DissapearBallAndText()
    {
        _qteText.text = " ";
        _qteManager._animatedBall.SetActive(false);
    }

    private IEnumerator DissapearHitterCouroutine()
    {
        yield return new WaitForSeconds(2.0f);
        _qteManager.chosenHitter.SetActive(false);
    }
}
