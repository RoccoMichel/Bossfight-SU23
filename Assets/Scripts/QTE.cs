using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QTE : MonoBehaviour
{
    [SerializeField] private float increaseRate = 0.25f;
    [SerializeField] private float decreaseRate = 0.1f;
    private Action[] onComplete;
    private float _progress = 0;
    public float Progress // always between 0 and 1
    {
        get => _progress;
        set => _progress = Mathf.Clamp01(value);
    }

    private bool hasTimeLimit;
    private float timeLimit = 1;
    private float timer;

    [SerializeField] private Slider slider;
    private InputAction interactAction;

    public void Set(float increaseRate, float decreaseRate, Action[] onComplete)
    {
        this.increaseRate = increaseRate;
        this.decreaseRate = decreaseRate;
        this.onComplete = onComplete;
        hasTimeLimit = false;
    }
    public void Set(float increaseRate, float decreaseRate, Action[] onComplete, float timeLimit)
    {
        this.increaseRate = increaseRate;
        this.decreaseRate = decreaseRate;
        this.onComplete = onComplete;
        this.timeLimit = timeLimit;
        hasTimeLimit = true;
    }

    private void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    internal void Complete()
    {
        if (onComplete != null)
            foreach (Action a in onComplete) a();
        Destroy(gameObject);
    }

    internal void Fail()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        Progress -= decreaseRate * Time.deltaTime;
        if (interactAction.WasPressedThisFrame()) Progress += increaseRate;
        if (slider != null) slider.value = Progress;

        if (Progress == 1)
        {
            Complete();
            return;
        }

        if (!hasTimeLimit) return;
        timer += Time.deltaTime;
        if (timer >= timeLimit) Fail();
    }
}
