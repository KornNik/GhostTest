using Ashsvp;
using UnityEngine;

namespace Behaviours
{
    [RequireComponent(typeof(Collider))]
    sealed class EndRaceTrigger : MonoBehaviour
    {
        [SerializeField] private Collider _endRaceCollider;

        private void Awake()
        {
            if (_endRaceCollider != null)
            {
                _endRaceCollider = GetComponent<Collider>();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponentInParent<SimcadeVehicleController>();
            if(player != null)
            {
                EndRaceEvent.Trigger();
            }
        }
    }
}
