using System.Collections.Generic;
using UnityEngine;

namespace ViewObjects.Unity
{

	public class ViewStudyMono : ViewObjMono, IViewStudy
	{

		[SerializeField] List<ViewObjMono> loadedObjs;

		public string viewName
		{
			get => gameObject.name;
			set => name = value;
		}

		public bool isValid
		{
			get => objs.Valid() && viewName.Valid();
		}

		public List<IViewObj> objs
		{
			get
			{
				var res = new List<IViewObj>();

				foreach (var obj in loadedObjs)
					if (obj != null && obj is IViewObj casted)
						res.Add(casted);

				return res;
			}
			set
			{
				loadedObjs = new List<ViewObjMono>();

				foreach (var obj in value)
					if (obj is ViewObjMono mono)
					{
						mono.transform.SetParent(transform);
						loadedObjs.Add(mono);
					}
					else
					{
						Debug.Log(obj.TypeName() + "- is not valid for mono");
					}
			}
		}
	}
}