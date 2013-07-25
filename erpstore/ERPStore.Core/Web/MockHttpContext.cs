using System;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;

namespace ERPStore.Web
{
	public class MockHttpContext : HttpContextBase
	{
		private readonly string _relativeUrl;
		private readonly Models.UserPrincipal _principal;
		private readonly NameValueCollection _formParams;
		private readonly NameValueCollection _queryStringParams;
		private readonly HttpCookieCollection _cookies;
		private readonly SessionStateItemCollection _sessionItems;


		public MockHttpContext(string relativeUrl)
			: this(relativeUrl, null, null, null, null, null)
		{
		}

		public MockHttpContext(string relativeUrl, Models.UserPrincipal principal, NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection cookies, SessionStateItemCollection sessionItems)
		{
			_relativeUrl = relativeUrl;
			_principal = principal;
			_formParams = formParams;
			_queryStringParams = queryStringParams;
			_cookies = cookies;
			_sessionItems = sessionItems;
		}

		public override HttpRequestBase Request
		{
			get
			{
				return new MockHttpRequest(_relativeUrl, _formParams, _queryStringParams, _cookies);
			}
		}

		public override IPrincipal User
		{
			get
			{
				return _principal;
			}
			set
			{
				throw new System.NotImplementedException();
			}
		}

		public override HttpSessionStateBase Session
		{
			get
			{
				return null;
			}
		}

	}
}
