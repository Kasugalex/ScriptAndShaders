using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class CalculateFrustum{

    public static Matrix4x4 Frustum(Camera _camera)
    {
        float tanHalfFOV = Mathf.Tan(0.5f * _camera.fieldOfView * Mathf.Deg2Rad);
        float halfHeight = tanHalfFOV * _camera.nearClipPlane;
        float halfWidth = halfHeight * _camera.aspect;
        Vector3 toTop = _camera.transform.up * halfHeight;
        Vector3 toRight = _camera.transform.right * halfWidth;
        Vector3 forward = _camera.transform.forward * _camera.nearClipPlane;
        Vector3 toTopLeft = forward + toTop - toRight;
        Vector3 toBottomLeft = forward - toTop - toRight;
        Vector3 toTopRight = forward + toTop + toRight;
        Vector3 toBottomRight = forward - toTop + toRight;

        toTopLeft /= _camera.nearClipPlane;
        toBottomLeft /= _camera.nearClipPlane;
        toTopRight /= _camera.nearClipPlane;
        toBottomRight /= _camera.nearClipPlane;

        Matrix4x4 frustumDir = Matrix4x4.identity;
        frustumDir.SetRow(0, toBottomLeft);
        frustumDir.SetRow(1, toBottomRight);
        frustumDir.SetRow(2, toTopLeft);
        frustumDir.SetRow(3, toTopRight);

        return frustumDir;
    }

}
