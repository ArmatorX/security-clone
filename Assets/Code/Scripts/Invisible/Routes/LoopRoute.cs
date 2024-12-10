using UnityEngine;
public class LoopRoute : Route
{
    public LoopRoute(Enemy enemy) : base(enemy)
    {
    }

    public override GameObject Next
    {
        get
        {
            if (position == Count - 1)
            {
                position = 0;
            }
            else
            {
                position++;
            }
            return Current;
        }
    }
}