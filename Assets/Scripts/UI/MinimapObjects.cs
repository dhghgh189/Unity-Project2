using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapObjects : MonoBehaviour
{
    [SerializeField] GameObject minimapObjectPrefab;
    [SerializeField] Vector3 objectPosition;
    [SerializeField] Color objectColor;

    GameObject _minimapObject;

    private void Awake()
    {
        _minimapObject = Instantiate(minimapObjectPrefab, gameObject.transform);

        SpriteRenderer sr = _minimapObject.GetComponent<SpriteRenderer>();
        sr.color = objectColor;
    }

    void Update()
    {
        if (GameManager.Instance.MinimapCamera == null)
            return;

        _minimapObject.transform.LookAt(GameManager.Instance.MinimapCamera.transform);
        _minimapObject.transform.position = gameObject.transform.position + objectPosition;
    }
}
