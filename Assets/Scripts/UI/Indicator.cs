using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] Vector3 objectPosition;
    [SerializeField] Color objectColor;

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = objectColor;

        transform.position = transform.position + objectPosition;
    }

    void Update()
    {
        if (GameManager.Instance.MinimapCamera == null)
            return;

        transform.rotation = Quaternion.Euler(90f, 0f, -GameManager.Instance.Player.transform.parent.localEulerAngles.y);
    }
}
