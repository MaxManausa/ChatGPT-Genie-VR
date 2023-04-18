using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public Animator animator;

    public void OnButtonClick()
    {
        animator.SetTrigger("ButtonPressed");
        Invoke("ReturnToIdle", 7.0f);
    }

    private void ReturnToIdle()
    {
        animator.SetTrigger("ButtonPressed");
    }
}
