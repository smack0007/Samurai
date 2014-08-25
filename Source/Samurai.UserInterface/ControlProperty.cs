using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.UserInterface
{
	public abstract class ControlProperty
	{
		public abstract Type PropertyType
		{
			get;
		}
	}

	public class ControlProperty<T> : ControlProperty
	{
		public override Type PropertyType
		{
			get { return typeof(T); }
		}
				
		public Func<ControlProperty<T>, T, bool, T, bool, T> ComputeFunc
		{
			get;
			private set;
		}

		public T DefaultValue
		{
			get;
			private set;
		}

		private ControlProperty()
		{
		}

		public static ControlProperty<T> Create(
			Func<ControlProperty<T>, T, bool, T, bool, T> computeFunc = null,
			T defaultValue = default(T))
		{
			if (computeFunc == null)
				computeFunc = DefaultComputeFunc;

			return new ControlProperty<T>()
			{
				ComputeFunc = computeFunc,
				DefaultValue = defaultValue
			};
		}

		public static T DefaultComputeFunc(ControlProperty<T> property, T controlValue, bool controlHasValue, T parentValue, bool parentHasValue)
		{
			if (controlHasValue)
				return controlValue;

			if (parentHasValue)
				return parentValue;

			return property.DefaultValue;
		}

		public static T IgnoreParentComputeFunc(ControlProperty<T> property, T controlValue, bool controlHasValue, T parentValue, bool parentHasValue)
		{
			if (controlHasValue)
				return controlValue;

			return property.DefaultValue;
		}
	}
}
