public class SMat3
    {
        public float m00, m01, m02, m11, m12, m22;

        public SMat3()
        { 
            clear();
        }

        public SMat3(float m00, float m01, float m02, float m11, float m12, float m22)
        { 
            this.setSymmetric(m00, m01, m02, m11, m12, m22);
        }

        public void clear() 
        {
            this.setSymmetric(0, 0, 0, 0, 0, 0); 
        }

        public void setSymmetric(float a00, float a01, float a02, float a11, float a12, float a22)
        {
            this.m00 = a00;
            this.m01 = a01;
            this.m02 = a02;
            this.m11 = a11;
            this.m12 = a12;
            this.m22 = a22;
        }

        public void setSymmetric(SMat3 rhs)
        {
            this.setSymmetric(rhs.m00, rhs.m01, rhs.m02, rhs.m11, rhs.m12, rhs.m22);
        }

        private SMat3(SMat3 rhs) 
        { 
            this.setSymmetric(rhs);
        }
    }