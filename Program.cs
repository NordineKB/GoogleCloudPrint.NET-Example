using GoogleCloudPrintApi;
using GoogleCloudPrintApi.Models;
using GoogleCloudPrintApi.Models.Application;
using GoogleCloudPrintApi.Models.Printer;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace GoogleCloudPrint.NET_Example
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var clientId = "#printClientId#";
			var clientSecret = "#printClientSecret#";
			var printerId = "#printer-id#";
			var credPath = $"cerd.json";

			var provider = new GoogleCloudPrintOAuth2Provider(clientId, clientSecret);

			var jsonString = System.IO.File.ReadAllText(credPath);

			Token token = JsonConvert.DeserializeObject<Token>(jsonString);

			var client = new GoogleCloudPrintClient(provider, token);

			client.OnTokenUpdated += (sender, e) =>
			{
				string json = JsonConvert.SerializeObject(e);
				System.IO.File.WriteAllText(credPath, json);
			};

			var ticket = new CloudJobTicket
			{
				Print = new PrintTicketSection
				{
					Color = new ColorTicketItem { Type = Color.Type.STANDARD_MONOCHROME },
					//Duplex = new DuplexTicketItem { Type = Duplex.Type.LONG_EDGE },
					PageOrientation = new PageOrientationTicketItem { Type = PageOrientation.Type.LANDSCAPE },
					Copies = new CopiesTicketItem { Copies = 1 }
				}
			};

			// Create a request for file submission, you can either submit a url with SubmitFileLink class, or a local file with SubmitFileStream class
			var request = new SubmitRequest
			{
				PrinterId = printerId,
				Title = "Ebay-Printer",
				Ticket = ticket,
				Content = new SubmitFileLink("URL HERE")
			};

			// Submit request
			var response = await client.SubmitJobAsync(request);
		}
	}
}
