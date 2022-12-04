using System.Collections.Generic;
using System.Linq;
using FigmaSharp.Models;
using FigmaVisk.Capability;
using Visklusa.Abstraction.Semantics;
using Visklusa.Preset;
using Color = FigmaSharp.Color;

namespace FigmaVisk;

internal class DocumentAnalyzer
{
	private record RecursiveContext(int NextId, int Depth);

	public Element[] Analyze(FigmaDocument document)
	{
		var canvas = document.children[0];

		var context = new RecursiveContext(0, 0);
		return canvas.Traverse<Element>(
				(node, parent, depth) =>
				{
					context = context with { Depth = depth };

					var element = Convert(node, context);

					// 親子情報を登録
					if (parent is {})
					{
						element = element with
						{
							Capabilities = element.Capabilities
								.Append(new FamilyShip(parent.Id))
								.ToArray()
						};
					}

					context = context with { NextId = context.NextId + 1 };

					return element;
				})
			.FilterNull().ToArray();
	}

	private Element Convert(FigmaNode node, RecursiveContext context)
	{
		var caps = (node switch
			{
				FigmaText text => GetCapabilities(text, context),
				FigmaVector vector => GetCapabilities(new FigmaBox(vector), context),
				FigmaFrame frame => GetCapabilities(frame, context),
				_ => new ICapability[0],
			})
			.Append(new FigmaId(node.id, node.name))
			.ToArray();
			
		return new Element(context.NextId, caps);
	}

	private IEnumerable<ICapability> GetCapabilities(FigmaText text, RecursiveContext context)
	{
		var bound = text.absoluteBoundingBox;
		yield return new BoundingBox(bound.X, bound.Y, bound.Width, bound.Height);

		yield return new Text(text.characters, text.style.fontFamily, text.style.fontSize, GetFill(new FigmaBox(text)));
		yield return new ZOffset(context.NextId);
	}

	private IEnumerable<ICapability> GetCapabilities(FigmaFrame frame, RecursiveContext context)
	{
		foreach (var capability in GetCapabilities(new FigmaBox(frame), context))
		{
			yield return capability;
		}

		if (frame.name.EndsWith("@VerticalScroll"))
		{
			yield return new VerticalScroll();
		}

		if (frame.name.EndsWith("@VerticalList"))
		{
			yield return new VerticalList(frame.itemSpacing);
		}
	}

	private IEnumerable<ICapability> GetCapabilities(FigmaBox box, RecursiveContext context)
	{
		var bound = box.BoundingBox;
		yield return new BoundingBox(bound.X, bound.Y, bound.Width, bound.Height);

		var paint = new Paint(GetFill(box), GetStroke(box));
		if (paint.Fill != Fill.Blank || paint.Stroke != Stroke.Blank)
		{
			yield return paint;

			if (box.CornerRadius != 0)
			{
				yield return new RoundedRectangle(box.CornerRadius);
			}

			yield return new ZOffset(context.NextId);
		}

		if (box.Paint is {type: "IMAGE"} p)
		{
			yield return new ImageRef(p.imageRef);
			yield return new ZOffset(context.NextId);
		}
	}

	private static Stroke GetStroke(FigmaBox box)
	{
		return box.Stroke is not { } s
			? Stroke.Blank
			: GetStrokeCapability(s.color, box.StrokeWeight);

		static Stroke GetStrokeCapability(Color color, int weight)
		{
			var t = (color.R, color.G, color.B, color.A).ToFloat();
			return new Stroke(t.Item1, t.Item2, t.Item3, t.Item4, weight);
		}
	}

	private static Fill GetFill(FigmaBox box)
	{
		return box.Paint is not { } p ? Fill.Blank
			: p.color is null ? Fill.Blank
			: GetFillCapability(p.color);

		static Fill GetFillCapability(Color color)
		{
			var t = (color.R, color.G, color.B, color.A).ToFloat();
			return new Fill(t.Item1, t.Item2, t.Item3, t.Item4);
		}
	}
}