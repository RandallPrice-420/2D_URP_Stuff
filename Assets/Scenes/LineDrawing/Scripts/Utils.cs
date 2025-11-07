using UnityEngine;
using UnityEngine.EventSystems;


public class Utils : MonoBehaviour
{
    // ---------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   Check2DObjectClicked()
    //   IsMouseBlocked()
    //   RestrictValue()
    // ---------------------------------------------------------------------

    #region .  Check2DObjectClicked()  .
    // ---------------------------------------------------------------------
    //   Method.......:  Check2DObjectClicked()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // ---------------------------------------------------------------------
    public static void Check2DObjectClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Mouse is pressed down");
            Camera cam = Camera.main;

            // Raycast depends on camera projection mode.
            Vector2 origin    = Vector2.zero;
            Vector2 direction = Vector2.zero;

            if (cam.orthographic)
            {
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                Ray ray   = Camera.main.ScreenPointToRay(Input.mousePosition);
                origin    = ray.origin;
                direction = ray.direction;
            }

            RaycastHit2D hit = Physics2D.Raycast(origin, direction);

            // Check if we hit anything.
            if (hit)
            {
                print($"We hit {hit.collider.name}");
            }
        }

    }   // Check2DObjectClicked()
    #endregion


    #region .  IsMouseBlocked()  .
    // ---------------------------------------------------------------------
    //   Method.......:  IsMouseBlocked()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // ---------------------------------------------------------------------
    public static bool IsMouseBlocked()
    {
        return (EventSystem.current.IsPointerOverGameObject());

    }   // IsMouseBlocked()
    #endregion


    #region .  RestrictValue()  .
    // ---------------------------------------------------------------------
    //   Method.......:  RestrictValue()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // ---------------------------------------------------------------------
    public static Vector2 RestrictValue(Vector2 value, float minX, float maxX, float minY, float maxY)
    {
        float x = (value.x < minX) ? minX
                : (value.x > maxX) ? maxX
                :  value.x;

        float y = (value.y < minY) ? minY
                : (value.y > maxY) ? maxY
                :  value.y;

        return new Vector2(x, y);

    }   // RestrictValue()
    #endregion


}   // class Utils
