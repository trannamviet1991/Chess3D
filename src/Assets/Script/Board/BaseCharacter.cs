using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public GameObject m_CameraGameObj;
    private SmoothFollow m_SmoothFollow;
    private bool m_MySide;
    private Renderer _renderer = null;
    private SkinnedMeshRenderer m_SkinnedMeshRenderer = null;
    private MeshRenderer m_MeshRenderer = null;
    private Material[] m_arrDefaultMaterial;
    private Material m_DefaultMaterial;

    private AudioSource[] m_arrAudioSrc;
    private AudioSource m_ClickAudio = null;
    private AudioSource m_AttackAudio = null;

    private static AudioSource m_CurPlayingAudio = null;
    protected static GameObject m_PreviousCLickedGameObj = null;
    private Color m_MyCol;

    protected readonly float speed = 1f;
    private GameObject m_SideObj = null;

    // Transforms to act as start and end markers for the journey.
    public Vector3 startMarker;
    public Vector3 endMarker;

    private bool m_bIsSelect = false;
    public bool GetIsSelect() { return m_bIsSelect; }
    public void SetIsSelect(bool b) { m_bIsSelect = b; }

    protected Transform m_Parent;
    protected Animation m_aim;
    protected Animator m_animator;
    protected float distCovered = 0;

    public enum STATE
    {
        ILDE=0,
        ON_SELECT,
        FLY,
        RUN,
        LAND,
    }
    protected STATE m_State = STATE.ILDE;
    public STATE GetState() { return m_State; }
    public void SetState(STATE state) { m_State = state; }

    protected virtual void Start()
    {
        m_CameraGameObj = GameObject.FindGameObjectWithTag("Radar");
        if (m_CameraGameObj != null) m_SmoothFollow = m_CameraGameObj.GetComponent<SmoothFollow>();
    }

    protected virtual void Awake()
    {
        LoadMyColor();

        LoadMaterial();
        SetMyColor();
        m_arrAudioSrc = GetComponents<AudioSource>();
        if (null != m_arrAudioSrc)
        {
            if (m_arrAudioSrc.Length > 0) m_ClickAudio = m_arrAudioSrc[0];
            if (m_arrAudioSrc.Length > 1) m_AttackAudio = m_arrAudioSrc[1];
        }
    }

    protected virtual void Update()
    {
    }

    public void ResetPreviousObj()
    {
        if (null != m_PreviousCLickedGameObj)
        {
            BaseCharacter previousObj = m_PreviousCLickedGameObj.GetComponent<BaseCharacter>();
            previousObj.SetMyColor();
            if (null != previousObj.m_animator)
            {
                previousObj.m_animator.SetBool("run", false);
                previousObj.m_animator.SetBool("fly", false);
            }
            previousObj.SetIsSelect(false);
            previousObj.SetState(STATE.ILDE);
        }
    }

    protected virtual void OnMouseDown()
    {
        if (null != m_SmoothFollow)
        {
            m_SmoothFollow.m_Side = m_MySide;
            m_SmoothFollow.target = this.gameObject.transform.parent;
        }

        ResetPreviousObj();
        SetIsSelect(true);

        // play on click audio
        if (null != m_CurPlayingAudio) m_CurPlayingAudio.Stop();
        m_CurPlayingAudio = m_ClickAudio;
        m_CurPlayingAudio.Play();

        // change color of current clicking obj
        ColorUtility.TryParseHtmlString("#5A84E0", out Color newCol);

        m_DefaultMaterial.color = newCol;
        if (null != m_arrDefaultMaterial)
        {
            if (m_arrDefaultMaterial.Length > 0)
            {
                foreach (Material item in m_arrDefaultMaterial)
                {
                    item.color = newCol;
                }
            }
        }

        m_PreviousCLickedGameObj = this.gameObject;
    }

    //
    // my functions
    //
    public void SetMyColor()
    {
        m_DefaultMaterial.color = m_MyCol;
        if (null != m_arrDefaultMaterial)
        {
            if (m_arrDefaultMaterial.Length > 0)
            {
                foreach (Material item in m_arrDefaultMaterial)
                {
                    item.color = m_MyCol;
                }
            }
        }
    }

    private void LoadMaterial()
    {
        _renderer = GetComponent<Renderer>();
        if (null == _renderer)
        {
            m_SkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            if (null != m_SkinnedMeshRenderer)
            {
                m_arrDefaultMaterial = m_SkinnedMeshRenderer.materials;
            }
            else
            {
                m_MeshRenderer = GetComponent<MeshRenderer>();
                if (null != m_MeshRenderer) m_arrDefaultMaterial = m_MeshRenderer.materials;
                //else m_arrDefaultMaterial = this.gameObject.GetComponents<Material>();
            }
        }
        else m_arrDefaultMaterial = _renderer.materials;
        m_DefaultMaterial = _renderer.material;
    }

    private void FindRootSideObj()
    {
        Transform curParentTransform = this.transform.parent;
        if (null != curParentTransform) {
            GameObject curParentGameObj = curParentTransform.gameObject;
            while (true != curParentGameObj.CompareTag("SideA") && true != curParentGameObj.CompareTag("SideB"))
            {
                curParentTransform = curParentGameObj.transform.parent;
                if (null != curParentTransform)
                {
                    curParentGameObj = curParentTransform.gameObject;
                }
                else break;
            }

            m_SideObj = curParentGameObj;
        }
    }

    private void LoadMyColor()
    {
        FindRootSideObj();
        if (null != m_SideObj)
        {
            if (true == m_SideObj.CompareTag("SideA"))
            {
                ColorUtility.TryParseHtmlString("#D2D2D2", out m_MyCol);
                m_MySide = true;
            }
            else
            {
                ColorUtility.TryParseHtmlString("#C66353", out m_MyCol);
                m_MySide = false;
            }
        }
        else
        {
            if (true == this.gameObject.CompareTag("SideA"))
            {
                ColorUtility.TryParseHtmlString("#D2D2D2", out m_MyCol);
                m_MySide = true;
            }
            else
            {
                ColorUtility.TryParseHtmlString("#C66353", out m_MyCol);
                m_MySide = false;
            }
        }
    }
}