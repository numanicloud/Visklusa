using System.Linq;
using FigmaSharp;
using FigmaSharp.Models;

namespace FigmaVisk
{
	internal class FigmaBox
	{
		public Rectangle BoundingBox { get; }
		public FigmaPaint? Paint { get; }
		public FigmaPaint? Stroke { get; }
		public int StrokeWeight { get; }
		public float CornerRadius { get; }
		public string Name { get; }

		public FigmaBox(FigmaVector vector)
		{
			BoundingBox = vector.absoluteBoundingBox;
			Paint = vector.fills.FirstOrDefault();
			Stroke = vector.strokes.FirstOrDefault();
			StrokeWeight = vector.strokeWeight;
			Name = vector.name;

			CornerRadius = vector is RectangleVector rect ? rect.cornerRadius : 0;
		}

		public FigmaBox(FigmaFrame frame)
		{
			BoundingBox = frame.absoluteBoundingBox;
			Paint = frame.fills.FirstOrDefault();
			Stroke = frame.strokes.FirstOrDefault();
			StrokeWeight = frame.strokeWeight;
			CornerRadius = frame.cornerRadius;
			Name = frame.name;
		}
	}
}
