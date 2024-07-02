using DotNet4xTestWeb.Classes;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions.Authentication;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using System.Security.Claims;
using Microsoft.Graph.Models;
using System.Linq;

namespace DotNet4xTestWeb.Helpers
{
	public class TokenProvider : IAccessTokenProvider
	{
        public string BearerToken { get; set; }
        public TokenProvider(string token)
        {
			BearerToken = token;
        }
        public Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object> additionalAuthenticationContext = default, CancellationToken cancellationToken = default)
		{
			return Task.FromResult(BearerToken);
		}

		public AllowedHostsValidator AllowedHostsValidator { get; }
	}
	public static class GraphHelper
	{
		private static async Task<string> GetGraphAccessTokenAsync(string[] scopes)
		{
			IConfidentialClientApplication app = MsalAppBuilder.BuildConfidentialClientApplication();
			IAccount userAccount = await app.GetAccountAsync(ClaimsPrincipal.Current.GetMsalAccountId());
			if (userAccount != null)
			{
				AuthenticationResult result = await app.AcquireTokenSilent(scopes, userAccount).ExecuteAsync();
				return result.AccessToken;
			}
			else
			{
				Exception ex = new Exception("User account not found in the token cache");
				throw ex;
			}
		}
		private static async Task<GraphServiceClient> GetAuthenticatedClientAsync(string[] scopes)
		{
			string accessToken = await GetGraphAccessTokenAsync(scopes);
			var authenticationProvider = new BaseBearerTokenAuthenticationProvider(new TokenProvider(accessToken));
			var graphClient = new GraphServiceClient(authenticationProvider, Globals.GraphResourceId);
			return graphClient;
		}

		public static async Task<UserData> GetUserDetailsAsync(string[] scopes)
		{
			var graphClient = await GetAuthenticatedClientAsync(scopes);
			var user = await graphClient.Me
				.GetAsync(requestConfiguration =>
				{
					requestConfiguration.QueryParameters.Select = new string[] { "DisplayName", "Mail", "UserPrincipalName", "jobTitle", "companyName", "streetAddress", "city", "state", "countryOrRegion" };
				});

			Stream photostream = await graphClient.Me.Photo.Content.GetAsync();
			byte[] imageBytes = new byte [0];
			if (photostream != null)
			{
				using (MemoryStream ms = new MemoryStream())
				{
					await photostream.CopyToAsync(ms);
					imageBytes = ms.ToArray();
				}
			}

			return new UserData
			{
				Avatar = imageBytes,
				DisplayName = user.DisplayName,
				Email = string.IsNullOrEmpty(user.Mail) ? user.UserPrincipalName : user.Mail,
				JobTitle = user.JobTitle,
				CompanyName = user.CompanyName,
				Address = user.StreetAddress,
				City = user.City,
				StateProvince = user.State,
				CountryRegion = user.Country
			};
		}

		public static async Task<List<string>> GetUserListAsync(string[] scopes)
		{
			var graphClient = await GetAuthenticatedClientAsync(scopes);
			var users = await graphClient.Users
				.GetAsync(requestConfiguration =>
				{
					requestConfiguration.QueryParameters.Select = new string[] { "DisplayName", "Mail", "UserPrincipalName", "jobTitle", "companyName", "streetAddress", "city", "state", "countryOrRegion" };
				});
			if(users == null)
			{
				return new List<string>();
			}
			List<User> graphUsers = users.Value;
			return graphUsers.Select(x=>x.DisplayName).ToList();
		}
	}
}