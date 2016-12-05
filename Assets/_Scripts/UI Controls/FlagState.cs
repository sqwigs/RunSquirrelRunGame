using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlagState : MonoBehaviour
{
    public Color activeState;
    public Color cooldownState;
    private Image flagImage;

    void Start ()
    {
        flagImage = this.GetComponent<Image>();
    }

    /// <summary>
    /// Turns the flags active color state on
    /// </summary>
    public void flagActive ()
    {
        flagImage.color = activeState;
    }

    /// <summary>
    /// Turns the flags cooldown color state on
    /// </summary>
    public void flagCooldown ()
    {
        flagImage.color = cooldownState;
    }
}
