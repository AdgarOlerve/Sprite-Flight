using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FitToCamera : MonoBehaviour
{
    void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        var cam = Camera.main;
        float h = cam.orthographicSize * 2f;
        float w = h * cam.aspect;
        Vector2 size = sr.sprite.bounds.size;
        float s = Mathf.Max(w / size.x, h / size.y);
        transform.localScale = new Vector3(s, s, 1f);
    }
}