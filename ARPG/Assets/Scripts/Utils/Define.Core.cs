using UnityEngine;

public partial class Define
{
    public enum EPOPUP_TYPE
    {
        None,
    }

    public enum ESCENE_TYPE
    {
        Start,
        Game
    }

    public enum ELOAD_TYPE
    {
        None,
        Fade,
        Loading
    }

    public enum ECANVAS_TYPE
    {
        Master = 0,
        Dialogue = 1,
        Popup = 2,
        Floating = 3,
        Tutorial = 4,
    }
}
