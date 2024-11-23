using UnityEngine;

namespace Behaviours
{
    interface IRecorder
    {
        void StartRecording();
        void StopRecording();
        void Recording(Vector3 position, Quaternion rotation);
        void SaveRecord();
    }
}
