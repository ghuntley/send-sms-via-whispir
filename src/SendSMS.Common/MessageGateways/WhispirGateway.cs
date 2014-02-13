using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using CuttingEdge.Conditions;
using NLog;
using SendSMS.Common.Entities;
using SendSMS.Common.Exceptions;
using ServiceStack;

namespace SendSMS.Common.MessageGateways
{
    public class WhispirGateway : IMessageGateway
    {
        public readonly string ContentType = "application/vnd.whispir.message-v1+json";
        private readonly Logger Log = LogManager.GetCurrentClassLogger();

        public WhispirGateway(string authorization, string apiurl, string apikey)
        {
            ApiAuthorization = authorization;
            ApiBaseUrl = apiurl;
            ApiKey = apikey;
        }

        public string ApiAuthorization { get; private set; }
        public string ApiBaseUrl { get; private set; }

        public string ApiKey { get; private set; }

        /// <summary>
        ///     Whispir Notes:
        ///     Each SMS message can contain up to 1600 characters.
        ///     The to field is Mandatory and is the phone number that the message will be sent to.
        ///     The subject field is Mandatory and should be set to a single whitespace due to poor API spec/implementation.
        ///     The body field is Mantatory and is the message that will be sent.
        /// </summary>
        /// <see cref="http://developer.whispir.com/docs/read/Messages_Resource" />
        public void SendSMS(SMS sms)
        {
            try
            {
                HttpClient client = GetHttpClient();
                string address = ApiBaseUrl + GetSMSEndpoint();
                string body = GetSMSBody(sms);

                Log.Debug("HTTP Request: {0}", address);
                Log.Trace("HTTP body: {0}", body);

                HttpResponseMessage response =
                    client.PostAsync(address, new StringContent(body, Encoding.UTF8, ContentType)).Result;

                Log.Debug("HTTP Response Code: {0}", response.StatusCode);
                Log.Trace("HTTP Response Headers: {0}", response.Headers.ToJson());
                Log.Debug("HTTP Response Content: {0}", response.Content.ReadAsStringAsync().Result);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Accepted:
                        Log.Info("The SMS has been accepted for processing.");
                        break;

                    case HttpStatusCode.Unauthorized:
                        throw new WhisipirAuthException(
                            "The base64 representation of the Whispir Username and Password specified in the Authorization HTTP header was incorrect.");

                    default:
                        throw new WhispirResponseException("The request has been rejected by the Whispir API.");
                }
            }
            catch (WhisipirAuthException ex)
            {
                Log.Error(ex.Message);
                throw;
            }
            catch (WhispirResponseException ex)
            {
                Log.Error(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
            }
        }

        public string GetSMSEndpoint()
        {
            return String.Format("/messages?apikey={0}", ApiKey);
        }

        /// <summary>
        ///     Whispir HTTP Responses:
        ///     202 Accepted - The request has been accepted for processing
        ///     401 Unauthorized - The authorization details provided in the Authorization header were incorrect.
        ///     415 Unsupported Media Type - The MIME type requested is not supported for the requested resource.
        /// </summary>
        /// <see cref="http://developer.whispir.com/docs/read/Messages_Resource" />
        public string GetSMSBody(SMS sms)
        {
            Condition.Requires(sms.To, "to")
                .IsNotNullOrWhiteSpace();

            Condition.Requires(sms.Message, "Message")
                .IsNotNullOrWhiteSpace();

            Condition.Requires(sms)
                .Evaluate(sms.Message.Length < 1599);

            var body = (WhispirSMS) sms;
            return body.ToJson();
        }

        public HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", ApiAuthorization);

            return client;
        }
    }
}