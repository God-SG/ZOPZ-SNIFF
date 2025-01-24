using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SNIFF.Classes.Auth.Interfaces;
using SNIFF.Classes.Auth.Models;

namespace SNIFF.Classes.Auth;

internal class Authentication
{
	private readonly IRequester _requester;


	public Authentication(IRequester requester)
	{
		_requester = requester;
	}


	public async Task<Statistics> GetStatisticsAsync()
	{
		return await _requester.SendRequestAsync<Statistics>(HttpMethod.Get, "stats");
	}


}
