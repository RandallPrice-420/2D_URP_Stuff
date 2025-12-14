using UnityEngine;
using DG.Tweening;


public class Basics : MonoBehaviour
{
    [Header("Object")]
    public Transform  blueCube;
    public Transform  greenCube;
    public Transform  purpleCube;
    public Transform  redCube;
    public Transform  whiteCylinder;

    [Header("Move")]
    public Vector3    blueCubeMove;
    public Vector3    greenCubeMove;
    public Vector3    purpleCubeMove;
    public Vector3    redCubeMove;
    public Vector3    whiteCylinderMove;

    [Header("Rotate")]
    public Vector3    blueCubeRotate;
    public Vector3    greenCubeRotate;
    public Vector3    purpleCubeRotate;
    public Vector3    redCubeRotate;
    public Vector3    whiteCylinderRotate;

    [Header("Scale")]
    public Vector3    blueCubeScale;
    public Vector3    greenCubeScale;
    public Vector3    purpleCubeScale;
    public Vector3    redCubeScale;
    public Vector3    whiteCylinderScale;

    [Header("Speed")]
    public float      blueCubeSpeed;
    public float      greenCubeSpeed;
    public float      purpleCubeSpeed;
    public float      redCubeSpeed;
    public float      whiteCylinderSpeed;

    [Header("Description")]
    public TextMesh   whiteCylinderDescription;



    public void ButtonResetClicked()
    {
        DOTween.RestartAll();

    }   // ButtonResetClicked()


    public void ButtonStartClicked()
    {
        DOTween.PlayAll();

    }   // ButtonStartClicked()



    private void Start()
    {
        this.whiteCylinderDescription.text = this.whiteCylinderDescription.text.Replace("%1", this.whiteCylinderRotate.ToString());

        //// Start after one second delay (to ignore Unity hiccups when activating Play mode in Editor)
        //yield return new WaitForSeconds(1);


        // Blue cube - Move (relative), rotate and scale.
        this.blueCube.DOMove        (this.blueCubeMove,        this.blueCubeSpeed     ).SetRelative().Pause();
        this.blueCube.DORotate      (this.blueCubeRotate,      this.blueCubeSpeed     ).SetRelative().Pause();
        this.blueCube.DOScale       (this.blueCubeScale,       this.blueCubeSpeed     ).SetRelative().Pause();

        // Green cube - Move, rotate and scale.
        this.greenCube.DOMove       (this.greenCubeMove,       this.greenCubeSpeed    ).From().Pause();
        this.greenCube.DORotate     (this.greenCubeRotate,     this.greenCubeSpeed    ).SetRelative().Pause();
        this.greenCube.DOScale      (this.greenCubeScale,      this.greenCubeSpeed    ).SetRelative().Pause();

        // Red cube - Move, rotate and scale.
        this.redCube.DOMove         (this.redCubeMove,         this.redCubeSpeed      ).Pause();
        this.redCube.DORotate       (this.redCubeRotate,       this.redCubeSpeed      ).SetRelative().Pause();
        this.redCube.DOScale        (this.redCubeScale,        this.redCubeSpeed      ).SetRelative().Pause();

        // White cylinder - Move, rotate and scale.
        this.whiteCylinder.DOMove   (this.whiteCylinderMove,   this.whiteCylinderSpeed).Pause();
        this.whiteCylinder.DORotate (this.whiteCylinderRotate, this.whiteCylinderSpeed).SetRelative().Pause();
        this.whiteCylinder.DOScale  (this.whiteCylinderScale,  this.whiteCylinderSpeed).SetRelative().Pause();

        // Purole cube - Move (relative), rotate and scale.
        // Change the color to yellow using the material as a target [instead of the transform).
        // Set the color Tween to loop infinitely forward and backwards.
        this.purpleCube.DOMove      (this.purpleCubeMove,      this.purpleCubeSpeed   ).SetRelative().Pause();
        this.purpleCube.DORotate    (this.purpleCubeRotate,    this.purpleCubeSpeed   ).SetRelative().Pause();
        this.purpleCube.DOScale     (this.purpleCubeScale,     this.purpleCubeSpeed   ).SetRelative().Pause();
        this.purpleCube.GetComponent<Renderer>().material
                       .DOColor(Color.yellow, 2)
                       .SetLoops(-1, LoopType.Yoyo)
                       .Pause();

    }   // Start()

}   // class Basics
