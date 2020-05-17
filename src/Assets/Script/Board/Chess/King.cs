using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class King : Chessman
{  
    private void ShowNextMoveableLocation()
    {
        if (m_cBoardInstance.GetTurn() == m_nSide)
        {
            int x = (int)this.gameObject.transform.localPosition.x;
            int y = (int)this.gameObject.transform.localPosition.y;
            MoveLeft(x, y);
            MoveRight(x, y);
            MoveUp(x, y);
            MoveDown(x, y);
        }
    }

    public override void OnMouseDown()
    {
        if (true == m_IsInAttacked) base.OnMouseDown();
        else ShowNextMoveableLocation();
    }

    private bool MoveLeft(int x, int y)
    {
        int nextX = x - 1;
        int nextY = y;

        bool bRet = false;
        if (BoardDefine.SIDE_A == m_nSide) bRet = BoardDefine.SIDE_A_DEF.King.InMyRange(nextX, nextY);
        else if (BoardDefine.SIDE_B == m_nSide) bRet = BoardDefine.SIDE_B_DEF.King.InMyRange(nextX, nextY);

        if (true == bRet)
        {
            bRet = true;
            int posx = ((int)this.gameObject.transform.position.x) - 1;
            int posy = (int)this.gameObject.transform.position.y;

            GameObject outGameObj = null;
            int nSide = m_cBoardInstance.IsExistObjAtPos(nextX, nextY, ref outGameObj);
            if (BoardDefine.SIDE_UNKNOW == nSide) CreateHideCat(posx, posy, BoardDefine.NEXT_MOVE_COLOR);
            else if (m_nSide != nSide) AttackToObj(ref outGameObj);
            else bRet = false;
        }

        return bRet;
    }

    private bool MoveRight(int x, int y)
    {
        int nextX = x + 1;
        int nextY = y;

        bool bRet = false;
        if (BoardDefine.SIDE_A == m_nSide) bRet = BoardDefine.SIDE_A_DEF.King.InMyRange(nextX, nextY);
        else if (BoardDefine.SIDE_B == m_nSide) bRet = BoardDefine.SIDE_B_DEF.King.InMyRange(nextX, nextY);

        if (true == bRet)
        {
            bRet = true;
            int posx = ((int)this.gameObject.transform.position.x) + 1;
            int posy = (int)this.gameObject.transform.position.y;

            GameObject outGameObj = null;
            int nSide = m_cBoardInstance.IsExistObjAtPos(nextX, nextY, ref outGameObj);
            if (BoardDefine.SIDE_UNKNOW == nSide) CreateHideCat(posx, posy, BoardDefine.NEXT_MOVE_COLOR);
            else if (m_nSide != nSide) AttackToObj(ref outGameObj);
            else bRet = false;
        }

        return bRet;
    }

    private bool MoveUp(int x, int y)
    {
        int nextX = x;
        int nextY = y + 1;

        bool bRet = false;
        if (BoardDefine.SIDE_A == m_nSide) bRet = BoardDefine.SIDE_A_DEF.King.InMyRange(nextX, nextY);
        else if (BoardDefine.SIDE_B == m_nSide) bRet = BoardDefine.SIDE_B_DEF.King.InMyRange(nextX, nextY);

        if (true == bRet)
        {
            bRet = true;
            int posx = (int)this.gameObject.transform.position.x;
            int posy = (int)this.gameObject.transform.position.y + 1;

            GameObject outGameObj = null;
            int nSide = m_cBoardInstance.IsExistObjAtPos(nextX, nextY, ref outGameObj);
            if (BoardDefine.SIDE_UNKNOW == nSide) CreateHideCat(posx, posy, BoardDefine.NEXT_MOVE_COLOR);
            else if (m_nSide != nSide) AttackToObj(ref outGameObj);
            else bRet = false;
        }

        return bRet;
    }

    private bool MoveDown(int x, int y)
    {
        int nextX = x;
        int nextY = y - 1;

        bool bRet = false;
        if (BoardDefine.SIDE_A == m_nSide) bRet = BoardDefine.SIDE_A_DEF.King.InMyRange(nextX, nextY);
        else if (BoardDefine.SIDE_B == m_nSide) bRet = BoardDefine.SIDE_B_DEF.King.InMyRange(nextX, nextY);

        if (true == bRet)
        {
            bRet = true;
            int posx = (int)this.gameObject.transform.position.x;
            int posy = ((int)this.gameObject.transform.position.y) - 1;

            GameObject outGameObj = null;
            int nSide = m_cBoardInstance.IsExistObjAtPos(nextX, nextY, ref outGameObj);
            if (BoardDefine.SIDE_UNKNOW == nSide) CreateHideCat(posx, posy, BoardDefine.NEXT_MOVE_COLOR);
            else if (m_nSide != nSide) AttackToObj(ref outGameObj);
            else bRet = false;
        }

        return bRet;
    }
}
