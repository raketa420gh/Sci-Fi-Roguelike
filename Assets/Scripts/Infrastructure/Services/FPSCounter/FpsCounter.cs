using UnityEngine;

namespace Raketa420
{
    [RequireComponent(typeof(FpsView))]
    
    public class FpsCounter : MonoBehaviour
    {
        private int frameRange = 60;
        private int[] fpsBuffer;
        private int fpsBufferIndex;
        
        public float AverageFps { get; private set; }

        private void Update()
        {
            if (fpsBuffer == null || frameRange != fpsBuffer.Length)
            {
                InitializeBuffer();
            }
            
            UpdateBuffer();
            CalculateFps();
        }

        private void InitializeBuffer()
        {
            if (frameRange <= 0)
            {
                frameRange = 1;
            }
            fpsBuffer = new int[frameRange];
            fpsBufferIndex = 0;
        }

        private void UpdateBuffer()
        {
            fpsBuffer[fpsBufferIndex++] = (int) (1f / Time.unscaledDeltaTime);

            if (fpsBufferIndex >= frameRange)
            {
                fpsBufferIndex = 0;
            }
        }

        private void CalculateFps()
        {
            var sum = 0;

            for (var i = 0; i < frameRange; i++)
            {
                sum += fpsBuffer[i];
            }

            AverageFps = sum / frameRange;
        }
    }
}
