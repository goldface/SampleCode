#if UNITY_ANDROID
using UnityEngine;
using System.Collections;

namespace InAppBilling
{
	public class InAppGooglePlay : InAppBillingBase
	{
		private AndroidJavaObject _JavaNativeObj = null;
		private const string UNITY_CLASS_NAME = "com.unity3d.player.UnityPlayer";
		public override void OnDestroy()
		{
			base.OnDestroy();

			_JavaNativeObj = null;
		}

		#region ## Unity -> Native ##
		public override void Init()					// Native 결제 모듈 Init
		{
			Debug.Log("+[InAppGooglePlay] Init");
			// Android Native Code 셋팅 및 호출
			// ex)
			if(_JavaNativeObj == null)
			{
				using (AndroidJavaClass jc = new AndroidJavaClass(UNITY_CLASS_NAME))
				{
					_JavaNativeObj = jc.GetStatic<AndroidJavaObject>("currentActivity");
				}
			}

			if(_JavaNativeObj != null)
			{
				//sample: _JavaNativeObj.Call("nativefunctionname", arg1, arg2, arg3...);
			}
			Debug.Log("-[InAppGooglePlay] Init");
		}

		public override void RequestProduct()		// 상품 정보 요청
		{
			// Google Play Consle에 등록한 물품 조회
		}

		public override void PurchaseProduct(string productID, string payload)		// 상품 구매 요청
		{
			// Java Native Call을 통해서 상품 구매 요청

		}
		#endregion


		#region ## Native -> Unity ##
		public override void onInitComplete(string result)		// Naitve 결제 모듈 Init 성공 CallBack
		{
			// to do
		}
		public override void onInAppComplete(string result)		// 상품 구매 성공 CallBack
		{
			// to do
		}
		public override void onInAppFailed(string result)		// 상품 구매 실패 CallBack
		{
			// to do
		}
		public override void onInAppRestore(string result)		// 상품 복구 성공 CallBack
		{
			// to do
		}
		#endregion
	}
}
#endif