using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using X.Common;
using X.Common.Know;

public class Mark : MonoBehaviour
{
    public Fingerprint fingerprint = default;
    [Space]
    public SpriteRenderer spr_color = default;
    public LineRenderer line_renderer = default;


    public float radius = default;

    //private static readonly Vector2 range = new Vector2(-1f, 1f);
    private float speedRotation = 0;

    private void Awake()
    {
        speedRotation = 20.MinusMax();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * speedRotation * Time.deltaTime);
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
        spr_color.sprite = GameManager._.list_character_sprites[_fingerprint.SpriteIndex];

        if (_fingerprint.RefID.Length > 0)
        {
            var _id_index = FingerPrintService._.list_id_marks.FindIndex(id => id.Equals(_fingerprint.RefID));
            Mark _mark = null;

            if (_id_index>=0)
            {
                _mark = FingerPrintService._.list_marks[_id_index];
            }

            if (_mark == null)
            {
                // NADA
                line_renderer.startColor = color;
                line_renderer.endColor = color;
                line_renderer.SetPosition(0, new Vector3(this.transform.position.x,-5, this.transform.position.z));
                line_renderer.SetPosition(1, new Vector3(this.transform.position.x, -5, this.transform.position.z));

                Debug.Log($"No Ref {name} => {_fingerprint.RefID}");
            }
            else
            {
                line_renderer.startColor = color;
                line_renderer.endColor = _mark.spr_color.color;
                line_renderer.SetPosition(0, new Vector3(this.transform.position.x, -5, this.transform.position.z));
                line_renderer.SetPosition(1, new Vector3(_mark.transform.position.x, -5, _mark.transform.position.z));
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = spr_color == null ? Color.white : spr_color.color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
