using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadSlider : MonoBehaviour
{
    [SerializeField] Transform target;
    private Slider reloadSlider;
    private float reloadTime;

    private void Awake()
    {
        reloadSlider = GetComponent<Slider>();
    }
    private void OnEnable()
    {
        reloadTime = Time.time + reloadSlider.maxValue;
        reloadSlider.value = 0f;
    }
    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            SetPosition();
            FillSlider();
        }
    }
    private void SetPosition()
    {
        Vector2 position = Camera.main.WorldToScreenPoint(target.position);
        transform.position = position;
        transform.rotation = target.rotation;
    }
    private void FillSlider()
    {
        if (Time.time < reloadTime)
        {
            reloadSlider.value += Time.deltaTime;
        }
        else reloadSlider.gameObject.SetActive(false);
    }
}
