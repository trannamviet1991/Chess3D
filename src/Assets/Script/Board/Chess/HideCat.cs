using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// HideCat: Object on the road
// Will be eaten or will be delete
public class HideCat : MonoBehaviour
{
    public GameObject m_BoardGameObj;
    private BoardDefine m_cBoardInstance = null;
    private ArrayList m_arrNextObj;

    void Start()
    {
        if (null != m_BoardGameObj) m_cBoardInstance = m_BoardGameObj.GetComponent<BoardDefine>();
        else Assert.IsNull(m_BoardGameObj);
    }

    private void Update()
    {
        this.transform.Rotate(Vector3.up * BoardDefine.SPIN_SPEED * Time.deltaTime);
    }

    private void Move()
    {
        // get shadow's parent
        GameObject cParent = this.transform.parent.gameObject;
        Chessman chessman = cParent.GetComponent<Chessman>();

        // set parent position = shadow position
        chessman.ToggleTurn();

        // move chessman to shadows
        chessman.UpdateNewPosition((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y);

        // delete all shadows
        chessman.DeleteAllShadows();

        // Remove attack flag
        chessman.DeleteCanEatObj();
    }

    // if click on shadow, move to here
    private void OnMouseDown()
    {
        Move();
    }

    public void SetShadowsObj(ArrayList arrObj)
    {
        m_arrNextObj = arrObj;
    }
}
