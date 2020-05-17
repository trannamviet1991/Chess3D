using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Chessman : MonoBehaviour
{
    public GameObject m_HideCatObj;
    public GameObject m_BoardGameObj;

    protected ArrayList m_arrNextObj;
    protected ArrayList m_arrCanEatObj;
    protected int m_nSide = BoardDefine.SIDE_UNKNOW;

    protected BoardDefine m_cBoardInstance = null;
    protected bool m_IsInAttacked = false;

    public GameObject m_WhoAttackedMe = null;

    public Chessman()
    {
        m_arrNextObj = new ArrayList();
        m_arrCanEatObj = new ArrayList();
    }

    ~Chessman()
    {
        m_arrNextObj.Clear();
        m_arrCanEatObj.Clear();
    }

    private void Update()
    {
        if (true == m_IsInAttacked) this.transform.Rotate(Vector3.up * BoardDefine.SPIN_SPEED * Time.deltaTime);
    }

    void Start()
    {
        // Get side and set my color
        if (this.gameObject.CompareTag(BoardDefine.SIDE_A_DEF.SIDE_TAG))
        {
            m_nSide = BoardDefine.SIDE_A;
            this.GetComponent<SpriteRenderer>().color = BoardDefine.SIDE_A_DEF.MY_COLOR;
        }
        else if (this.gameObject.CompareTag(BoardDefine.SIDE_B_DEF.SIDE_TAG))
        {
            m_nSide = BoardDefine.SIDE_B;
            this.GetComponent<SpriteRenderer>().color = BoardDefine.SIDE_B_DEF.MY_COLOR;
        }

        // get board rule
        if (null != m_BoardGameObj) m_cBoardInstance = m_BoardGameObj.GetComponent<BoardDefine>();
        else Assert.IsNull(m_BoardGameObj);
    }

    // create shadows object for next moving
    protected void CreateHideCat(int posx, int posy, Color color)
    {
        GameObject newObj = Instantiate(m_HideCatObj);
        newObj.transform.SetParent(this.transform, true);
        SpriteRenderer sprite = newObj.GetComponent<SpriteRenderer>();
        sprite.color = color;
        if (null != sprite) sprite.sortingLayerName = BoardDefine.SORTING_LAYER_TOP;

        HideCat hideCat = newObj.GetComponent<HideCat>();
        hideCat.SetShadowsObj(m_arrNextObj);

        newObj.transform.position = new Vector3(posx, posy, 0);
        m_arrNextObj.Add(newObj);
    }

    // put gameobj under attack
    protected void AttackToObj(ref GameObject gameobj)
    {
        if (null != gameobj)
        {
            gameobj.GetComponent<SpriteRenderer>().color = BoardDefine.EAT_COLOR;
            gameobj.GetComponent<Chessman>().m_IsInAttacked = true;
            gameobj.GetComponent<Chessman>().m_WhoAttackedMe = this.gameObject;
            m_arrCanEatObj.Add(gameobj);
        }
    }

    public void ToggleTurn()
    {
        if (null != m_cBoardInstance) m_cBoardInstance.ToogleTurn();
    }

    public void DeleteAllShadows()
    {
        foreach (GameObject child in m_arrNextObj)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    public void DeleteCanEatObj()
    {
        foreach (GameObject child in m_arrCanEatObj)
        {
            if (child.GetComponent<Chessman>().m_nSide == BoardDefine.SIDE_A) child.GetComponent<SpriteRenderer>().color = BoardDefine.SIDE_A_DEF.MY_COLOR;
            else child.GetComponent<SpriteRenderer>().color = BoardDefine.SIDE_B_DEF.MY_COLOR;
            child.GetComponent<Chessman>().m_IsInAttacked = false;
        }
    }

    public void UpdateNewPosition(int x, int y)
    {
        this.transform.position = new Vector3(x, y, 0);
    }

    public virtual void OnMouseDown()
    {
        // if im in attacked, find who attacked me, replace me by him
        if (true == m_IsInAttacked)
        {
            GameObject attacker = this.gameObject.GetComponent<Chessman>().m_WhoAttackedMe; // set by AttackToObj
            Chessman chessman = attacker.GetComponent<Chessman>();
            chessman.UpdateNewPosition((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y);
            chessman.ToggleTurn();
            chessman.DeleteAllShadows();
            m_IsInAttacked = false;

            // delete myself
            DestroyImmediate(this.gameObject);
        }

        m_cBoardInstance.m_LastClickObj = this.gameObject;
    }
}
