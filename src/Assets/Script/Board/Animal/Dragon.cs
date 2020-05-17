using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : BaseCharacter
{
    public ChessmanIF m_ChessmanIF = null;

    protected override void Awake()
    {
        base.Awake();
        m_ChessmanIF = new CanonImp();
        m_Parent = gameObject.transform.parent;
        if (m_Parent != null)
        {
            m_aim = m_Parent.GetComponent<Animation>();
            m_animator = m_Parent.GetComponent<Animator>();
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        switch (m_State)
        {
            case STATE.ILDE:
                {
                    if (true == GetIsSelect())
                    {
                        m_State = STATE.FLY;
                        if (null != m_animator)
                        {
                            startMarker = m_Parent.transform.position;
                            endMarker = startMarker;
                            endMarker.y += 1.5f;
                            endMarker.z += 3.5f;
                            m_State = STATE.FLY;
                            m_animator.SetBool("fly", true);
                        }
                    }
                    break;
                }
            case STATE.ON_SELECT:
                {
                    break;
                }
            case STATE.FLY:
                {
                    distCovered += BoardDefine.MOVE_SPEED-0.02f;
                    m_Parent.transform.position = Vector3.Lerp(startMarker, endMarker, distCovered);
                    if (distCovered >= 1.0)
                    {
                        m_State = STATE.LAND;
                        m_animator.SetBool("fly", true);
                        startMarker = m_Parent.transform.position;
                        endMarker = startMarker;
                        endMarker.z += 3.5f;
                        endMarker.y = 0;
                        distCovered = 0;
                    }
                    break;
                }
            case STATE.RUN:
                {
                    break;
                }
            case STATE.LAND:
                {
                    distCovered += BoardDefine.MOVE_SPEED - 0.01f;
                    m_Parent.transform.position = Vector3.Lerp(startMarker, endMarker, distCovered);
                    if (distCovered >= 1.0)
                    {
                        m_State = STATE.ILDE;
                        m_animator.SetBool("fly", false);
                        SetIsSelect(false);
                        distCovered = 0;
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
    }
}