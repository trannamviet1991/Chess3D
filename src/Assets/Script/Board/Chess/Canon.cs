using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Canon : Chessman
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
        bool bRet = false;
        bool bFoundFirstObjectOnMyRoad = false;
        int nextX = x;
        int nextY = y;
        int posx = ((int)this.gameObject.transform.position.x);
        int posy = (int)this.gameObject.transform.position.y;

        for (int i = x - 1; i >= BoardDefine.MIN_X; --i)
        {
            nextX = nextX - 1;
            posx = posx - 1;
            if (true == BoardDefine.InBoard(nextX, nextY))
            {
                GameObject outGameObj = null;
                int nSide = m_cBoardInstance.IsExistObjAtPos(nextX, nextY, ref outGameObj);
                if (true == bFoundFirstObjectOnMyRoad)
                {
                    if (BoardDefine.SIDE_UNKNOW != nSide)
                    {
                        if (m_nSide != nSide) AttackToObj(ref outGameObj);
                        break;
                    }
                }
                else
                {
                    // No thing on my road
                    // If next position is empty so I can move here
                    if (BoardDefine.SIDE_UNKNOW == nSide) CreateHideCat(posx, posy, BoardDefine.NEXT_MOVE_COLOR);
                    else bFoundFirstObjectOnMyRoad = true;
                }
            }
        }

        return bRet;
    }

    private bool MoveRight(int x, int y)
    {
        bool bRet = false;
        bool bFoundFirstObjectOnMyRoad = false;
        int nextX = x;
        int nextY = y;
        int posx = ((int)this.gameObject.transform.position.x);
        int posy = (int)this.gameObject.transform.position.y;
        for (int i = x + 1; i <= BoardDefine.MAX_X; i++)
        {
            nextX = nextX + 1;
            posx = posx + 1;
            if (true == BoardDefine.InBoard(nextX, nextY))
            {
                GameObject outGameObj = null;
                int nSide = m_cBoardInstance.IsExistObjAtPos(nextX, nextY, ref outGameObj);
                if (true == bFoundFirstObjectOnMyRoad)
                {
                    if (BoardDefine.SIDE_UNKNOW != nSide)
                    {
                        if (m_nSide != nSide) AttackToObj(ref outGameObj);
                        break;
                    }
                }
                else
                {
                    // No thing on my road
                    // If next position is empty so I can move here
                    if (BoardDefine.SIDE_UNKNOW == nSide) CreateHideCat(posx, posy, BoardDefine.NEXT_MOVE_COLOR);
                    else bFoundFirstObjectOnMyRoad = true;
                }
            }
        }


        return bRet;
    }

    private bool MoveUp(int x, int y)
    {
        bool bRet = false;
        bool bFoundFirstObjectOnMyRoad = false;
        int nextX = x;
        int nextY = y;
        int posx = (int)this.gameObject.transform.position.x;
        int posy = (int)this.gameObject.transform.position.y ;
        for (int i = y + 1; i <= BoardDefine.MAX_Y; i++)
        {
            nextY = nextY + 1;
            posy = posy + 1;
            if (true == BoardDefine.InBoard(nextX, nextY))
            {
                GameObject outGameObj = null;
                int nSide = m_cBoardInstance.IsExistObjAtPos(nextX, nextY, ref outGameObj);
                if (true == bFoundFirstObjectOnMyRoad)
                {
                    if (BoardDefine.SIDE_UNKNOW != nSide)
                    {
                        if (m_nSide != nSide) AttackToObj(ref outGameObj);
                        break;
                    }
                }
                else
                {
                    // No thing on my road
                    // If next position is empty so I can move here
                    if (BoardDefine.SIDE_UNKNOW == nSide) CreateHideCat(posx, posy, BoardDefine.NEXT_MOVE_COLOR);
                    else bFoundFirstObjectOnMyRoad = true;
                }
            }
        }

        return bRet;
    }

    private bool MoveDown(int x, int y)
    {
        bool bRet = false;
        bool bFoundFirstObjectOnMyRoad = false;
        int nextX = x;
        int nextY = y;
        int posx = (int)this.gameObject.transform.position.x;
        int posy = ((int)this.gameObject.transform.position.y);
        for (int i = y - 1; i >= BoardDefine.MIN_Y; --i)
        {
            nextY = nextY - 1;
            posy = posy - 1;
            if (true == BoardDefine.InBoard(nextX, nextY))
            {
                GameObject outGameObj = null;
                int nSide = m_cBoardInstance.IsExistObjAtPos(nextX, nextY, ref outGameObj);
                if (true == bFoundFirstObjectOnMyRoad)
                {
                    if (BoardDefine.SIDE_UNKNOW != nSide)
                    {
                        if (m_nSide != nSide) AttackToObj(ref outGameObj);
                        break;
                    }
                } else {
                    // No thing on my road
                    // If next position is empty so I can move here
                    if (BoardDefine.SIDE_UNKNOW == nSide) CreateHideCat(posx, posy, BoardDefine.NEXT_MOVE_COLOR);
                    else bFoundFirstObjectOnMyRoad = true;
                }
            }
        }

        return bRet;
    }
}
