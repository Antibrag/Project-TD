using Godot;

namespace Data
{
    public interface IClonable
    {
        public object Clone();
    }

    public struct BuildMesh
    {
        public Vector3 HeadPosition;
        public Vector3 BodyPosition;

        public BuildMesh(in Vector3 headPosition, in Vector3 bodyPosition)
        {
            HeadPosition = headPosition;
            BodyPosition = bodyPosition;
        }
    }
}