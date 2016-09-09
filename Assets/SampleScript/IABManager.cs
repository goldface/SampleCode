using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace InAppBilling
{
	public class IABManager : MonoBehaviour
	{
		#region ## Self Singleton ##
		private static IABManager _instance;

		public static IABManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new GameObject(typeof(IABManager).ToString(), typeof(IABManager)).GetComponent<IABManager>();
				}

				return _instance;
			}
		}
		#endregion

		private static bool IsInitialized = false;

		// 플랫폼에 따라서 설정된다
		InAppBillingBase _InAppObj = null;

		// 상점 사용 가능 상태인지 확인
		public bool IsShopReady
		{
			get
			{
				return _InAppObj.IsShopReady;
			}
		}

		public void InitManager()
		{
			Debug.Log("+[IABManager] InitManager");

			DontDestroyOnLoad(this);

			if (!IsInitialized)
			{
#if UNITY_EDITOR
				_InAppObj = new InAppUnityEditor();
#elif UNITY_IOS
				_InAppObj = new InAppAppleAppStore();
#elif UNITY_ANDROID
				_InAppObj = new InAppGooglePlay();
#endif
				_InAppObj.Init();
				IsInitialized = true;
			}

			Debug.Log("-[IABManager] InitManager");
		}

		public void OnDestroy()
		{
			if(_InAppObj != null)
			{
				_InAppObj.OnDestroy();
				_InAppObj = null;
			}

			_instance = null;
			IsInitialized = false;
		}


		#region ## Unity -> Native ##
		public void RequestProduct() { _InAppObj.RequestProduct(); }
		public void PurchaseProduct(string productID, string payload) { _InAppObj.PurchaseProduct(productID, payload); }
		#endregion

		#region ## Native -> Unity ##
		public void onInitComplete(string result) { _InAppObj.onInitComplete(result); }
		public void onInAppComplete(string result) { _InAppObj.onInAppComplete(result); }
		public void onInAppFailed(string result) { _InAppObj.onInAppFailed(result); }
		public void onInAppRestore(string result) { _InAppObj.onInAppRestore(result); }
		public void onInAppRequest(string result) { _InAppObj.onInAppRequest(result); }
		#endregion
	}

}
