using System;
using System.Collections.Generic;

namespace Samurai.UserInterface
{
	public class ControlPropertyStore
	{
		class ValueWrapper<T>
		{
			public T Value;

			public bool IsNull
			{
				get
				{
					if (typeof(T).IsValueType)
						return false;

					return this.Value == null;
				}
			}

			public ValueWrapper(T value)
			{
				this.Value = value;
			}
		}

		Dictionary<ControlProperty, object> values;

		public ControlPropertyStore()
		{
			this.values = new Dictionary<ControlProperty, object>();
		}

		public bool TryGetValue<T>(ControlProperty<T> property, out T result)
		{
			result = default(T);

			object resultWrapper;
			if (this.values.TryGetValue(property, out resultWrapper))
			{
				result = ((ValueWrapper<T>)resultWrapper).Value;
				return true;
			}

			return false;
		}

		public bool SetValue<T>(ControlProperty<T> property, T value)
		{
			bool isChanged = false;

			object resultWrapperObj;
			if (this.values.TryGetValue(property, out resultWrapperObj))
			{
				ValueWrapper<T> valueWrapper = (ValueWrapper<T>)resultWrapperObj;
				
				if (valueWrapper.IsNull && !value.Equals(valueWrapper.Value))
				{
					isChanged = true;
				}
				else
				{
					isChanged = valueWrapper.Value.Equals(value);
				}

				valueWrapper.Value = value;
			}
			else
			{
				this.values[property] = new ValueWrapper<T>(value);
				isChanged = true;
			}

			return isChanged;
		}
	}
}
