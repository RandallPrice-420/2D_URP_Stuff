using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UGUI : MonoBehaviour
{
    public Image  circleOutline;
    public Image  dotweenLogo;
    public Text   relativeText;
    public Text   scrambledText;
    public Text   regularText;
    public Slider slider;


    public void RestartTweens()
    {
        DOTween.RestartAll();

    }   // RestartTweens()


    public void StartTweens()
    {
        DOTween.PlayAll();

    }   // StartTweens()



    private Color RandomColor()
    {
        return new Color(Random.Range(0f, 1f),
                         Random.Range(0f, 1f),
                         Random.Range(0f, 1f), 1);

    }    // RandomColor()


    private void Start()
    {
        // All Tweens are created in a paused state by chaining to them a final Pause()
        // so that the PLAY button can activate them when pressed.  The ones that don't
        // loop infinitely have AutoKill property set to FALSE so they aren't destroyed
        // when complete and can be resued by the RESTART button.

        // Animate the fade out of DOTween's logo.
        this.dotweenLogo  .DOFade(0, 1.5f)
                          .SetAutoKill(false)
                          .Pause()
                          ;

        // Animate the circle outline's color and fill amount.
        this.circleOutline.DOColor(RandomColor(), 1.5f)
                          .SetEase(Ease.Linear)
                          .Pause()
                          ;

        this.circleOutline.DOFillAmount(0, 1.5f)
                          .SetEase(Ease.Linear)
                          .SetLoops(-1, LoopType.Yoyo)
                          .OnStepComplete(()=>
                          {
                              this.circleOutline.fillClockwise = !this.circleOutline.fillClockwise;
                              this.circleOutline.DOColor(RandomColor(), 1.5f)
                                                .SetEase(Ease.Linear);
                          })
                          .Pause();

        // Animate the regular text.
        this.regularText  .DOText("This text will replace the existing one", 2)
                          .SetAutoKill(false)
                          .SetEase(Ease.Linear)
                          .Pause()
                          ;

        // Animate the relative text.
        this.relativeText .DOText(" - This text will be added to the existing one", 2)
                          .SetAutoKill(false)
                          .SetEase(Ease.Linear)
                          .SetRelative()
                          .Pause()
                          ;

        // Animate the scrambled text.
        this.scrambledText.DOText("This text will appear from scrambled chars", 2, true, ScrambleMode.All)
                          .SetAutoKill(false)
                          .SetEase(Ease.Linear)
                          .Pause()
                          ;

        // Animate the slider.
        this.slider       .DOValue(1, 1.5f)
                          .SetEase(Ease.InOutQuad)
                          .SetLoops(-1, LoopType.Yoyo)
                          .Pause()
                          ;

    }   // Start()


}   // class UGUI
