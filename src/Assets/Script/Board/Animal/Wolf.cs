using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : BaseCharacter
{
    public ChessmanIF m_ChessmanIF = null;
    protected override void Awake()
    {
        base.Awake();
        m_ChessmanIF = new HorseImp();
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
                        if (null != m_animator)
                        {
                            startMarker = m_Parent.transform.position;
                            endMarker = startMarker;
                            endMarker.z += 5.5f;
                            m_State = STATE.RUN;
                            m_animator.SetBool("run", true);
                        }
                    }
                    break;
                }
            case STATE.ON_SELECT:
                {
                    break;
                }
            case STATE.RUN:
                {
                    distCovered += BoardDefine.MOVE_SPEED-0.02f;
                    m_Parent.transform.position = Vector3.Lerp(startMarker, endMarker, distCovered);
                    if (distCovered >= 1.0)
                    {
                        m_State = STATE.ILDE;
                        m_animator.SetBool("run", false);
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
