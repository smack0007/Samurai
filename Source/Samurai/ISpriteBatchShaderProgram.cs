using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai
{
	public interface ISpriteBatchShaderProgram
	{
		void Use();

		void SetProjectionMatrix(ref Matrix4 matrix);

		void SetSampler(Texture texture);
	}
}
