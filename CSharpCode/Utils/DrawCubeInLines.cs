using UnityEngine;

namespace Code.Utils
{
    public static class DrawCubeInLines
    {
        public static void DrawCube(Vector3 pos, int size, Color color)
        {
            DrawGLLine(new Vector3(pos.x, pos.y, pos.z), new Vector3(pos.x + size, pos.y, pos.z), color);
            DrawGLLine(new Vector3(pos.x + size, pos.y, pos.z), new Vector3(pos.x + size, pos.y + size, pos.z), color);
            DrawGLLine(new Vector3(pos.x + size, pos.y + size, pos.z), new Vector3(pos.x, pos.y + size, pos.z), color);
            DrawGLLine(new Vector3(pos.x, pos.y + size, pos.z), new Vector3(pos.x, pos.y, pos.z), color);

            DrawGLLine(new Vector3(pos.x, pos.y, pos.z + size), new Vector3(pos.x + size, pos.y, pos.z + size), color);
            DrawGLLine(new Vector3(pos.x + size, pos.y, pos.z + size), new Vector3(pos.x + size, pos.y + size, pos.z + size), color);
            DrawGLLine(new Vector3(pos.x + size, pos.y + size, pos.z + size), new Vector3(pos.x, pos.y + size, pos.z + size), color);
            DrawGLLine(new Vector3(pos.x, pos.y + size, pos.z + size), new Vector3(pos.x, pos.y, pos.z + size), color);

            DrawGLLine(new Vector3(pos.x, pos.y, pos.z), new Vector3(pos.x, pos.y, pos.z + size), color);
            DrawGLLine(new Vector3(pos.x + size, pos.y, pos.z), new Vector3(pos.x + size, pos.y, pos.z + size), color);
            DrawGLLine(new Vector3(pos.x + size, pos.y + size, pos.z), new Vector3(pos.x + size, pos.y + size, pos.z + size), color);
            DrawGLLine(new Vector3(pos.x, pos.y + size, pos.z), new Vector3(pos.x, pos.y + size, pos.z + size), color);
        }

        public static void DrawTri(Vector3 pos1, Vector3 pos2, Vector3 pos3, Color color)
        {
            DrawGLLine(pos1, pos2, color);
            DrawGLLine(pos2, pos3, color);
            DrawGLLine(pos3, pos1, color);
        }

        public static void DrawGLLine(Vector3 P1, Vector3 P2, Color lineColor)
        {
            GL.PushMatrix();
            GL.Begin(GL.LINES);
            GL.Color(lineColor);
            GL.Vertex(P1);
            GL.Vertex(P2);
            GL.End();
            GL.PopMatrix();
        }
    }


}
