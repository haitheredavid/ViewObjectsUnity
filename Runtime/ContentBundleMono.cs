using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ViewObjects.Unity
{

	public class ContentBundleMono : ViewObjMono, IViewContentBundle
	{
		[SerializeField] List<ContentMono> viewContents;

		public List<IViewContent> contents
		{
			get => viewContents.Valid() ? viewContents.Cast<IViewContent>().ToList() : new List<IViewContent>();
			set
			{
				viewContents = new List<ContentMono>();
				foreach (var v in value)
				{
					ContentMono mono = null;
					if (v is ContentMono contentMono)
						mono = contentMono;
					else
						mono = v switch
						{
							ITargetContent _ => new GameObject().AddComponent<ContentTargetMono>(),
							IBlockerContent _ => new GameObject().AddComponent<ContentBlockerMono>(),
							IDesignContent _ => new GameObject().AddComponent<ContentDesignMono>(),
							_ => null
						};

					mono.contentLayerMask = v.GetLayerMask();
					mono.transform.SetParent(transform);
					viewContents.Add(mono);
				}
			}
		}

		public void ChangeColors()
		{
			var colors = contents.CreateBundledColors();
			for (var i = 0; i < contents.Count; i++)
				contents[i].viewColor = colors[i];
		}

		public void Prime(Material material, Action<ContentMono> OnAfterPrime = null, Action<ContentObj> OnContentObjPrimed = null)
		{
			ChangeColors();

			foreach (var mono in viewContents)
			{
				var matInstance = Instantiate(material);
				mono.PrimeMeshData(matInstance, OnContentObjPrimed);
				OnAfterPrime?.Invoke(mono);
			}
		}

		void Purge()
		{
			if (viewContents.Valid())
				for (var i = contents.Count - 1; i >= 0; i--)
					if (Application.isPlaying)
						Destroy(viewContents[i].gameObject);
					else
						DestroyImmediate(viewContents[i].gameObject);

			viewContents = new List<ContentMono>();
		}
	}

	public static class ViewMonoExt
	{
		public static readonly int TargetLayer = 7;

		public static readonly int BlockerLayer = 8;

		public static readonly int DesignLayer = 6;

		public static readonly int CloudLayer = 9;

		public static int GetLayerMask(this IViewContent value)
		{
			return value switch
			{
				ITargetContent _ => TargetLayer,
				IBlockerContent _ => BlockerLayer,
				IDesignContent _ => DesignLayer,
				_ => 0
			};
		}
	}
}