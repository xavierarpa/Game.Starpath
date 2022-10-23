using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    public Fingerprint fingerprint = default;
    [Space]
    public SpriteRenderer spr_color = default;
    public GameObject obj_message = default;
    public TMPro.TMP_Text text_message = default;
    public TMPro.TMP_Text text_nickname = default;
    public TMPro.TMP_Text text_createdAt = default;
    public LineRenderer line_renderer = default;


    public float radius = default;

    private void Awake()
    {
        obj_message.SetActive(false);
    }

    public void SetFingerprint( Fingerprint _fingerprint)
    {
        this.fingerprint = _fingerprint;

        ColorUtility.TryParseHtmlString(_fingerprint.Color, out Color color);


        // Position
        transform.position = new Vector3(
            float.TryParse(fingerprint.Position_X.ToString(), out float _X) ? _X : float.MaxValue,
            -1,
            float.TryParse(fingerprint.Position_Y.ToString(), out float _Y) ? _Y : float.MaxValue
        );

#if UNITY_EDITOR
        name = fingerprint.Nick;
#endif
        spr_color.color = color;

        if (_fingerprint.RefID.Length > 0)
        {
            var _mark = FingerPrintService._.list_marks.Find(m => m.fingerprint.RefID.Equals(_fingerprint.RefID));
            if (_mark.Equals(null))
            {
                // NADA
                line_renderer.startColor = color;
                line_renderer.endColor = color;
                line_renderer.SetPosition(0, this.transform.position + Vector3.down);
                line_renderer.SetPosition(1, this.transform.position + Vector3.down);
            }
            else
            {
                line_renderer.startColor = color;
                line_renderer.endColor = _mark.spr_color.color;
                line_renderer.SetPosition(0, this.transform.position + Vector3.down);
                line_renderer.SetPosition(1, _mark.transform.position + Vector3.down);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = spr_color == null ? Color.white : spr_color.color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
