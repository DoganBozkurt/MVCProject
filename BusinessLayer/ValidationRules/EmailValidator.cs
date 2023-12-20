using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

public class EmailValidator
{
    public bool IsValidEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(pattern);
        if (regex.IsMatch(email))
        {
            bool isOkay = true;//await ZeroBounceEmailVerifier(email);
            if (isOkay)
            {
                return true;
            }
            return false;
        }

        return false;
    }


    private const string ApiKey = "8bbc9ce6588d4a8ab71be022e414f3c8";
    private async Task<bool> ZeroBounceEmailVerifier(string emailAddress)
    {
        using (var httpClient = new HttpClient())
        {
            string apiUrl = $"https://api.zerobounce.net/v2/validate?api_key={ApiKey}&email={emailAddress}";

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (responseBody.Contains("\"status\":\"valid\""))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }
        }

        return false;
    }
}
