using UnityEngine;

public static class MatUtils
{
    public static float fnorm(Mat3 a) 
    { 
        return Mathf.Sqrt((a.m00 * a.m00) + (a.m01 * a.m01) + (a.m02 * a.m02)
                    + (a.m10 * a.m10) + (a.m11 * a.m11) + (a.m12 * a.m12)
                    + (a.m20 * a.m20) + (a.m21 * a.m21) + (a.m22 * a.m22));
    }

    public static float fnorm(SMat3 a)
    {
        return Mathf.Sqrt((a.m00 * a.m00) + (a.m01 * a.m01) + (a.m02 * a.m02)
                    + (a.m01 * a.m01) + (a.m11 * a.m11) + (a.m12 * a.m12)
                    + (a.m02 * a.m02) + (a.m12 * a.m12) + (a.m22 * a.m22)); 
    }

    public static float off(Mat3 a)
    { 
        return Mathf.Sqrt((a.m01 * a.m01) + (a.m02 * a.m02) + (a.m10 * a.m10) + (a.m12 * a.m12) + (a.m20 * a.m20) + (a.m21 * a.m21));
    }

    public static float off(SMat3 a) 
    { 
        return Mathf.Sqrt(2 * ((a.m01 * a.m01) + (a.m02 * a.m02) + (a.m12 * a.m12))); 
    }

    public static void mmul(out Mat3 Out, Mat3 a, Mat3 b)
    { 
        Out = new Mat3();
        Out.set(a.m00 * b.m00 + a.m01 * b.m10 + a.m02 * b.m20,
                a.m00 * b.m01 + a.m01 * b.m11 + a.m02 * b.m21,
                a.m00 * b.m02 + a.m01 * b.m12 + a.m02 * b.m22,
                a.m10 * b.m00 + a.m11 * b.m10 + a.m12 * b.m20,
                a.m10 * b.m01 + a.m11 * b.m11 + a.m12 * b.m21,
                a.m10 * b.m02 + a.m11 * b.m12 + a.m12 * b.m22,
                a.m20 * b.m00 + a.m21 * b.m10 + a.m22 * b.m20,
                a.m20 * b.m01 + a.m21 * b.m11 + a.m22 * b.m21,
                a.m20 * b.m02 + a.m21 * b.m12 + a.m22 * b.m22);
    }

    public static void mmul_ata(out SMat3 Out, Mat3 a)
    { 
        Out = new SMat3();

        Out.setSymmetric(a.m00 * a.m00 + a.m10 * a.m10 + a.m20 * a.m20,
                         a.m00 * a.m01 + a.m10 * a.m11 + a.m20 * a.m21,
                         a.m00 * a.m02 + a.m10 * a.m12 + a.m20 * a.m22,
                         a.m01 * a.m01 + a.m11 * a.m11 + a.m21 * a.m21,
                         a.m01 * a.m02 + a.m11 * a.m12 + a.m21 * a.m22,
                         a.m02 * a.m02 + a.m12 * a.m12 + a.m22 * a.m22);
    }

    public static void transpose(out Mat3 Out, Mat3 a) 
    {
        Out = new Mat3();

        Out.set(a.m00, a.m10, a.m20, a.m01, a.m11, a.m21, a.m02, a.m12, a.m22);
    }

    public static void vmul(out Vector3 Out, Mat3 a, Vector3 v)
    { 
        Out = new Vector3(
            (a.m00 * v.x) + (a.m01 * v.y) + (a.m02 * v.z),
            (a.m10 * v.x) + (a.m11 * v.y) + (a.m12 * v.z),
            (a.m20 * v.x) + (a.m21 * v.y) + (a.m22 * v.z));
    }

    public static void vmul_symmetric(out Vector3 Out, SMat3 a, Vector3 v) 
    { 
        Out = new Vector3(
            (a.m00 * v.x) + (a.m01 * v.y) + (a.m02 * v.z),
            (a.m01 * v.x) + (a.m11 * v.y) + (a.m12 * v.z),
            (a.m02 * v.x) + (a.m12 * v.y) + (a.m22 * v.z));
    }
}
