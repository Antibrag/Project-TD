using Godot;

namespace Data
{
    public interface IClonable
    {
        public object Clone();
    }

    public class GameObject
    {
        public MeshObject Mesh { get; set; }
        public int Level { get; set; }
    }

    public struct MeshObject
    {
        public string MeshPath;
        public Vector3 Scale;

        public MeshObject(string meshPath, in Vector3 scale)
        {
            MeshPath = meshPath;
            Scale = scale;
        }
    }
}