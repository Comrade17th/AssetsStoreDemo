using UnityEngine;

namespace QuestExplosiveCubeV2
{
	[RequireComponent(typeof(Rigidbody))]
	public class Explosion : MonoBehaviour
	{
		[SerializeField] private float _modifier = 100f;
		[SerializeField] private float _power = 10f;
		[SerializeField] private float _radius = 5f;
		[SerializeField] private float _upwardsModifier = 3f;

		public void ExplodeAround()
		{
			float radius = _radius / transform.localScale.x;
			float power = _power * _modifier / transform.localScale.x;
            
			Collider[] colliders = Physics.OverlapSphere(
				transform.position,
				radius);
            
			foreach (Collider hit in colliders)
			{
				Rigidbody rigidbody = hit.GetComponent<Rigidbody>();

				if (rigidbody != null)
					rigidbody.AddExplosionForce(power,
						transform.position,
						radius,
						_upwardsModifier);
			}
		}

		public void ExplodeGroup(ExplosiveCube[] cubes)
		{
			foreach (ExplosiveCube cube in cubes)
			{
				Rigidbody rigidbody = cube.GetComponent<Rigidbody>();
				rigidbody.AddExplosionForce(
					_power,
					transform.position,
					_radius,
					_upwardsModifier);
			}
		}
	}	
}

