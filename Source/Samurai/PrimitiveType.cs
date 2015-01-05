using System;

namespace Samurai
{
	public enum PrimitiveType : uint
	{
		Lines = GLContext.Lines,

		Points = GLContext.Points,

		Triangles = GLContext.Triangles,

        TriangleStrip = GLContext.TriangleStrip,

        TriangleFan = GLContext.TriangleFan
	}
}
