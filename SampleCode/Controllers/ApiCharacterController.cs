using Manager.Models;
using Microsoft.AspNetCore.Mvc;
using SampleCode.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCode.Controllers
{
    [ApiController]
    [Route("api/character")]
    public class ApiCharacterController : ControllerBase
    {
        [Route("auid")]
        [HttpPost]
        public async Task<JsonResult> Info(long auid)
        {
            CharacterResponse response = new CharacterResponse();

            try
            {
                string ip = string.Empty;
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
                if (null != remoteIpAddress)
                {
                    ip = remoteIpAddress.MapToIPv4().ToString();
                }
                                
                if (0 >= auid)
                {
                    return Common.GetResult(response, ErrorCode.ParameterError);
                }

                AccountSelect select = new AccountSelect();
                select.SetParam(auid);

                select.Connection(new System.Data.SqlClient.SqlConnection(""));
                var reader = await select.ExecuteReaderAsync();

                // 데이터 처리
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return Common.GetResult(response, ErrorCode.SystemError);
            }

            return Common.GetResult(response, ErrorCode.ErrorNone);
        }

        [Route("userinfo")]
        [HttpPost]
        public async Task<JsonResult> Info(string accountName, string accountPass)
        {
            CharacterResponse response = new CharacterResponse();

            try
            {
                if (string.IsNullOrEmpty(accountName))
                {
                    return Common.GetResult(response, ErrorCode.ParameterError);
                }

                if (string.IsNullOrEmpty(accountPass))
                {
                    return Common.GetResult(response, ErrorCode.ParameterError);
                }

                string ip = string.Empty;
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
                if (null != remoteIpAddress)
                {
                    ip = remoteIpAddress.MapToIPv4().ToString();
                }

                string url = ConfigManager.instance.ServerConfig.Config.ServerInfo.Host.URL;

                if (null != url)
                {
                    string result = string.Empty;
                    string data = $"name={accountName}&pass={accountPass}";

                    if (System.Net.HttpStatusCode.OK == await Common.PostRequestAsync(url, data, r => result = r).ConfigureAwait(false))
                    {
                        // 데이터 처리
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return Common.GetResult(response, ErrorCode.SystemError);
            }

            return Common.GetResult(response, ErrorCode.ErrorNone);
        }
    }
}
