using UnityEngine;
public class BackAndForthRoute: Route
{
    private int _direction = -1;

    public BackAndForthRoute(Enemy enemy) : base(enemy)
    {
    }

    public override GameObject Next
    {
        get
        {
            if (position == Count - 1 || position == 0)
            {
                _direction *= -1;
            }
            position += _direction;
            return Current;
        }
    }
}