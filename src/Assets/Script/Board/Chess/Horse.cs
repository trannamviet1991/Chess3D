using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Horse : Chessman
{
    private void ShowNextMoveableLocation()
    {
        if (m_cBoardInstance.GetTurn() == m_nSide)
        {
        }
    }

    public override void OnMouseDown()
    {
        if (true == m_IsInAttacked) base.OnMouseDown();
        else ShowNextMoveableLocation();
    }
}
