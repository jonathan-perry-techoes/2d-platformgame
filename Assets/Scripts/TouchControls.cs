using UnityEngine;

public class TouchControls : MonoBehaviour
{
    private PlayerController thePlayer;


    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    public void LeftArrow()
    {
        thePlayer.Move(-1);
    }

    public void RightArrow()
    {        
        thePlayer.Move(1);
    }

    public void UnpressedArrow()
    {
        thePlayer.Move(0);
    }

    public void Sword()
    {
        thePlayer.Sword();
    }

    public void ResetSword()
    {
        thePlayer.ResetSword();
    }

    public void Star()
    {
        thePlayer.FireStar();
    }

    public void Jump()
    {
        thePlayer.Jump();
    }
}
