public class Mat3
    {
        public float m00, m01, m02, m10, m11, m12, m20, m21, m22;
        public Mat3()
        { }
        public Mat3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22) { }
        public void clear() { }
        public void set(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22) { }
        public void set(Mat3 rhs) { }
        public void setSymmetric(float a00, float a01, float a02, float a11, float a12, float a22) { }
        public void setSymmetric(SMat3 rhs) { }

        private Mat3(Mat3 rhs) { }

    }
