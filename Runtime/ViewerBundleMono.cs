using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ViewObjects.Viewer;

namespace ViewObjects.Unity
{

	public class ViewerBundleMono : ViewObjMono, IViewerBundle
	{
		[SerializeField] List<ViewerLayoutMono> viewerLayouts = new();

		public List<IViewerLayout> layouts
		{
			get => viewerLayouts.Valid() ? viewerLayouts.Cast<IViewerLayout>().ToList() : new List<IViewerLayout>();
			set
			{
				viewerLayouts = new List<ViewerLayoutMono>();

				foreach (var item in value)
				{
					ViewerLayoutMono mono = null;
					if (item is ViewerLayoutMono casted)
					{
						mono = casted;
					}
					else if (item is ViewerLayout obj)
					{
						mono = new GameObject().AddComponent<ViewerLayoutMono>();
						mono.SetData(obj);
					}
					else
					{
						Debug.Log($"{item} is not valid for ref");
					}

					mono.transform.SetParent(transform);
					viewerLayouts.Add(mono);
				}
			}
		}

		public void Clear()
		{
			ViewObjMonoExt.ClearList(viewerLayouts);
			viewerLayouts = new List<ViewerLayoutMono>();
		}

		public void Build(Action<ViewerMono> onViewerCreate = null)
		{
			if (!layouts.Valid())
				return;

			foreach (var l in viewerLayouts)
				l.Build(onViewerCreate);
		}
	}
}