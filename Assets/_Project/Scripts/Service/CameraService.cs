using UnityEngine;

namespace _Project.Scripts.Service
{
    public class CameraService : ICameraService
    {
        private readonly Camera _camera;

        public Camera Camera => _camera;

        public CameraService(Camera camera)
        {
            _camera = camera;
        }

        public Vector3 ClampDirectionViewPort(Vector3 direction, Vector3 worldPosition)
        {
            Vector3 futurePosition = worldPosition + direction;
            Vector3 viewPortPoint = _camera.WorldToViewportPoint(futurePosition);

            Vector3 newDirection = direction;
            bool isChanged = false;


            if (viewPortPoint.x < 0f)
            {
                newDirection.x = Mathf.Abs(direction.x);
                isChanged = true;
            }
            else if (viewPortPoint.x > 1f)
            {
                newDirection.x = -Mathf.Abs(direction.x);
                isChanged = true;
            }


            if (viewPortPoint.y < 0f)
            {
                newDirection.z = Mathf.Abs(direction.z);
                isChanged = true;
            }
            else if (viewPortPoint.y > 1f)
            {
                newDirection.z = -Mathf.Abs(direction.z);
                isChanged = true;
            }

            return isChanged ? newDirection.normalized : direction.normalized;
        }

        public bool IsOutOfView(Vector3 worldPosition)
        {
            Vector3 viewPos = _camera.WorldToViewportPoint(worldPosition);
            return viewPos.x < 0f || viewPos.x > 1f || viewPos.y < 0f || viewPos.y > 1f;
        }

        public Vector3 ClampToViewport(Vector3 worldPosition)
        {
            Vector3 viewPos = _camera.WorldToViewportPoint(worldPosition);
            viewPos.x = Mathf.Clamp01(viewPos.x);
            viewPos.y = Mathf.Clamp01(viewPos.y);
            return _camera.ViewportToWorldPoint(viewPos);
        }
    }
}