#if UNITY_IOS
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace InAppBilling
{
	public class InAppAppleAppStore : InAppBillingBase
	{
		// Naitve IAB Setting
		[DllImport("__Internal")]
		private static extern void _InitInAppBilling();

		#region ## Unity -> Native ##
		public override void Init()					// Native 결제 모듈 Init
		{
			// iOS Native Code 셋팅 및 호출
			//ex)
			_InitInAppBilling();
		}

		public override void RequestProduct()		// 상품 정보 요청
		{
			// Apple itunes Connect 에 등록한 물품 조회
		}

		[DllImport("__Internal")]
		private static extern void _PurchaseProduct(string productID);

		public override void PurchaseProduct(string productID, string payload)		// 상품 구매 요청
		{
			// iOS는 payload 사용하지 않음
			// iOS Native Call을 통해서 상품 구매 요청
			//ex)
			_PurchaseProduct(productID);
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