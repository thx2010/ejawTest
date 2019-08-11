using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if ((int) Time.timeScale == 0 || !Input.GetMouseButtonDown(0)) return;

        var hitInfo = Physics2D.Raycast(_cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hitInfo && hitInfo.collider.CompareTag("Pipe"))
        {
            hitInfo.collider.gameObject.transform.Rotate(0, 0, 270);
        }
    }
}