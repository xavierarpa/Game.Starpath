
// pixel perfect camera helpers, from old unity 2D tutorial videos
// source: https://www.youtube.com/watch?v=rMCLWt1DuqI
using System;
using UnityEngine;
using X.Common.Change;
using X.Common.Set;
using X.Common.Know;
using X.Common;
using X;

namespace UnityLibrary
{
    [ExecuteInEditMode]
    public class ScaleCamera : MonoBehaviour
    {
        public int targetWidth = 640;
        public float pixelsToUnits = 100;
        Camera cam;

        public SmoothTransition smooth_zoom;
        public float input_scroll_zoom;

        public float speedMovement = default;

        void Start()
        {
            cam = GetComponent<Camera>();
            if (cam == null)
            {
                Debug.LogError("Camera not found..", gameObject);
                this.enabled = false;
                return;
            }

            SetScale();
        }

        // in editor need to update in a loop, in case of game window resizes
        private void Update()
        {
            if (!Application.isPlaying) return;
                    
            transform.position = Vector3.MoveTowards(transform.position, Character._.transform.position.Axis(AxisType.y, transform.position.y), speedMovement * Time.deltaTime);


            input_scroll_zoom = Input.mouseScrollDelta.y * 10;
            if (0 != input_scroll_zoom)
            {
                smooth_zoom.Update_Delta(input_scroll_zoom);
                smooth_zoom.Update_Output(pixelsToUnits);
                pixelsToUnits = smooth_zoom.Output;

                SetScale();
            }
            else
            {
#if UNITY_EDITOR
            SetScale();
#endif
            }
        }

        void SetScale()
        {
            int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);
            cam.orthographicSize = height / pixelsToUnits / 2;
        }
    }
}






[Serializable]
public struct SmoothTransition
{
    #region Variables
    public const float MIN_RANGE = .1f;
    public const float MAX_RANGE = 10f;

    [field: SerializeField]
    public Vector2 Limit { get; private set; }

    [field: SerializeField, Range(MIN_RANGE, MAX_RANGE)]
    public float Intensity_delta { get; private set; }

    [field: SerializeField, Range(MIN_RANGE, MAX_RANGE)]
    public float Intensity_speed { get; private set; }


    [Header("Debug")]

    //[SerializeField]
    [HideInInspector]
    public float delta;

    //[SerializeField]
    [HideInInspector]
    public float deltaSpeed;

    //[SerializeField]
    [HideInInspector]
    public float current;

    //[SerializeField]
    [HideInInspector]
    public float target;

    //[field:SerializeField]
    [HideInInspector]
    public float Output { get; private set; }

    //[Header("Progress")]
    //[Space]
    //[SerializeField, Range(0f, 1f)]
    private float progress_target;

    //[SerializeField, Range(0f, 1f)]
    private float progress_ouput;

    #endregion
    #region Methods
    public void Update_Limit(Vector2 limit) => Limit = limit;
    public void Update_Intensity_Delta(float intensity) => Intensity_delta = intensity;
    public void Update_Intensity_Speed(float intensity) => Intensity_speed = intensity;
    /// <summary>
    /// Actualizamos los datos del movimiento, representa la actualización de la aceleración
    /// </summary>
    public void Update_Delta(float _delta)
    {
        delta = _delta;
        target += delta * Intensity_delta;
        target = target.MinMax(Limit);

        progress_target = ((Limit.x - target) / (Limit.x - Limit.y));
    }
    /// <summary>
    /// Hacemos la actualización de la transición, modificando los datos hasta llegar a la salida
    /// </summary>
    public void Update_Output(float _current)
    {
        current = _current.MinMax(Limit);
        target = target.MinMax(Limit);
        deltaSpeed = (target - current).Positive();

        //Hacer la transición
        Output = Mathf.MoveTowards(
            current,
            target,
            Time.deltaTime * Intensity_speed * deltaSpeed
        );


        progress_ouput = ((Limit.x - Output) / (Limit.x - Limit.y));
    }
    #endregion

}

public static class Utils
{
    public static float MinMax(this float val, Vector2 minMax) => val.Min(minMax.x).Max(minMax.y);
}
