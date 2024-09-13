using UnityEngine;

namespace CubeRainV2
{
	[RequireComponent(typeof(Rigidbody))]
	public class Explosion : MonoBehaviour
	{
		[SerializeField] private float _modifier = 100f;
		[SerializeField] private float _power = 1000f;
		[SerializeField] private float _radius = 5f;
		[SerializeField] private float _upwardsModifier = 3f;

		public void Explode()
		{
			Collider[] colliders = Physics.OverlapSphere(
				transform.position,
				_radius);

			foreach (Collider collider in colliders)
			{
				if (collider.TryGetComponent(out Rigidbody rigidbody))
				{
					rigidbody.AddExplosionForce(
						_power,
						transform.position,
						_radius,
						_upwardsModifier);
				}
			}
		}
	}	
}

