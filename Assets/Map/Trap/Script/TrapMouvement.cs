using UnityEngine;
using System.Collections;

public class TrapMouvement : MonoBehaviour {

    public enum EMouv
    {
        Rotation,
        Translation
    }

    public enum EDirection
    {
        X,
        Y,
        Z
    }

    public EMouv mouvement;
    public EDirection direction;
    public float value;
    public float timerAtMinAndMax;
    public float speed;
    public float initValue;
    public bool isLooping = true;

    private bool growUp;
    private bool isStatic;

    [HideInInspector]
    public float startTime;
    private Vector3 maxScale;
    private Vector3 minScale;

    private Vector3 lastValue;

    void Start()
    {
        if (mouvement == EMouv.Translation)
        {
            maxScale = this.transform.localPosition;
            minScale = this.transform.localPosition;
        }
        else
        {
            maxScale = this.transform.localEulerAngles;
            minScale = this.transform.localEulerAngles;
        }
        growUp = true;

        if (direction == EDirection.X)
            maxScale.x += value;
        else if (direction == EDirection.Y)
            maxScale.y += value;
        else
            maxScale.z += value;

        startTime = Time.time;
    }

    void FixedUpdate()
    {
        if (initValue > 0)
        {
            initValue -= Time.deltaTime;
            return;
        }
        if (isStatic == true && Time.time > startTime + timerAtMinAndMax)
        {
            startTime = Time.time;
            isStatic = false;
        }
        if (isStatic == false)
        {
            if (growUp)
            {
                if (mouvement == EMouv.Translation)
                {
                    this.transform.localPosition = Vector3.Slerp(minScale, maxScale, (Time.time - startTime) / speed);
                    if ((lastValue == this.transform.localPosition && lastValue != minScale) || maxScale == this.transform.localPosition)
                    {
                        Raz();
                    }
                    lastValue = this.transform.localPosition;
                }
                else
                {
                    this.transform.localEulerAngles = Vector3.Slerp(minScale, maxScale, (Time.time - startTime) / speed);
                    if ((lastValue == this.transform.localEulerAngles && lastValue != minScale) || maxScale == this.transform.localEulerAngles)
                    {
                        Raz();
                    }
                    lastValue = this.transform.localEulerAngles;
                }
            }
            else
            {
                if (mouvement == EMouv.Translation)
                {
                    this.transform.localPosition = Vector3.Slerp(maxScale, minScale, (Time.time - startTime) / speed);
                    if ((lastValue == this.transform.localPosition && lastValue != maxScale) || minScale == this.transform.localPosition)
                    {
                        Raz();
                    }
                    lastValue = this.transform.localPosition;
                }
                else
                {
                    this.transform.localEulerAngles = Vector3.Slerp(maxScale, minScale, (Time.time - startTime) / speed);
                    if ((lastValue == this.transform.localEulerAngles && lastValue != maxScale) || minScale == this.transform.localEulerAngles)
                    {
                        Raz();
                    }
                    lastValue = this.transform.localEulerAngles;
                }
            }
        }
    }

    public void Raz()
    {
        startTime = Time.time;
        isStatic = true;

        if (isLooping)
        {
            growUp = !growUp;
        }
        else
        {
            if (mouvement == EMouv.Translation)
            {
                this.transform.localPosition = minScale;
            }
            else
            {
                this.transform.localEulerAngles = minScale;
            }
        }
    }

    public bool getGrowUp()
    {
        return growUp;
    }
}
