using UnityEngine;

namespace QuestExplosiveCube
{
	public class Player : MonoBehaviour
	{
		private KeyCode _leftMouseButton = KeyCode.Mouse0;
		private Camera _camera;

		private void Awake()
		{
			_camera = Camera.main;
		}

		private void Update()
		{
			if (Input.GetKeyDown(_leftMouseButton))
			{
				OnLeftMouseButtonClick();
			}
		}

		private void OnLeftMouseButtonClick()
		{
			Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
				
			if(Physics.Raycast(ray, out RaycastHit hit))
			{
				if (hit.collider.TryGetComponent(out ExplosiveCube explosiveCube))
				{
					explosiveCube.OnClick();
				}
			}
		}
	}
}
