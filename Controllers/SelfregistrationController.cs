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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ScannerApp.Controllers
{
    public class SelfregistrationController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string id)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index([Bind(Include = "ID,Name,Phone,Gender")] Registration registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string phone = registration.Phone;

                    string token = await GetLoginTokenAsync();
                    TempData["_token"] = token;

                    String deptId = await GetdeptId(registration.ID, token);
                    TempData["_deptId"] = deptId;

                    Device device = db.Devices.Where(_ => _.sn == registration.ID).SingleOrDefault();

                    TempData["_name"] = registration.Name;
                    TempData["_phone"] = registration.Phone;
                    TempData["_job"] = string.IsNullOrEmpty(device.client) ? device.sn : device.client;
                    TempData["_sex"] = registration.Gender.ToString();



                    //using (var client = new HttpClient())
                    //{
                    //    var url = "http://175.143.69.73:8085/cloudIntercom/insertPerson";
                    //    client.DefaultRequestHeaders.Add("token", token);
                    //    var parameters = new Dictionary<string, string> {
                    //{ "deptId", deptId },
                    //{ "name", registration.Name},
                    //{ "phone", registration.Phone },
                    //{ "job", device.client },
                    //{ "sex", registration.Gender.ToString() }};
                    //    var encodedContent = new FormUrlEncodedContent(parameters);

                    //    var response = await client.PostAsync(url, encodedContent).ConfigureAwait(false);
                    //    if (response.StatusCode == HttpStatusCode.OK)
                    //    {
                    //        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    //        string personID = await GetPersonId(registration.Phone, token);
                    //        TempData["personID"] = personID;
                    //    }
                    //}

                    return RedirectToAction("UploadSelfie");
                }
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        private async Task<string> GetdeptId(string sn, string token)
        {
            var url = "http://175.143.69.73:8085/cloudIntercom/selectGateEquipByQueryVo";
            var deptId = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("token", token);
                var parameters = new Dictionary<string, string> {
                    { "sn", sn }};
                var encodedContent = new FormUrlEncodedContent(parameters);

                var response = await client.PostAsync(url, encodedContent).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    dynamic jsData = JsonConvert.DeserializeObject(responseContent);
                    List<Device> list = jsData.data.list.ToObject<List<Device>>();

                    if (list.Count() > 0)
                    {
                        deptId = list.Where(_ => _.sn == sn).SingleOrDefault().deptId;
                    }
                }

                return deptId;
            }
        }

        private async Task<string> GetLoginTokenAsync()
        {
            var Loginurl = "http://175.143.69.73:8085/cloudIntercom/login";
            var token = "";
            using (var client = new HttpClient())
            {

                var parameters = new Dictionary<string, string> {
                    { "userName", "admin" },
                    { "password", "554206"}};
                var encodedContent = new FormUrlEncodedContent(parameters);

                var response = await client.PostAsync(Loginurl, encodedContent).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    dynamic data = JsonConvert.DeserializeObject(responseContent);
                    token = data.token.ToString();
                }

                return token;
            }
        }
       
        private async Task<string> GetPersonId(string phone, string token)
        {
            string personId = "";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("token", token);
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

        //[HttpPost]
        //public async Task<ActionResult> UploadPhoto1()
        //{

        //        var data = Request.Form;
        //        byte[] valor1 = Encoding.ASCII.GetBytes(data["image"].ToString());


        //    return View();
        //}

        [HttpPost]
        public async Task<ActionResult> UploadPhoto(HttpPostedFileBase file)
        {
            string perId = "";
            string token = "";

            if (TempData["personID"] != null)
            {
                perId = TempData["personID"].ToString();
            }

            if (TempData["_token"] != null)
            {
                token = TempData["_token"].ToString();
            }

            if (file != null)
            {
                var fileNameExt = Path.GetFileName(file.FileName);

                byte[] paramFileStream = new byte[file.ContentLength];
                file.InputStream.Read(paramFileStream, 0, paramFileStream.Length);

                using (var client = new HttpClient())
                {
                    var url = "http://175.143.69.73:8085/cloudIntercom/insertFaceFile";
                    client.DefaultRequestHeaders.Add("token", token);
                    var formContent = new MultipartFormDataContent
                                        {
                                        {new StringContent(perId),"perId"},
                                        {new StringContent("0"),"angle" },
                                        {new StreamContent(new MemoryStream(paramFileStream)),"files",fileNameExt}
                                        };

                    var response = await client.PostAsync(url, formContent);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
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

        public ActionResult UploadSelfy()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadSelfy(int? id)
        {
            string perId = "";
            HttpPostedFileBase file = Request.Files[0];
         
            string token = await GetLoginTokenAsync();
            string _deptId = TempData["_deptId"].ToString();
            string _name = TempData["_name"].ToString();
            string _phone = TempData["_phone"].ToString();
            string _job = TempData["_job"].ToString();
            string _sex = TempData["_sex"].ToString();

            if (file != null)
            {
                var fileNameExt = Path.GetFileName(file.FileName);

                byte[] paramFileStream = new byte[file.ContentLength];
                file.InputStream.Read(paramFileStream, 0, paramFileStream.Length);

                using (var client = new HttpClient())
                {
                    //================
                    var url = "http://175.143.69.73:8085/cloudIntercom/insertPerson";
                    client.DefaultRequestHeaders.Add("token", token);
                    var parameters = new Dictionary<string, string> {
                                    { "deptId", _deptId },
                                    { "name", _name},
                                    { "phone", _phone },
                                    { "job", _job},
                                    { "sex", _sex }};
                    var encodedContent = new FormUrlEncodedContent(parameters);

                    var response = await client.PostAsync(url, encodedContent).ConfigureAwait(false);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        perId = await GetPersonId(_phone, token);
                    }
                    //================
                }
                using (var client = new HttpClient())
                {
                    var url = "http://175.143.69.73:8085/cloudIntercom/insertFaceFile";
                    client.DefaultRequestHeaders.Add("token", token);
                    var formContent = new MultipartFormDataContent
                                        {
                                            {new StringContent(perId),"perId"},
                                            {new StringContent("0"),"angle" },
                                            {new StreamContent(new MemoryStream(paramFileStream)),"files",fileNameExt}
                                        };

                    var response = await client.PostAsync(url, formContent);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
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


        public ActionResult UploadSelfie()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> UploadSelfie(string image_data)
        {
            try
            {
                string perId = "";
                HttpPostedFileBase file = Request.Files[0];

                //string fname;
                //fname = file.FileName;
                //fname = Path.Combine(Server.MapPath("~/Uploads/"), fname+ ".png");
                //file.SaveAs(fname);

                string token = await GetLoginTokenAsync();
                string _deptId = TempData["_deptId"].ToString();
                string _name = TempData["_name"].ToString();
                string _phone = TempData["_phone"].ToString();
                string _job = TempData["_job"].ToString();
                string _sex = TempData["_sex"].ToString();

                if (file != null)
                {
                    var fileNameExt = Path.GetFileName(file.FileName + ".png");

                    byte[] paramFileStream = new byte[file.ContentLength];
                    file.InputStream.Read(paramFileStream, 0, paramFileStream.Length);

                    using (var client = new HttpClient())
                    {
                        //================
                        var url = "http://175.143.69.73:8085/cloudIntercom/insertPerson";
                        client.DefaultRequestHeaders.Add("token", token);
                        var parameters = new Dictionary<string, string> {
                                    { "deptId", _deptId },
                                    { "name", _name},
                                    { "phone", _phone },
                                    { "job", _job},
                                    { "sex", _sex }};
                        var encodedContent = new FormUrlEncodedContent(parameters);

                        var response = await client.PostAsync(url, encodedContent).ConfigureAwait(false);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            perId = await GetPersonId(_phone, token);
                        }
                        //================
                    }
                    using (var client = new HttpClient())
                    {
                        var url = "http://175.143.69.73:8085/cloudIntercom/insertFaceFile";
                        client.DefaultRequestHeaders.Add("token", token);
                        var formContent = new MultipartFormDataContent
                                        {
                                            {new StringContent(perId),"perId"},
                                            {new StringContent("0"),"angle" },
                                            {new StreamContent(new MemoryStream(paramFileStream)),"files",fileNameExt}
                                        };

                        var response = await client.PostAsync(url, formContent);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        }
                    }

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { msg = "Please Select any file" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message });
            }
        }


        //[HttpPost]
        //public async Task<ActionResult> UploadSelfie(string image_data)
        //{
        //    string perId = "";
        //    HttpPostedFileBase file = Request.Files[0];           

        //    //string fname;
        //    //fname = file.FileName;
        //    //fname = Path.Combine(Server.MapPath("~/Uploads/"), fname+ ".png");
        //    //file.SaveAs(fname);

        //    string token = await GetLoginTokenAsync();
        //    string _deptId = TempData["_deptId"].ToString();
        //    string _name = TempData["_name"].ToString();
        //    string _phone = TempData["_phone"].ToString();
        //    string _job = TempData["_job"].ToString();
        //    string _sex = TempData["_sex"].ToString();

        //    if (file != null)
        //    {
        //        var fileNameExt = Path.GetFileName(file.FileName+".png");



        //        byte[] paramFileStream = new byte[file.ContentLength];
        //        file.InputStream.Read(paramFileStream, 0, paramFileStream.Length);

        //        using (var client = new HttpClient())
        //        {
        //            //================
        //            var url = "http://175.143.69.73:8085/cloudIntercom/insertPerson";
        //            client.DefaultRequestHeaders.Add("token", token);
        //            var parameters = new Dictionary<string, string> {
        //                            { "deptId", _deptId },
        //                            { "name", _name},
        //                            { "phone", _phone },
        //                            { "job", _job},
        //                            { "sex", _sex }};
        //            var encodedContent = new FormUrlEncodedContent(parameters);

        //            var response = await client.PostAsync(url, encodedContent).ConfigureAwait(false);
        //            if (response.StatusCode == HttpStatusCode.OK)
        //            {
        //                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        //                perId = await GetPersonId(_phone, token);
        //            }
        //            //================
        //        }
        //        using (var client = new HttpClient())
        //        {
        //            var url = "http://175.143.69.73:8085/cloudIntercom/insertFaceFile";
        //            client.DefaultRequestHeaders.Add("token", token);
        //            var formContent = new MultipartFormDataContent
        //                                {
        //                                    {new StringContent(perId),"perId"},
        //                                    {new StringContent("0"),"angle" },
        //                                    {new StreamContent(new MemoryStream(paramFileStream)),"files",fileNameExt}
        //                                };

        //            var response = await client.PostAsync(url, formContent);
        //            if (response.StatusCode == HttpStatusCode.OK)
        //            {
        //                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //            }
        //        }
        //        return RedirectToAction("Notice");
        //       // return Json(new { success = true });
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Please Select any file";
        //        return View();
        //    }
        //}



        public ActionResult Notice()
        {
            return View();
        }
    }
}