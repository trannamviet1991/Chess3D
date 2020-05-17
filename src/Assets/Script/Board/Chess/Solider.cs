using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Solider : Chessman
{
    private void ShowNextMoveableLocation()
    {
        if (m_cBoardInstance.GetTurn() == m_nSide)
        {
            int x = (int)this.gameObject.transform.localPosition.x;
            int y = (int)this.gameObject.transform.localPosition.y;
            if (BoardDefine.SIDE_A == m_nSide)
            {
                if (true == BoardDefine.SIDE_A_DEF.InMyLand(x, y))
                {
                    MoveUp(x, y);
                }
                else
                {
                    MoveLeft(x, y);
                    MoveRight(x, y);
                    MoveUp(x, y);
                }
            }
            else if (BoardDefine.SIDE_B == m_nSide)
            {
                if (true == BoardDefine.SIDE_B_DEF.InMyLand(x, y))
                {
                    MoveDown(x, y);
                }
                else
                {
                    MoveLeft(x, y);
                    MoveRight(x, y);
                    MoveDown(x, y);
                }
            }
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
        if (true == BoardDefine.InBoard(nextX, nextY))
        {
            bRet = true;
            int posx = ((int)this.gameObject.transform.position.x) - 1;
            int posy = (int)this.gameObject.transform.position.y;

            GameObject outGameObj = null;
            int nSide = m_cBoardInstance.IsExistObjAtPos(nextX, nextY, ref outGameObj);
            if (BoardDefine.SIDE_UNKNOW == nSide) CreateHideCat(posx, posy, BoardDefine.NEXT_MOVE_COLOR);
            else if(m_nSide != nSide) AttackToObj(ref outGameObj);
            else bRet = false;
        }

        return bRet;
    }

    private bool MoveRight(int x, int y)
    {
        int nextX = x + 1;
        int nextY = y;

        bool bRet = false;
        if (true == BoardDefine.InBoard(nextX, nextY))
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
        if (true == BoardDefine.InBoard(nextX, nextY))
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
        if (true == BoardDefine.InBoard(nextX, nextY))
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
