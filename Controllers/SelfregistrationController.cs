using Newtonsoft.Json;
using ScannerApp.Models;
using ScannerApp.Models.JSON;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ScannerApp.Controllers
{
    public class SelfregistrationController : Controller
    {


        public ActionResult Index(string id)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index([Bind(Include = "ID,Name,Phone,Gender")] Registration registration)
        {

            try
            {
                string phone = registration.Phone;

                using (var client = new HttpClient())
                {
                    var url = "http://175.143.69.73:8085/cloudIntercom/insertPerson";
                    client.DefaultRequestHeaders.Add("token", "180a7ba4-6334-4626-95d0-47c29e159eca");
                    var parameters = new Dictionary<string, string> {
                    { "deptId", "0b4e1d44a85e4512a3a0ee94d1334f86" },
                    { "name", registration.Name},
                    { "phone", registration.Phone },
                    { "sex", registration.Gender.ToString() }};
                    var encodedContent = new FormUrlEncodedContent(parameters);

                    var response = await client.PostAsync(url, encodedContent).ConfigureAwait(false);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // Do something with response. Example get content:
                        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        string personID = await GetPersonId(registration.Phone);
                        TempData["personID"] = personID;
                    }
                }

                return RedirectToAction("UploadPhoto");
            }
            catch (Exception)
            {

                return View();
            }
        }

        private async Task<string> GetPersonId(string phone)
        {
            string personId = "";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("token", "180a7ba4-6334-4626-95d0-47c29e159eca");
            string jsonString = "";

            HttpResponseMessage result = await client.GetAsync("http://175.143.69.73:8085/cloudIntercom/selectPersonByQueryVo");

            if (result.IsSuccessStatusCode)
            {
                jsonString = await result.Content.ReadAsStringAsync();
                dynamic response = JsonConvert.DeserializeObject(jsonString);
                List<Person_JsM> list = response.data.list.ToObject<List<Person_JsM>>();

                if (list.Count() > 0)
                {
                    personId = list.Where(_ => _.phone == phone).SingleOrDefault().id;
                }

            }

            return personId;
        }

        public ActionResult UploadPhoto()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadPhoto(HttpPostedFileBase file)
        {
            string perId = "";

            if (TempData["personID"] != null)
            {
                perId = TempData["personID"].ToString();
            }

           // string path = Server.MapPath("~/App_Data/uploads/");
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}


            if (file != null)
            {
                var fileNameExt = Path.GetFileName(file.FileName);
                //string[] ext = fileNameExt.ToString().Split('.');
                //string name = string.IsNullOrEmpty(phone) ? ext[0] : phone;
                //file.SaveAs(Path.Combine(path, name + "." + ext[1]));

                byte[] paramFileStream = new byte[file.ContentLength];
                file.InputStream.Read(paramFileStream, 0, paramFileStream.Length);

                //ImageConverter _imageConverter = new ImageConverter();
                //byte[] paramFileStream = (byte[])_imageConverter.ConvertTo(file, typeof(byte[]));


                using (var client = new HttpClient())
                {
                    var url = "http://175.143.69.73:8085/cloudIntercom/insertFaceFile";
                    client.DefaultRequestHeaders.Add("token", "180a7ba4-6334-4626-95d0-47c29e159eca");
                    var formContent = new MultipartFormDataContent
                                        {
                                        // Send form text values here
                                        {new StringContent(perId),"perId"},
                                        {new StringContent("0"),"angle" },
                                        // Send Image Here
                                        {new StreamContent(new MemoryStream(paramFileStream)),"files",fileNameExt}
                                        };
                    // var encodedContent = new FormUrlEncodedContent(parameters);

                    var response = await client.PostAsync(url, formContent);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // Do something with response. Example get content:
                        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);


                    }
                }
                return RedirectToAction("Notice");


            }
            else
            {
                ViewBag.Message = "Please Select any file";
                return View();
            }
        }

        public ActionResult Notice()
        {
            return View();
        }
    }
}