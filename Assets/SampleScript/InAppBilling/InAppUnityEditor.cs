#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

namespace InAppBilling
{
	/// <summary>
	/// Local & Development Test Only
	/// </summary>
	public class InAppUnityEditor : InAppBillingBase
	{
		#region ## Unity -> Native ##
		public override void Init()					// Native 결제 모듈 Init
		{
			// Unity Editor 테스트용 셋팅 및 호출
		}

		public override void RequestProduct()		// 상품 정보 요청
		{
			// Unity Editor에 등록한 물품 조회
		}

		public override void PurchaseProduct(string productID, string payload)		// 상품 구매 요청
		{
			// 내부 서버 및 로컬을 통해서 상품 구매 요청

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