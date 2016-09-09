using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace InAppBilling
{
	public abstract class InAppBillingBase
	{
		protected const string NOT_AVAILABLE_VALUE = "N/A";

		public class InAppProduct
		{
			public string ID;		// 마켓(구글, 앱스토어) 상품 ID
			public string Price;	// 상품 가격

			public InAppProduct(string id)
			{
				ID = id;
				Price = NOT_AVAILABLE_VALUE;
			}

			public InAppProduct(string id, string price)
			{
				ID = id;
				Price = price;
			}
		}

		protected bool _shopReady = false;

		/// <summary>
		/// 상점 사용 가능 상태인지 확인
		/// </summary>
		public bool IsShopReady
		{
			get
			{
				return _shopReady;
			}
		}

		protected JsonData _JsonProductInfo = null;

		// Native에서 전달 받은 Json 데이터를 Dictionary로 담는다.
		protected Dictionary<string, InAppProduct> _DicProduct = new Dictionary<string, InAppProduct>();


		/// <summary>
		/// 요청하는 ID해당하는 상품 값을 얻어온다.
		/// </summary>
		/// <param name="productID"> 마켓에 등록된 상품 ID </param>
		/// <param name="product"> 반환 될 상품 </param>
		/// <returns></returns>
		public virtual bool TryGetPriceInfo(string productID, out InAppProduct product)
		{
			if (!_DicProduct.TryGetValue(productID, out product))
			{
				product = new InAppProduct(productID, ConvertJsonToString(productID, _JsonProductInfo));
				_DicProduct.Add(productID, product);	// 한번 조회한 데이터는 Dictionary에 캐싱
				return false;
			}

			return true;
		}

		public virtual void OnDestroy()
		{
			_shopReady = false;

			if (_JsonProductInfo != null)
			{
				_JsonProductInfo.Clear();
			}

			if (_DicProduct != null)
			{
				_DicProduct.Clear();
			}
		}

		#region ## Unity -> Native ##
		public abstract void Init();				// Native 결제 모듈 Init
		public abstract void RequestProduct();		// 상품 정보 요청
		public abstract void PurchaseProduct(string productID, string payload);		// 상품 구매 요청
		#endregion

		#region ## Native -> Unity ##
		public abstract void onInitComplete(string result);		// Naitve 결제 모듈 Init 성공 CallBack
		public abstract void onInAppComplete(string result);	// 상품 구매 성공 CallBack
		public abstract void onInAppFailed(string result);		// 상품 구매 실패 CallBack
		public abstract void onInAppRestore(string result);		// 상품 복구 성공 CallBack

		/// <summary>
		/// Native -> Unity로 넘어오는 콜백
		/// </summary>
		/// <param name="result"> 상품 정보가 JSON Type으로 온다. </param>
		public virtual void onInAppRequest(string result)		// 상품 정보 요청 성공 CallBack
		{
			Debug.LogFormat("[onInAppRequest] result:'{0}'", result);

			_JsonProductInfo = JsonMapper.ToObject(result);

			// 상품 정보가 정상적으로 들어오면 상점을 사용 가능 상태로 바꾼다.
			if(_JsonProductInfo != null)
			{
				_shopReady = true;
			}
			
		}
		#endregion

		protected string ConvertJsonToString(string key, JsonData jsondata)
		{
			return jsondata.Keys.Contains(key) ? jsondata[key].ToString() : NOT_AVAILABLE_VALUE;
		}

	}
}
