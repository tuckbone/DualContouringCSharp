/*
 * This is free and unencumbered software released into the public domain.
 *
 * Anyone is free to copy, modify, publish, use, compile, sell, or
 * distribute this software, either in source code form or as a compiled
 * binary, for any purpose, commercial or non-commercial, and by any
 * means.
 *
 * In jurisdictions that recognize copyright laws, the author or authors
 * of this software dedicate any and all copyright interest in the
 * software to the public domain. We make this dedication for the benefit
 * of the public at large and to the detriment of our heirs and
 * successors. We intend this dedication to be an overt act of
 * relinquishment in perpetuity of all present and future rights to this
 * software under copyright law.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * For more information, please refer to <http://unlicense.org/>
 */

using UnityEngine;

public static class SVD
{
    public static void rotate01(SMat3 vtav, Mat3 v)
    {
        if (vtav.m01 == 0)
        {
            return;
        }

        float c = 0, s = 0;
        Schur2.rot01(vtav, c, s);
        Givens.rot01_post(v, c, s);
    }

    public static void rotate02(SMat3 vtav, Mat3 v)
    {
        if (vtav.m02 == 0)
        {
            return;
        }

        float c = 0, s = 0;
        Schur2.rot02(vtav, c, s);
        Givens.rot02_post(v, c, s);
    }

    public static void rotate12(SMat3 vtav, Mat3 v)
    {
        if (vtav.m12 == 0)
        {
            return;
        }

        float c = 0, s = 0;
        Schur2.rot12(vtav, c, s);
        Givens.rot12_post(v, c, s);
    }

    public static void getSymmetricSvd(SMat3 a, SMat3 vtav, Mat3 v, float tol, int max_sweeps)
    {
        vtav.setSymmetric(a);
        v.set(1, 0, 0, 0, 1, 0, 0, 0, 1);
        float delta = tol * MatUtils.fnorm(vtav);

        for (int i = 0; i < max_sweeps && MatUtils.off(vtav) > delta; ++i)
        {
            rotate01(vtav, v);
            rotate02(vtav, v);
            rotate12(vtav, v);
        }
    }

    public static float calcError(Mat3 A, Vector3 x, Vector3 b)
    {
        Vector3 vtmp;
        MatUtils.vmul(out vtmp, A, x);
        vtmp = b - vtmp;
        return Vector3.Dot(vtmp, vtmp);
    }

    public static float calcError(SMat3 origA, Vector3 x, Vector3 b)
    {
        Mat3 A = new Mat3();
        Vector3 vtmp;
        A.setSymmetric(origA);
        MatUtils.vmul(out vtmp, A, x);
        vtmp = b - vtmp;
        return Vector3.Dot(vtmp, vtmp);
    }

    public static float pinv(float x, float tol)
    {
        return (Mathf.Abs(x) < tol || Mathf.Abs(1 / x) < tol) ? 0 : (1 / x);
    }

    public static void pseudoinverse(out Mat3 Out, SMat3 d, Mat3 v, float tol)
    {
        float d0 = pinv(d.m00, tol), d1 = pinv(d.m11, tol), d2 = pinv(d.m22, tol);

        Out = new Mat3();
        Out.set(v.m00 * d0 * v.m00 + v.m01 * d1 * v.m01 + v.m02 * d2 * v.m02,
                v.m00 * d0 * v.m10 + v.m01 * d1 * v.m11 + v.m02 * d2 * v.m12,
                v.m00 * d0 * v.m20 + v.m01 * d1 * v.m21 + v.m02 * d2 * v.m22,
                v.m10 * d0 * v.m00 + v.m11 * d1 * v.m01 + v.m12 * d2 * v.m02,
                v.m10 * d0 * v.m10 + v.m11 * d1 * v.m11 + v.m12 * d2 * v.m12,
                v.m10 * d0 * v.m20 + v.m11 * d1 * v.m21 + v.m12 * d2 * v.m22,
                v.m20 * d0 * v.m00 + v.m21 * d1 * v.m01 + v.m22 * d2 * v.m02,
                v.m20 * d0 * v.m10 + v.m21 * d1 * v.m11 + v.m22 * d2 * v.m12,
                v.m20 * d0 * v.m20 + v.m21 * d1 * v.m21 + v.m22 * d2 * v.m22);
    }

    public static float solveSymmetric(SMat3 A, Vector3 b, Vector3 x, float svd_tol, int svd_sweeps, float pinv_tol)
    {
        Mat3 pinv;
        Mat3 V = new Mat3();
        SMat3 VTAV = new SMat3();
        getSymmetricSvd(A, VTAV, V, svd_tol, svd_sweeps);
        pseudoinverse(out pinv, VTAV, V, pinv_tol);
        MatUtils.vmul(out x, pinv, b);
        return calcError(A, x, b);
    }

    public static void calcSymmetricGivensCoefficients(float a_pp, float a_pq, float a_qq, float c, float s)
    {
        if (a_pq == 0)
        {
            c = 1;
            s = 0;
            return;
        }

        float tau = (a_qq - a_pp) / (2 * a_pq);
        float stt = Mathf.Sqrt(1.0f + tau * tau);
        float tan = 1.0f / ((tau >= 0) ? (tau + stt) : (tau - stt));
        c = 1.0f / Mathf.Sqrt(1.0f + tan * tan);
        s = tan * c;
    }

    public static float solveLeastSquares(Mat3 a, Vector3 b, Vector3 x, float svd_tol, int svd_sweeps, float pinv_tol)
    {
        Mat3 at;
        SMat3 ata;
        Vector3 atb;
        MatUtils.transpose(out at, a);
        MatUtils.mmul_ata(out ata, a);
        MatUtils.vmul(out atb, at, b);
        return solveSymmetric(ata, atb, x, svd_tol, svd_sweeps, pinv_tol);
    }

}