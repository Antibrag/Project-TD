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
        public string Path;
        public Vector3 Scale;

        public MeshObject(string path, in Vector3 scale)
        {
            Path = path;
            Scale = scale;
        }
    }

    public struct BuildMeshObject
    {
        public string HeadPath;
        public string BodyPath;
        public Vector3 HeadScale;
        public Vector3 BodyScale;

        public BuildMeshObject (string headPath, in Vector3 headScale, string bodyPath, in Vector3 bodyScale)
        {
            HeadPath = headPath;
            BodyPath = bodyPath;
            HeadScale = headScale;
            BodyScale = bodyScale;
        }
    }
}