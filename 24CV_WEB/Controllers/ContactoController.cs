using _24CV_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace _24CV_WEB.Controllers
{
    public class ContactoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EnviarCorreo(string email, string comentario)
        {
            TempData["Email"] = email;
            TempData["Comentario"] = comentario;

            return View("Index");
        }
        [HttpPost]
        public IActionResult EnviarInformacion(InformacionViewModel model)
        {
            TempData["Nombre"] = model.Nombre;
            TempData["Apellidos"] = model.Apellidos;
            TempData["Email"] = model.Email;
            TempData["FechaNac"] = model.FechaNac;
            TempData["Turno"] = model.Turno;
            TempData["Comentario"] = model.Comentario;

            SendEmail(model);
            return View("Index", model);
        }
        public bool SendEmail(InformacionViewModel model)
        {
            MailMessage mail = new MailMessage();

            SmtpClient smtp = new SmtpClient("mail.shapp.mx", 587);

            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("moises.puc@shapp.mx", "Dhaserck_999");
            mail.From = new MailAddress("moises.puc@shapp.mx", "Administrador");
            mail.To.Add(model.Email);
            mail.Subject = "Notificación de contacto.";
            mail.IsBodyHtml = true;
            mail.Body = $"Se ha recibido información del correo <h1>{model.Email}</h1> <br/><p>{model.Nombre} {model.Apellidos}</p><br/><p>Fecha de nacimiento {model.FechaNac}</p><br/><p>{model.Turno}</p><br/><p>{model.Comentario}</p>";

            smtp.Send(mail);

            return true;
        }
    }
}
