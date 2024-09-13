using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    [SerializeField] private GameObject _qteParent;
    [SerializeField] public TextMeshPro _qteText;
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
                _qteManager.PlayLoseParticles();
                _qteManager.shouldHitterStayUp = true;
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
                _qteManager.hitterAnimator.Play(GetHitterAnimationName(_qteManager.chosenHitter.name));
                _qteManager.PlayOutgoingAnim(isLastBall);


                foreach (SpriteRenderer item in _qteManager.impactFrameSprites)
                {
                    item.enabled = true;
                    StartCoroutine(DissapearImpactFramesCoroutine());
                }

                Camera sceneCamera = Camera.main;
                sceneCamera.DOShakePosition(0.5f, 0.3f, 10, 90, false);

                if (isLastBall)
                {
                    _homeRunText.SetActive(true);
                    _homeRunText.transform.DOScale(1f, 1.0f).SetEase(Ease.OutBounce);
                    _qteManager.shouldHitterStayUp = true;
                    _qteManager.ChangeBackground(true, false);
                    _qteManager.PlayWinParticles();
                }
                else
                {
                    _qteManager.shouldHitterStayUp = false;
                    StartCoroutine(DissapearHitterCouroutine());
                    _qteManager.ChangeBackground(true);
                }
            }
            else
            {
                _qteManager.shouldHitterStayUp = true;
                _qteManager.ChangeBackground(false);
                _qteManager.PlayLoseParticles();
            }
        }
        else
        {
            _qteManager.shouldHitterStayUp = true;
            _qteManager.ChangeBackground(false);
            _qteManager.PlayLoseParticles();
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

    public void SwitchQTEVisibility()
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

    private IEnumerator DissapearImpactFramesCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (SpriteRenderer item in _qteManager.impactFrameSprites)
        {
            item.enabled = false;
        }
    }

    private string GetHitterAnimationName(string hitterName)
    {
        switch (hitterName)
        {
            case "FrappeurHammer":
                return "FrappeBallHammer";
            case "Frappeur":
                return "FrappeBall";
            case "Frappeur2":
                return "FrappeBallBatte";
            case "Frappeur3":
                return "FrappeBallWrench";
            default:
                return " ";
        }
    }
}
