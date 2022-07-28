using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Diagnostics;

var sw = new Stopwatch(); sw.Start();
var client = new HttpClient();
var url= "https://inline.app/waiting/api/waiting/create-waiting?company=-MNCESUEwk6XYGi_SSev%3Ainline-live-2&branch=-MNCESeVk25UUKomalfj";
var request = new
{
    language= "en",
    groupSize= 10,
    phoneNumber= "+886977533306",
    time= "lunch",
    customerName= "柯閔翔",
    gender= 0,
    numberOfKidSets= 0,
    numberOfKidChairs= 0,
    customerNote= "11:30會到 謝謝"
};
StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

var responseMessage = await client.PostAsync(url, content);
int PostTimes = 1;
Console.WriteLine($"呼叫API第{PostTimes}次");
while (!responseMessage.IsSuccessStatusCode)
{
    PostTimes++;
    Console.WriteLine(responseMessage.StatusCode);
    Console.WriteLine($"呼叫API第{PostTimes}次");
    Thread.Sleep(500);
    responseMessage = await client.PostAsync(url, content);
}
sw.Stop();
Console.WriteLine($"執行花了 {sw.Elapsed.TotalSeconds} 秒鐘");
Console.WriteLine($"{DateTime.Now}呼叫API成功");
Console.WriteLine(responseMessage.StatusCode);
Console.WriteLine(responseMessage.Content);
byte[] buffer = await responseMessage.Content.ReadAsByteArrayAsync();
string responseString = Encoding.UTF8.GetString(buffer.ToArray(), 0, buffer.ToArray().Length);
Console.WriteLine(responseString);
Console.WriteLine("Press Enter To Exit");
Console.ReadLine();