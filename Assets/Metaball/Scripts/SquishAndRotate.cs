using UnityEngine;

public class SquishAndRotate : MonoBehaviour
{
    #region variables
    public float rotationSpeedAngle = 45f;
    public float minSquishX = 0.5f, maxSquishX = 1f;
    public float minSquishY = 0.5f, maxSquishY = 1f;
    public Vector2 movementSpeedRange = new Vector2(0.1f, 0.2f);

    float differenceX;
    float differenceY;
    float startAngle = 0;
    #endregion
    #region initialization
    void Start()
    {
        startAngle = Random.Range(0, 180);

        differenceY = (maxSquishY - minSquishY) * 0.5f;
        differenceX = (maxSquishX - minSquishX) * 0.5f;

    }
    #endregion
    #region logic
    void Update()
    {
        transform.localScale = new Vector3(
            minSquishX + differenceX + Mathf.Cos(startAngle + Time.time) * differenceX,
            minSquishY + differenceY + Mathf.Sin(startAngle + Time.time) * differenceY,
            transform.localScale.z);



        transform.rotation *= Quaternion.AngleAxis(rotationSpeedAngle * Time.deltaTime, Vector3.forward);
    }
    #endregion
    #region public interface
    #endregion
    #region private interface
    #endregion
    #region events
    #endregion
}
