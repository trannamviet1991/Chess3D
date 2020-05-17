using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side : MonoBehaviour
{
    private float distCovered = 0f;
    enum STATE
    {
        INIT = 0,
        ILDE = 1,
    }
    STATE m_State = STATE.INIT;
    // Transforms to act as start and end markers for the journey.
    public Vector3 startMarker;
    public Vector3 endMarker;

    protected virtual void Awake()
    {
        startMarker = this.gameObject.transform.position;
        endMarker = new Vector3(startMarker.x, startMarker.y - 4, startMarker.z);
    }

    protected virtual void Update()
    {
        switch (m_State)
        {
            case STATE.INIT:
                {
                    distCovered += BoardDefine.MOVE_SPEED*2;
                    transform.position = Vector3.Lerp(endMarker, startMarker, distCovered);
                    if (distCovered >= 1.0)
                    {
                        m_State = STATE.ILDE;
                    }
                    break;
                }
            case STATE.ILDE:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
