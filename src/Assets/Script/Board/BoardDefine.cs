using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class BoardDefine : MonoBehaviour
{
    public Text TimerA;
    public Text TimerB;
    public const int SIDE_UNKNOW = 0;
    public const int SIDE_A = 1;
    public const int SIDE_B = 2;

    // for setting obj layer 
    public const string SORTING_LAYER_TOP = "top";
    public const string SORTING_LAYER_BG = "background";
    public const string SORTING_LAYER_DEFAULT = "Default";

    // Board range
    public const int MIN_X = 1;
    public const int MAX_X = 9;

    public const int MIN_Y = 1;
    public const int MAX_Y = 10;

    // Hold all game objs
    public GameObject[] m_arrCurSideAObj;
    public GameObject[] m_arrCurSideBObj;

    // timer for 1 turn
    const int MAX_TIMEOUT = 15;
    float timeoutA = MAX_TIMEOUT;
    float timeoutB = MAX_TIMEOUT;

    // for turn
    private int m_nTurn = SIDE_A;
    public int GetTurn() { return m_nTurn; }

    // for hide cat color
    static public Color NEXT_MOVE_COLOR = Color.blue;
    static public Color EAT_COLOR = Color.red;

    static public float SPIN_SPEED = 0;
    public GameObject m_LastClickObj = null;

    public const float MOVE_SPEED = 0.03f;

    public void SetTurn(int nTurn)
    {
        if (SIDE_A == nTurn || SIDE_B == nTurn) m_nTurn = nTurn;
    }

    public void ToogleTurn()
    {
        if (SIDE_A == m_nTurn) m_nTurn = SIDE_B;
        else if (SIDE_B == m_nTurn) m_nTurn = SIDE_A;
    }

    // check if an object is exist at this local pos? Return what side it belongs to
    public int IsExistObjAtLocalPos(int x, int y)
    {
        int nRet = SIDE_UNKNOW;
        // find in A Side
        foreach (GameObject itA in m_arrCurSideAObj)
        {
            if ((int)itA.gameObject.transform.localPosition.x == x && (int)itA.gameObject.transform.localPosition.y == y)
            {
                nRet = SIDE_A;
                break;
            }
        }

        // find in B Side
        if (SIDE_UNKNOW == nRet)
        {
            foreach (GameObject itB in m_arrCurSideBObj)
            {
                if ((int)itB.gameObject.transform.localPosition.x == x && (int)itB.gameObject.transform.localPosition.y == y)
                {
                    nRet = SIDE_B;
                    break;
                }
            }
        }

        return nRet;
    }

    // check if an object is exist at this pos? Return what side it belongs to
    public int IsExistObjAtPos(int x, int y, ref GameObject outGameObj)
    {
        outGameObj = null;
        int nRet = SIDE_UNKNOW;
        // find in A Side
        foreach (GameObject itA in m_arrCurSideAObj)
        {
            if (null == itA) continue;
            if ((int)itA.gameObject.transform.position.x == x && (int)itA.gameObject.transform.position.y == y)
            {
                outGameObj = itA.gameObject;
                nRet = SIDE_A;
                break;
            }
        }

        // find in B Side
        if (SIDE_UNKNOW == nRet)
        {
            foreach (GameObject itB in m_arrCurSideBObj)
            {
                if (null == itB) continue;
                if ((int)itB.gameObject.transform.position.x == x && (int)itB.gameObject.transform.position.y == y)
                {
                    nRet = SIDE_B;
                    outGameObj = itB.gameObject;
                    break;
                }
            }
        }

        return nRet;
    }

    static public bool InBoard(int x, int y)
    {
        bool bRet = false;
        if (MIN_X <= x && x <= MAX_X)
        {
            if (MIN_Y <= y && y <= MAX_Y)
            {
                bRet = true;
            }
        }

        return bRet;
    }

    private void Update()
    {
        if (timeoutA < 0) timeoutA = MAX_TIMEOUT;
        if (timeoutB < 0) timeoutB = MAX_TIMEOUT;

        if (SIDE_A == m_nTurn)
        {
            timeoutA -= Time.deltaTime;
            timeoutB = MAX_TIMEOUT;
            TimerB.gameObject.SetActive(false);
            TimerA.gameObject.SetActive(true);
            TimerA.text = "Remain: " + ((int)timeoutA).ToString();
        }
        else
        {
            timeoutB -= Time.deltaTime;
            timeoutA = MAX_TIMEOUT;
            TimerA.gameObject.SetActive(false);
            TimerB.gameObject.SetActive(true);
            TimerB.text = "Remain: " + ((int)timeoutB).ToString();
        }
    }

    //
    // SIDE A DEFINE
    //
    public class SIDE_A_DEF
    {
        public const string SIDE_TAG = "sideA";
        static public Color MY_COLOR = Color.yellow;

        public const int RIVER_Y = 5;
        public const int MIN_X = BoardDefine.MIN_X;
        public const int MAX_X = BoardDefine.MAX_X;

        public const int MIN_Y = BoardDefine.MIN_Y;
        public const int MAX_Y = RIVER_Y;

        static public bool InMyLand(int x, int y)
        {
            bool bRet = false;
            if (MIN_X <= x && x <= MAX_X)
            {
                if (MIN_X <= y && y <= RIVER_Y)
                {
                    bRet = true;
                }
            }

            return bRet;
        }

        public class King
        {
            public const int MIN_X = 4;
            public const int MAX_X = 6;

            public const int MIN_Y = 1;
            public const int MAX_Y = 3;

            static public bool InMyRange(int x, int y)
            {
                bool bRet = false;
                if (MIN_X <= x && x <= MAX_X)
                {
                    if (MIN_Y <= y && y <= MAX_Y)
                    {
                        bRet = true;
                    }
                }

                return bRet;
            }
        }

        public class Guarder
        {
            static public bool InMyRange(int x, int y)
            {
                return SIDE_A_DEF.King.InMyRange(x, y);
            }
        }

        public class Elephant
        {
            static public bool InMyLand(int x, int y)
            {
                return SIDE_A_DEF.InMyLand(x, y);
            }
        }
    }

    //
    // SIDE B
    //
    public class SIDE_B_DEF
    {
        public const string SIDE_TAG = "sideB";
        static public Color MY_COLOR = Color.cyan;

        // define B range
        public const int RIVER_Y = 6;
        public const int MIN_X = BoardDefine.MIN_X;
        public const int MAX_X = BoardDefine.MAX_X;

        public const int MIN_Y = RIVER_Y;
        public const int MAX_Y = BoardDefine.MAX_Y;

        static public bool InMyLand(int x, int y)
        {
            bool bRet = false;
            if (MIN_X <= x && x <= MAX_X)
            {
                if (RIVER_Y <= y && y <= MAX_Y)
                {
                    bRet = true;
                }
            }

            return bRet;
        }

        //
        // SIDE B KING
        //
        public class King
        {
            public const int MIN_X = 4;
            public const int MIN_Y = 8;

            public const int MAX_X = 6;
            public const int MAX_Y = 10;

            // Not cross over the river
            static public bool InMyRange(int x, int y)
            {
                bool bRet = false;
                if (MIN_X <= x && x <= MAX_X)
                {
                    if (MIN_Y <= y && y <= MAX_Y)
                    {
                        bRet = true;
                    }
                }

                return bRet;
            }
        }

        //
        // SIDE B Guarder
        //
        public class Guarder
        {
            // Can not go out this range
            static public bool InMyRange(int x, int y)
            {
                return SIDE_B_DEF.King.InMyRange(x, y);
            }
        }

        //
        // SIDE B Elephant
        //
        public class Elephant
        {
            // Can not go out this range
            static public bool InMyLand(int x, int y)
            {
                return SIDE_B_DEF.InMyLand(x, y);
            }
        }
    }
}
