﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Elephant : Chessman
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
        int nextX = x + 2;
        int nextY = y + 2;

        bool bRet = false;
        if (BoardDefine.SIDE_A == m_nSide) bRet = BoardDefine.SIDE_A_DEF.Elephant.InMyLand(nextX, nextY);
        else if (BoardDefine.SIDE_B == m_nSide) bRet = BoardDefine.SIDE_B_DEF.Elephant.InMyLand(nextX, nextY);

        GameObject outGameObj = null;
        if (true == bRet && BoardDefine.SIDE_UNKNOW == m_cBoardInstance.IsExistObjAtPos(x + 1, y + 1, ref outGameObj))
        {
            bRet = true;
            int posx = ((int)this.gameObject.transform.position.x) + 2;
            int posy = (int)this.gameObject.transform.position.y + 2;

            int nSide = m_cBoardInstance.IsExistObjAtPos(nextX, nextY, ref outGameObj);
            if (BoardDefine.SIDE_UNKNOW == nSide) CreateHideCat(posx, posy, BoardDefine.NEXT_MOVE_COLOR);
            else if (m_nSide != nSide) AttackToObj(ref outGameObj);
            else bRet = false;
        }

        return bRet;
    }

    private bool MoveRight(int x, int y)
    {
        int nextX = x - 2;
        int nextY = y - 2;

        bool bRet = false;
        if (BoardDefine.SIDE_A == m_nSide) bRet = BoardDefine.SIDE_A_DEF.Elephant.InMyLand(nextX, nextY);
        else if (BoardDefine.SIDE_B == m_nSide) bRet = BoardDefine.SIDE_B_DEF.Elephant.InMyLand(nextX, nextY);

        GameObject outGameObj = null;
        if (true == bRet && BoardDefine.SIDE_UNKNOW == m_cBoardInstance.IsExistObjAtPos(x - 1, y - 1, ref outGameObj))
        {
            bRet = true;
            int posx = ((int)this.gameObject.transform.position.x) - 2;
            int posy = (int)this.gameObject.transform.position.y - 2;

            int nSide = m_cBoardInstance.IsExistObjAtPos(nextX, nextY, ref outGameObj);
            if (BoardDefine.SIDE_UNKNOW == nSide) CreateHideCat(posx, posy, BoardDefine.NEXT_MOVE_COLOR);
            else if (m_nSide != nSide) AttackToObj(ref outGameObj);
            else bRet = false;
        }

        return bRet;
    }

    private bool MoveUp(int x, int y)
    {
        int nextX = x + 2;
        int nextY = y - 2;

        bool bRet = false;
        if (BoardDefine.SIDE_A == m_nSide) bRet = BoardDefine.SIDE_A_DEF.Elephant.InMyLand(nextX, nextY);
        else if (BoardDefine.SIDE_B == m_nSide) bRet = BoardDefine.SIDE_B_DEF.Elephant.InMyLand(nextX, nextY);

        GameObject outGameObj = null;
        if (true == bRet && BoardDefine.SIDE_UNKNOW == m_cBoardInstance.IsExistObjAtPos(x + 1, y - 1, ref outGameObj))
        {
            bRet = true;
            int posx = (int)this.gameObject.transform.position.x + 2;
            int posy = (int)this.gameObject.transform.position.y - 2;

            int nSide = m_cBoardInstance.IsExistObjAtPos(nextX, nextY, ref outGameObj);
            if (BoardDefine.SIDE_UNKNOW == nSide) CreateHideCat(posx, posy, BoardDefine.NEXT_MOVE_COLOR);
            else if (m_nSide != nSide) AttackToObj(ref outGameObj);
            else bRet = false;
        }

        return bRet;
    }

    private bool MoveDown(int x, int y)
    {
        int nextX = x - 2;
        int nextY = y + 2;

        bool bRet = false;
        if (BoardDefine.SIDE_A == m_nSide) bRet = BoardDefine.SIDE_A_DEF.Elephant.InMyLand(nextX, nextY);
        else if (BoardDefine.SIDE_B == m_nSide) bRet = BoardDefine.SIDE_B_DEF.Elephant.InMyLand(nextX, nextY);

        GameObject outGameObj = null;
        if (true == bRet && BoardDefine.SIDE_UNKNOW == m_cBoardInstance.IsExistObjAtPos(x - 1, y + 1, ref outGameObj))
        {
            bRet = true;
            int posx = (int)this.gameObject.transform.position.x - 2;
            int posy = ((int)this.gameObject.transform.position.y) + 2;

            int nSide = m_cBoardInstance.IsExistObjAtPos(nextX, nextY, ref outGameObj);
            if (BoardDefine.SIDE_UNKNOW == nSide) CreateHideCat(posx, posy, BoardDefine.NEXT_MOVE_COLOR);
            else if (m_nSide != nSide) AttackToObj(ref outGameObj);
            else bRet = false;
        }

        return bRet;
    }
}