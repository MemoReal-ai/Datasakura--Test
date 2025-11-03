using UnityEngine;

namespace _Project.Scripts.Service
{
    public interface ICameraService
    {
        Vector3 ClampToViewport(Vector3 worldPosition);
        bool IsOutOfView(Vector3 worldPosition);
        Vector3 ClampDirectionViewPort(Vector3 direction, Vector3 worldPosition);
    }
}