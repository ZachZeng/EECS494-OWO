using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeController : MonoBehaviour
{
    [HideInInspector] public Shader m_shader;
    private float m_time = 1.0f;
    private Vector4 m_screenResolution;
    private Material m_material;
    [HideInInspector] public float m_value = 0.5f;
    [HideInInspector] public float m_size = 1f;
    [HideInInspector] public float m_duration = 1f;
    [HideInInspector] private float m_timer = 1f;
    [HideInInspector] public float m_speed = 15f;
    [SerializeField] private Shader shader;
    Material material
    {
        get
        {
            if (m_material == null)
            {
                m_material = new Material(m_shader);
                m_material.hideFlags = HideFlags.HideAndDontSave;
            }
            return m_material;
        }
    }
    void Awake()
    {
        m_value = 0;
        m_timer = 0;
        m_shader = shader;
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            Debug.Log("手机不支持后处理...");
            return;
        }

        m_duration = 0.2f;
        m_speed = 30;
        m_size = 1;
    }

    // Start Animation
    void OnEnable()
    {
        m_value = 0;
        m_timer = 0;
    }


    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (m_shader != null)
        {
            m_time += Time.deltaTime;
            if (m_time > 100) m_time = 0;
            material.SetFloat("_TimeX", m_time);
            material.SetFloat("_Value", m_speed);
            material.SetFloat("_Value2", m_size * 0.008f);
            material.SetFloat("_Value3", m_size * 0.008f);
            material.SetVector("_ScreenResolution", new Vector4(sourceTexture.width, sourceTexture.height, 0.0f, 0.0f));
            Graphics.Blit(sourceTexture, destTexture, material);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }
    void Update()
    {

        m_timer += Time.deltaTime * (1 / m_duration);
        if (m_timer > 1.1f) /*Object.Destroy(this);*/
            this.enabled = false;
        Debug.Log("myshaderis"+m_shader);
    }
    void OnDisable()
    {
        if (m_material)
        {
            DestroyImmediate(m_material);
        }
    }
}
