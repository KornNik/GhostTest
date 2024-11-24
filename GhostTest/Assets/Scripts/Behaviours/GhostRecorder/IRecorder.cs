using UnityEngine;

namespace Behaviours
{
    interface IRecorder
    {
        void StartRecording(Transform recordingTransform);
        void StopRecording();
        void Recording();
        void SaveRecord();
    }
}
