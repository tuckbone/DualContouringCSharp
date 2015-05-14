using UnityEngine;

namespace Code.Noise
{
    public partial class Noise
    {
        #region static readonlys
        private static readonly Vector3[] Grad3 = {
        new Vector3(1,1,0), new Vector3(-1,1,0), new Vector3(1,-1,0), new Vector3(-1,-1,0),
        new Vector3(1,0,1), new Vector3(-1,0,1), new Vector3(1,0,-1), new Vector3(-1,0,-1),
        new Vector3(0,1,1), new Vector3(0,-1,1), new Vector3(0,1,-1), new Vector3(0,-1,-1)
    };

        private static readonly int[] P = {151,160,137,91,90,15,
                                             131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
                                             190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
                                             88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
                                             77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
                                             102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
                                             135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
                                             5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
                                             223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
                                             129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
                                             251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
                                             49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
                                             138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180};


        private static readonly int[] Perm = new int[512];
        #endregion

        public static float Perlin(float x, float y)
        {
            var i = x > 0 ? (int)x : (int)x - 1;
            var j = y > 0 ? (int)y : (int)y - 1;

            x = x - i;
            y = y - j;

            i = i & 255;
            j = j & 255;

            var gll = Perm[i + Perm[j]] % 12;
            var glh = Perm[i + Perm[j + 1]] % 12;
            var ghl = Perm[i + 1 + Perm[j]] % 12;
            var ghh = Perm[i + 1 + Perm[j + 1]] % 12;

            var nll = Vector2.Dot(Grad3[gll], new Vector2(x, y));
            var nlh = Vector2.Dot(Grad3[glh], new Vector2(x, y - 1));
            var nhl = Vector2.Dot(Grad3[ghl], new Vector2(x - 1, y));
            var nhh = Vector2.Dot(Grad3[ghh], new Vector2(x - 1, y - 1));

            var u = (float)(x * x * x * (x * (x * 6 - 15) + 10));
            var v = (float)(y * y * y * (y * (y * 6 - 15) + 10));

            //var nyl = Mathf.Lerp(nll, nhl, u);
            var nyl = (1-u)*nll + u*nhl;
            //var nyh = Mathf.Lerp(nlh, nhh, u);
            var nyh = (1-u)*nlh + u*nhh;

            //var nxy = Mathf.Lerp(nyl, nyh, v);
            var nxy = (1-v)*nyl + v*nyh;

            return nxy;

            
        }

        public static float Perlin(float x, float y, float z)
        {
            var X = x > 0 ? (int)x : (int)x - 1;
            var Y = y > 0 ? (int)y : (int)y - 1;
            var Z = z > 0 ? (int)z : (int)z - 1;

            x = x - X;
            y = y - Y;
            z = z - Z;

            X = X & 255;
            Y = Y & 255;
            Z = Z & 255;

            var gi000 = Perm[X + Perm[Y + Perm[Z]]] % 12;
            var gi001 = Perm[X + Perm[Y + Perm[Z + 1]]] % 12;
            var gi010 = Perm[X + Perm[Y + 1 + Perm[Z]]] % 12;
            var gi011 = Perm[X + Perm[Y + 1 + Perm[Z + 1]]] % 12;
            var gi100 = Perm[X + 1 + Perm[Y + Perm[Z]]] % 12;
            var gi101 = Perm[X + 1 + Perm[Y + Perm[Z + 1]]] % 12;
            var gi110 = Perm[X + 1 + Perm[Y + 1 + Perm[Z]]] % 12;
            var gi111 = Perm[X + 1 + Perm[Y + 1 + Perm[Z + 1]]] % 12;


            //TODO: inline the dot products to speed up perlin noise
            var n000 = Vector2.Dot(Grad3[gi000], new Vector3( x, y, z));
            var n100 = Vector2.Dot(Grad3[gi100], new Vector3(x - 1, y, z));
            var n010 = Vector2.Dot(Grad3[gi010], new Vector3(x, y - 1, z));
            var n110 = Vector2.Dot(Grad3[gi110], new Vector3(x - 1, y - 1, z));
            var n001 = Vector2.Dot(Grad3[gi001], new Vector3( x, y, z - 1));
            var n101 = Vector2.Dot(Grad3[gi101], new Vector3(x - 1, y, z - 1));
            var n011 = Vector2.Dot(Grad3[gi011], new Vector3(x, y - 1, z - 1));
            var n111 = Vector2.Dot(Grad3[gi111], new Vector3(x - 1, y - 1, z - 1));

            var u = (float)(x * x * x * (x * (x * 6 - 15) + 10));
            var v = (float)(y * y * y * (y * (y * 6 - 15) + 10));
            var w = (float)(z * z * z * (z * (z * 6 - 15) + 10));

            //TODO: inline lerps to speed up perlin noise
            var nx00 = Mathf.Lerp(n000, n100, u);
            var nx01 = Mathf.Lerp(n001, n101, u);
            var nx10 = Mathf.Lerp(n010, n110, u);
            var nx11 = Mathf.Lerp(n011, n111, u);

            var nxy0 = Mathf.Lerp(nx00, nx10, v);
            var nxy1 = Mathf.Lerp(nx01, nx11, v);


            var nxyz = Mathf.Lerp(nxy0, nxy1, w);

            return nxyz;


        }
    }
}
