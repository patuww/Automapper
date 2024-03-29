using System;
// Added Comment
namespace AutoMapper.Mappers
{
	public class ArrayMapper : EnumerableMapperBase<Array>
	{
		public override bool IsMatch(ResolutionContext context)
		{
			return (context.DestinationType.IsArray) && (context.SourceType.IsEnumerableType());
		}

		protected override void ClearEnumerable(Array enumerable)
		{
			// no op
		}

		protected override void SetElementValue(Array destination, object mappedValue, int index)
		{
			destination.SetValue(mappedValue, index);
		}

		protected override Array CreateDestinationObjectBase(Type destElementType, int sourceLength)
		{
			return ObjectCreator.CreateArray(destElementType, sourceLength);
		}
	}
}