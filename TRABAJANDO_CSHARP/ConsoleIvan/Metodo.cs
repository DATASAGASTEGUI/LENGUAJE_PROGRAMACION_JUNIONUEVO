using OfficeOpenXml;
using System.Net.Mail;
using System.Net;

namespace ConsoleIvan
{
    public class Metodo
    {
        public static void procesoExcel_1()
        {
            // Habilitar licencias no comerciales para EPPlus
            //ExcelPackage.LicenseContext = System.ComponentModel.LicenseContext.NonCommercial;

            // Ruta del archivo Excel a crear
            string filePath = "EjemploExcel.xlsx";

            // Crear un nuevo paquete Excel
            using (var package = new ExcelPackage())
            {
                // Crear una nueva hoja de trabajo
                var worksheet = package.Workbook.Worksheets.Add("Hoja1");

                // Escribir datos en la hoja
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Nombre";
                worksheet.Cells[1, 3].Value = "Edad";

                // Agregar algunos datos de ejemplo
                worksheet.Cells[2, 1].Value = 1;
                worksheet.Cells[2, 2].Value = "Juan Pérez";
                worksheet.Cells[2, 3].Value = 30;

                worksheet.Cells[3, 1].Value = 2;
                worksheet.Cells[3, 2].Value = "María López";
                worksheet.Cells[3, 3].Value = 25;

                worksheet.Cells[4, 1].Value = 3;
                worksheet.Cells[4, 2].Value = "Carlos Gómez";
                worksheet.Cells[4, 3].Value = 35;

                // Aplicar estilo a los encabezados
                using (var range = worksheet.Cells[1, 1, 1, 3])
                {
                    range.Style.Font.Bold = true; //Negrita
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                // Ajustar el ancho de las columnas automáticamente
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Guardar el archivo Excel
                var fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);

                Console.WriteLine($"Archivo Excel creado exitosamente en: {fileInfo.FullName}");
            }
        }

        public static void procesoCorreo_1()
        {
            try
            {
                // Configuración del servidor SMTP
                string smtpHost = "smtp.gmail.com"; // Servidor SMTP, por ejemplo, para Gmail
                int smtpPort = 587; // Puerto para TLS
                string smtpUser = "datasagamadrid@gmail.com"; // "tu_correo@gmail.com" Tu dirección de correo electrónico
                string smtpPass = "aaaAAA111"; // Contraseña o token de aplicación
 
                // Configuración del correo
                string fromAddress = "datasagamadrid@gmail.com"; // "tu_correo@gmail.com";
                string toAddress = "datasagamadrid@gmail.com";   // "destinatario@ejemplo.com";
                string subject = "Prueba de envío de correo";
                string body = "¡Hola! Este es un correo de prueba enviado desde C#.";

                // Crear el mensaje de correo
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromAddress);
                mail.To.Add(toAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = false; // Cambia a true si quieres enviar HTML en el cuerpo

                // Agregar un archivo adjunto (opcional)
                string attachmentPath = @"F:\BORJA80GB\TRABAJANDO\PROJECTS___C#\C#_TEXTO\ConsoleIvan\bin\Debug\net8.0\EjemploExcel.xlsx"; // Cambiar si es necesario
                if (System.IO.File.Exists(attachmentPath))
                {
                    mail.Attachments.Add(new Attachment(attachmentPath));
                }
                Console.WriteLine("LLEGO 1"); // OK
                // Configuración del cliente SMTP
                using (SmtpClient smtp = new SmtpClient(smtpHost, smtpPort))
                {
                    smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);
                    smtp.EnableSsl = true; // Activar SSL para conexiones seguras
                    smtp.Send(mail); // Enviar el correo
                }

                Console.WriteLine("¡Correo enviado exitosamente!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al enviar el correo: {ex.Message}");
            }
        }
    }
}


