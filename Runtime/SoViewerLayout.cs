using System;
using UnityEngine;
using ViewObjects.Viewer;

namespace ViewObjects.Unity
{

	public class SoViewerLayout : ScriptableObject
	{
		[SerializeField] ClassTypeReference objType;

		public IViewerLayout GetRef
		{
			get => objType != null ? (ViewerLayout)Activator.CreateInstance(objType.Type) : null;
		}

		public string GetName
		{
			get => GetRef?.TypeName();
		}

		public void SetRef(ViewerLayout obj)
		{
			objType = new ClassTypeReference(obj.GetType());
		}

		public ViewerLayoutMono ToViewMono()
		{
			var mono = new GameObject().AddComponent<ViewerLayoutMono>();
			mono.SetData(this);
			return mono;
		}
	}

}