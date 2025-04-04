

namespace Application.smtp
{
    public static class MailCode
    {
        public static async Task enviarCredenciales(string destinatario, string password)
        {
            string htmlBody = $@"
                <html>
                    <body style='font-family: Arial, sans-serif; background-color: #f4f6f9; margin: 0; padding: 0;'>
                        <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #ffffff; border-radius: 8px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);'>
                            <div style='display: flex; align-items: center;'>
                                <img src='https://www.unicesar.edu.co/wp-content/uploads/2024/05/LOGO-MENU.png' alt='Imagen' style='width: 50px; height: 50px; border-radius: 50%; margin-right: 10px;' />
                                <h2 style='color: #4CAF50;'>UPCPRO</h2>
                            </div>
                            
                            <div style='padding: 20px;'>
                                <h2 style='color: #4CAF50; text-align: center;'>¡Tu cuenta ha sido creada exitosamente!</h2>
                                
                                <p style='font-size: 16px; color: #333;'>Hola,</p>
                                <p style='font-size: 16px; color: #333;'>Nos complace informarte que tu cuenta ha sido creada con éxito. A continuación, te enviamos tus credenciales de acceso:</p>

                                <div style='background-color: #f4f4f4; padding: 15px; border-radius: 5px; margin: 20px 0; text-align: center;'>
                                    <p style='font-size: 18px; font-weight: bold; color: #333;'>Correo electrónico: <span style='color: #4CAF50;'>{destinatario}</span></p>
                                    <p style='font-size: 18px; font-weight: bold; color: #333;'>Contraseña: <span style='color: #4CAF50;'>{password}</span></p>
                                </div>

                                <p style='font-size: 16px; color: #333;'>Te recomendamos cambiar tu contraseña después de iniciar sesión.</p>

                                <div style='text-align: center; margin: 20px 0;'>
                                    <a href='https://www.unicesar.edu.co' style='background-color: #4CAF50; color: white; text-decoration: none; padding: 10px 20px; border-radius: 5px; font-size: 16px;'>Iniciar Sesión</a>
                                </div>

                                <p style='font-size: 16px; color: #333;'>Si no creaste esta cuenta, por favor ignora este correo.</p>
                            </div>

                            <footer style='text-align: center; font-size: 12px; color: #888; padding-top: 20px;'>
                                <p>Este es un correo automático, por favor no respondas.</p>
                            </footer>
                        </div>
                    </body>
                </html>
                ";

            await SmtpMail.enviarMail(destinatario, "Credenciales de tu cuenta", htmlBody);

        }
    }
}