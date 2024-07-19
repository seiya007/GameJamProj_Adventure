using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MathUtil
{
    /// <summary>
    /// 球の内側か
    /// (x - a)^2 + (y - b)^2 + (z - c)^2 <= r^2
    /// </summary>
    /// <param name="p">球の中心座標</param>
    /// <param name="r">半径</param>
    /// <param name="c">対象となる点</param>
    /// <returns></returns>
    public static bool InSphere(Vector3 p, float r, Vector3 c)
    {
        var sum = 0f;
        for (var i = 0; i < 3; i++)
            sum += Mathf.Pow(p[i] - c[i], 2);
        return sum <= Mathf.Pow(r, 2f);
    }
 
    /// <summary>
    /// 円の内側か
    /// (x - a)^2 + (y - b)^2 <= r^2
    /// </summary>
    /// <param name="p">円の中心座標</param>
    /// <param name="r">半径</param>
    /// <param name="c">対象となる点</param>
    /// <returns></returns>
    public bool InCircle(Vector2 p, float r, Vector2 c)
    {
        var sum = 0f;
        for (var i = 0; i < 2; i++)
            sum += Mathf.Pow(p[i] - c[i], 2);
        return sum <= Mathf.Pow(r, 2f);
    }

    public bool InCircleTwoCircle(Vector2 c1, float r1, Vector2 c2, float r2)
    {
        float r = r1 + r2;
        float xDis = c2.x - c1.x;
        float yDis = c2.y - c1.y;
        float distance = xDis * xDis + yDis * yDis;
        bool isHit = (r * r) > distance;
        return isHit;
    }
}