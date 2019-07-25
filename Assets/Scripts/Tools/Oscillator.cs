using UnityEngine;

namespace FlyTheCoop.Tools
{
    // only one of these scripts allowed on GameObject.
    [DisallowMultipleComponent]

    // This script attaches to a GameObject that moves.
    public class Oscillator : MonoBehaviour
    {
#region PublicProperties
        [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
        [SerializeField] float period = 2f;
        float movementFactor; // 0 for not, 1 for fully moved
        Vector3 startingPos;
#endregion
#region Startup
        void Start()
        {
            startingPos = transform.position;
        }
        void Update()
        {
            ProcessOscillation();
        }
        private void ProcessOscillation()
        {
            if (System.Math.Abs(period) > 0)
            {
                float cycles = Time.time / period; // grows continualy from 0
                const float tau = Mathf.PI * 2f; //about 6.28
                float rawSinWave = Mathf.Sin(cycles * tau); //goes from -1 to +1

                movementFactor = rawSinWave / 2f + 0.5f;

                //set movement factor automatically
                Vector3 offset = movementVector * movementFactor;
                transform.position = startingPos + offset;
            }
        }
#endregion
    }
}
