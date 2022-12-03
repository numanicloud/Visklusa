using FigmaSharp;
using FigmaSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FigmaVisk
{
	public delegate TResult ResultSelector<out TResult>(FigmaNode node, FigmaNode? parent, int depth);

	public static class Helpers
	{
		public static IEnumerable<TResult> Traverse<TResult>
			(this FigmaCanvas canvas, ResultSelector<TResult> selector)
		{
			return canvas.children.SelectMany(x => Traverse(x, selector, 0, null));
		}

		public static IEnumerable<TResult> Traverse<TResult>
			(this FigmaNode pivot, ResultSelector<TResult> selector, int depth, FigmaNode? parent)
		{
			yield return selector(pivot, parent, depth);

			foreach (var child in pivot.GetChildren<FigmaNode>())
			{
				foreach (var node in Traverse(child, selector, depth + 1, pivot))
				{
					yield return node;
				}
			}
		}

		public static Rectangle? GetAbsoluteBounding(this FigmaNode node)
		{
			return node is FigmaVector vector ? vector.absoluteBoundingBox
				: node is FigmaFrame frame ? frame.absoluteBoundingBox
				: null;
		}

		public static IEnumerable<T> FilterNull<T>(this IEnumerable<T?> source) where T : class
		{
			foreach (var item in source)
			{
				if (item is not null)
				{
					yield return item;
				}
			}
		}

		public static (float, float, float, float) ToFloat(this (double, double, double, double) t)
		{
			return ((float)t.Item1, (float)t.Item2, (float)t.Item3, (float)t.Item4);
		}
	}
}
