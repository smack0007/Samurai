using System;

namespace Samurai.Graphics
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
